using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace Dal
{
   sealed public class DalXml // : IDal
    {
        #region singelton

        static readonly DalXml instance = new DalXml();
        static DalXml() { }
        DalXml() { }
        public static DalXml Instance => instance;
        #endregion

        string customerPath = @"CustomerXml.xml";//
        string stationPath = @"StationsXml.xml";  //
        string parcelPath = @"ParcelXml.xml";//XElement
        string dronePath = @"DroneXml.xml";//XMLSerializer
        string dronechargePath = @"DroneCharge.xml";
        static int runID = 0;

        #region ----------------------------------stattion------------------------------------

        public IEnumerable<DO.Station> getAllStations()
        {
            List<DO.Station> list = new List<DO.Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return from Station in list
                   select Station;
        }

        public DO.Station findStation(int id)
        {
            List<DO.Station> list = new List<DO.Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            foreach (DO.Station item in list)
            {
                if (item.ID == id)
                    return item;
            }
            throw new DO.IdUnExistsException("ERROR! the station doesn't exist");
        }

        public IEnumerable<DO.Station> getStationsWithChargeSlots()
        {
            List<DO.Station> list = new List<DO.Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            var s = from Station in list
                    where Station.chargeSlots > 0
                    select Station;
            return s;
        }

        public void addStations(DO.Station temp)
        {
            bool flug = true;
            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            foreach (Station item in list)
            {
                if (item.ID == temp.ID) //return true if the field is the same
                    flug = false;
            }

            if (flug)
                list.Add(temp);
            else
                throw new DO.IdExistsException("ERROR! the ID already exist");

            XMLTools.SaveListToXMLSerializer(list, stationPath);
        }
        public void deleteStation(int id)
        {

            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            foreach (DO.Station item in list)
            {
                if (item.ID == id)
                {
                    list.Remove(item);
                    XMLTools.SaveListToXMLSerializer(list, stationPath);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the station doesn't found");
        }

        public void updateStation(int id, DO.Station st)
        {
            List<Station> list = new List<Station>();
            list = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            foreach ( var item in list)
            {
                if (item.ID == id)
                {
                    list.Remove(item);
                    list.Add(st);
                    XMLTools.SaveListToXMLSerializer(list, stationPath);
                    return;
                }
            }
            
            throw new DO.IdUnExistsException("ERROR! the station doesn't found");
        }
        #endregion
        #region-------------------------------------parcel-----------------------------------------
        public IEnumerable<Parcel> GetPartParcel()
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return from Parcel in list
                   select Parcel;
        }

        public int addParcel(DO.Parcel temp)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            temp.ID = runID++;
            list.Add(temp);
            XMLTools.SaveListToXMLSerializer(list, parcelPath);
            //temp.ID = DAL.DataSource.Config.runnerID;
            //DAL.DataSource.ParcelList.Add(temp);
            //DAL.DataSource.Config.runnerID++;
            return temp.ID;
        }
       
        public void ParcelDrone(int parcelID, int droneID)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (var item in list)
            { 
                if (item.ID == parcelID)
                {
                    DO.Parcel p = item;
                    list.Remove(item);
                    p.droneID = droneID;
                    p.scheduled = DateTime.Now;
                    list.Add(p);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.generalException("ERROR! the value not found");
        }

        public void ParcelPickedUp(int parcelID, DateTime day)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            foreach (var item in list)
            
            {
                if (item.ID == parcelID)
                {
                    DO.Parcel p = item;
                    p.pickedUp = day;
                    list.Remove(item);
                    list.Add(p);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.generalException("ERROR! the value not found");
        }

        public void ParcelReceived(int parcelID, DateTime day)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var l = from Parcel in list
                    where Parcel.ID == parcelID
                    select new
                    {
                        ID = Parcel.ID,
                        pickedUp = Parcel.pickedUp,
                        delivered = day,
                        priority = Parcel.priority,
                        requested = Parcel.requested,
                        scheduled = Parcel.scheduled,
                        senderID = Parcel.senderID,
                        targetID = Parcel.targetId,
                        weight = Parcel.weight
                    };

            XMLTools.SaveListToXMLSerializer(list, parcelPath);
            //foreach (var item in list)
            //{
            //    if (item.ID == parcelID)
            //    {
            //        DO.Parcel p = DAL.DataSource.ParcelList[i];
            //        p.delivered = day;
            //        DAL.DataSource.ParcelList[i] = p;
            //        return;
            //    }
            //}
            throw new DO.generalException("ERROR! the value not found");
        }

        public DO.Parcel findParcel(int id)
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            
            foreach (DO.Parcel item in list)
            {
                if (item.ID == id)
                    return item;
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }

        
        public IEnumerable<DO.Parcel> getAllParcels()
        {
            List<Parcel> list = new List<Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return from Parcel in list
                   select Parcel;
        }

        /// <summary>
        /// print all parcels that have no yet drone
        /// </summary>
        public IEnumerable<DO.Parcel> getParcelsWithoutDrone()
        {
            List<DO.Parcel> list = new List<DO.Parcel>();
            List<DO.Parcel> temp = new List<DO.Parcel>();
            temp = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (DO.Parcel item in list)
            {
                if (item.droneID == 0 && item.delivered == null)
                    temp.Add(item);
            }
            return temp;
        }

        public void deleteSParcel(int id)
        {
            List<DO.Parcel> list = new List<DO.Parcel>();
            list= XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (DO.Parcel item in list)
            {
                if (item.ID == id)
                {
                    list.Remove(item);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }

        public void updateParcel(int id, DO.Parcel p)
        {
            List<DO.Parcel> list = new List<DO.Parcel>();
            list = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (var item in list)
            {

                if (item.ID == id)
                {
                    list.Remove(item);
                    list.Add(p);
                    XMLTools.SaveListToXMLSerializer(list, parcelPath);
                    return;
                }
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }
        #endregion

        #region ----------------------------Drone-----------------------------------
        public IEnumerable<DO.Drone> GetPartOfDrone(Func<DO.Drone, bool> droneCondition = null)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
             var list = from Drone in droneList
                      select Drone;
            if (droneCondition == null)
                return list;
            return list.Where(droneCondition);
        }

        public void addDrone(DO.Drone temp)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if(droneList.Exists(x=>x.ID==temp.ID))
                throw new DO.IdExistsException();
            droneList.Add(temp);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList, dronePath);

        }

        public DO.DroneCharge SendToCharge(int DroneID, int StationID)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == DroneID))
                throw new DO.IdUnExistsException();
          
            DO.DroneCharge d = new DO.DroneCharge();
            d.droneID = DroneID;
            d.stationeld = StationID;
            d.enterToCharge = DateTime.Now;
            List<DO.Station> stationList= Dal.XMLTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
            DO.Station s = stationList.Find(x => x.ID == StationID);
            stationList.Remove(s);
            s.chargeSlots--;
            stationList.Add(s);
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronechargePath);
            dr.Add(d);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Station>(stationList, stationPath);
            Dal.XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(dr, dronechargePath);
            return d;
        }
        public void BatteryCharged(DO.DroneCharge dc)
        {
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronechargePath);
            if (!dr.Exists(x => x.droneID == dc.droneID))
                throw new DO.generalException("ERROR! value not found");
            dr.Remove(dc);
            List<DO.Station> stationList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
            DO.Station s = stationList.Find(x => x.ID == dc.stationeld);
            stationList.Remove(s);
            s.chargeSlots++;
            stationList.Add(s);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Station>(stationList, stationPath);
            Dal.XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(dr, dronechargePath);
        }
        public DO.Drone findDrone(int id)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == id))
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            return droneList.Find(x => x.ID == id);
           
        }

        /// <summary>
        /// return a list of all drones
        /// </summary>
        public IEnumerable<DO.Drone> getAllDrones()
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            var lst = from Drone in droneList
                      select Drone;
            return lst;
        }

        public void deleteDrone(int id)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == id))
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            var d = droneList.Find(x => x.ID == id);
            droneList.Remove(d);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList, dronePath);
        }

        public void updateDrone(int id, int mod)
        {
            List<DO.Drone> droneList = Dal.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Exists(x => x.ID == id))
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            var d = droneList.Find(x => x.ID == id);
            droneList.Remove(d);
            d.model = mod;
            droneList.Add(d);
            Dal.XMLTools.SaveListToXMLSerializer<DO.Drone>(droneList, dronePath);

        }
        public IEnumerable<DO.DroneCharge> findDroneCharge(int id)
        {
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronechargePath);

            //find all the station with empty charge slots
            var lst = from item in dr
                      where item.stationeld == id
                      select item;
            foreach (var item2 in lst)
                lst.ToList().Add(item2);
            return lst;
          
        }

        public DO.DroneCharge findStationOfDroneCharge(int id)
        {
            List<DO.DroneCharge> dr = Dal.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronechargePath);

            DO.DroneCharge temp = new DO.DroneCharge();
            foreach (DO.DroneCharge item in dr)
                if (item.droneID == id)
                    temp = item;

            return temp;
        }
        #endregion
    }
}

        
    

