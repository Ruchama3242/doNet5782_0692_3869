using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;

namespace BL
{
   partial class BL
    {
        Random rnd = new Random();
        /// <summary>
        /// "convert" a drone from BL type to DAL type
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private IDAL.DO.Drone droneDal(IBL.BO.DroneToList d)
        {
            IDAL.DO.Drone dr = new IDAL.DO.Drone { ID = d.ID, model = d.droneModel, weight = (IDAL.DO.WeightCategories)d.weight };
            return dr;
        }
        /// <summary>
        /// adding a drone to droneList
        /// </summary>
        /// <param name="drone"></param>
        public void addDrone(int id,int model,int weight,int stationId)
        {
           
            try
            {

                IBL.BO.DroneToList d = new IBL.BO.DroneToList();
                d.ID = id;
                d.droneModel = model;
                d.weight = (IBL.BO.WeightCategories)weight;
                d.battery = rnd.Next(20, 40);
                d.status = IBL.BO.DroneStatus.maintenace;
                try
                {
                    IDAL.DO.Station s = dl.findStation(stationId);
                    d.currentLocation.latitude = s.lattitude;
                    d.currentLocation.longitude = s.longitude;
                }
                catch (Exception )
                {
                    throw new BLIdUnExistsException("Error! The station dosen't exist");
                }
                IDAL.DO.Drone dr = droneDal(d);//drone of "DAL" type
                dl.addDrone(dr);//add drone to the list in the dal
                DroneArr.Add(d);//add drone to the list of the drones in the BL
                IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge { droneID = id, stationeld = stationId };
                dl.BatteryCharged(dc);//send the drone to charge
            }
            catch(Exception e)
            {
                throw new BLIdExistsException("Error! the drone is already exist");
            }
        }

        /// <summary>
        /// update the name of the drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void updateNameDrone(int ID, string model)
        {
            try
            {
                if(model!="")
                {
                    int m = int.Parse(model);
                    dl.updateDrone(ID, m);
                    IBL.BO.DroneToList dr = DroneArr.Find(p => p.ID == ID);
                    DroneArr.Remove(dr);
                    dr.droneModel = int.Parse(model);
                    DroneArr.Add(dr);
                }
            }
            catch (Exception)
            {
                throw new BLIdUnExistsException("Error! the drone not found");
            }

        }

        public IBL.BO.Drone getBlDrone(int id)
        {
            try
            {
                var drn = DroneArr.Find(x => x.ID == id);
                if (drn == null)
                    throw new BLIdUnExistsException("Error! the drone doesn't found");
                IBL.BO.Drone d = new IBL.BO.Drone();
                d.ID = drn.ID;
                d.model = drn.droneModel;
                d.weight = drn.weight;
                d.status = drn.status;
                d.battery = drn.battery;
                d.location = drn.currentLocation;
                IBL.BO.ParcelInTransfer pt = new IBL.BO.ParcelInTransfer();
                if (drn.status == IBL.BO.DroneStatus.delivery)
                {
                    pt.ID = drn.parcelNumber;
                    IDAL.DO.Parcel p;
                    try
                    {
                        p = dl.printParcel(drn.parcelNumber);//get the parcel from the dal
                    }
                    catch (Exception)
                    {
                        throw new BLIdUnExistsException("Error! the parcel not found");
                    }
                    if (p.pickedUp == DateTime.MinValue)
                        pt.status = false;
                    else
                        pt.status = true;
                    pt.priority = (IBL.BO.Priorities)p.priority;
                    pt.weight = (IBL.BO.WeightCategories)p.weight;
                    pt.sender = getCustomerInParcel(p.senderID);
                    pt.target = getCustomerInParcel(p.targetId);
                    IDAL.DO.Customer sender = dl.findCustomer(p.senderID);
                    IDAL.DO.Customer target = dl.findCustomer(p.targetId);
                    pt.collectionLocation.longitude = sender.longitude;
                    pt.collectionLocation.latitude = sender.lattitude;
                    pt.targetLocation.longitude = target.longitude;
                    pt.targetLocation.latitude = target.lattitude;
                    pt.distance = distance(pt.collectionLocation, pt.targetLocation);
                }
                d.parcel = pt;
                return d;
            }
            catch (Exception)
            {
                throw new BLgeneralException("Error!");
            }
        }

        private IBL.BO.CustomerInParcel getCustomerInParcel(int id)
        {
            IBL.BO.CustomerInParcel c = new IBL.BO.CustomerInParcel();
            c.ID = id;
            IDAL.DO.Customer cs = dl.findCustomer(id);
            c.customerName = cs.name;
            return c;
        }
        /// <summary>
        /// calculate a distance between 2 points
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        private double distance(IBL.BO.Location l1,IBL.BO.Location l2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = deg2rad(l2.latitude - l1.latitude);  // deg2rad below
            var dLon = deg2rad(l2.longitude - l1.longitude);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(l1.latitude)) * Math.Cos(deg2rad(l2.latitude)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        public void releaseFromCharge(int id, int time)
        {
            //רוחמה זה לא טעות שלא עידכנתי את הרחפן שבדל אלא שבתרגיל הזה נדרשנו למחוק את השדה הזה מהיישות בדל
            IBL.BO.DroneToList d = DroneArr.Find(p => p.ID == id);
            
            if (d.status == IBL.BO.DroneStatus.maintenace)
            {
                //the time* charging rate per hour, couldnt be more then 100%
                d.battery += time * chargeCapacity[4];
                if (d.battery > 100)
                    d.battery = 100;

                d.status = IBL.BO.DroneStatus.available;

                // up the number of the empty charge slots
               IDAL.DO.DroneCharge tmp= myDalObject.findStationOfDroneCharge(id);
                IEnumerable<IDAL.DO.Station> tmpList = new List<IDAL.DO.Station>();
                tmpList = myDalObject.printAllStations();
                foreach (var item in tmpList)
                {
                    if (item.ID == tmp.stationeld)
                    {
                        IDAL.DO.Station s= new IDAL.DO.Station();
                        s.chargeSlots = item.chargeSlots+1;
                        s.ID = item.ID;
                        s.lattitude = item.lattitude;
                        s.longitude = item.longitude;
                        s.name = item.name;
                        myDalObject.updateStation(tmp.stationeld, s);
                    }     
                }
                //remove the drone frome the list of the droneCharge
                myDalObject.BatteryCharged(tmp);
            }
            else throw new BLgeneralException("Error! the drone dont was in charge");

        }

        public IBL.BO.Drone getBlDrone()
        {
           
        }
        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public IEnumerable<IBL.BO.DroneToList> getAllDrones()
        {
            List<IBL.BO.DroneToList> lst = new List<IBL.BO.DroneToList>();
            foreach (var item in DroneArr)
                lst.Add(item);
            return lst;
        }
    }
}
