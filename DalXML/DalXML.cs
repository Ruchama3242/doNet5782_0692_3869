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
        static string dir = @"..\..\..\..\xml\";

        string customerPath = @"CustomerXml.xml";//
        string stationPath = @"StationsXml.xml";  //
        string parcelPath = @"ParcelXml.xml";//
        string dronePath = @"DroneXml.xml";//XMLSerializer
        string droneChargePath = @"DroneCharge.xml";
        #region singelton

        static readonly DalXml instance = new DalXml();
        static DalXml() { }
        DalXml() 
        { 
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(dir + customerPath))
                Directory.CreateDirectory(dir + customerPath);
               

            if (!File.Exists(dir + dronePath))
                Directory.CreateDirectory(dir + dronePath);

            if (!File.Exists(dir + droneChargePath))
                Directory.CreateDirectory(dir + droneChargePath);

            if (!File.Exists(dir + parcelPath))
                Directory.CreateDirectory(dir + parcelPath);

            if(!File.Exists(dir+stationPath))
                Directory.CreateDirectory(dir + stationPath);
        }
        public static DalXml Instance => instance;
        #endregion

       

        static int runID = 0;

        #region ----------------------------------stattion------------------------------------

       // DalName = dalConfig.Element("packnum").Value;
        //    dalConfig.Element("packnum").Value = 8887;
        //    dalConfig.Save(path);
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

    }
}
