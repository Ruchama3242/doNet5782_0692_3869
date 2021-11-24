using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL;
namespace BL
{
   partial class BL : InterfaceIBL
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
                    d.currentLocation = new IBL.BO.Location();
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

        /// <summary>
        /// get a id and return the drone(of bl) with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Drone findDrone(int id)
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
                d.location = new IBL.BO.Location();
                d.location = drn.currentLocation;
                IBL.BO.ParcelInTransfer pt = new IBL.BO.ParcelInTransfer();
                if (drn.status == IBL.BO.DroneStatus.delivery)
                {
                    pt.ID = drn.parcelNumber;
                    IDAL.DO.Parcel p;
                    try
                    {
                        p = dl.findParcel(drn.parcelNumber);//get the parcel from the dal
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
                    pt.sender = new IBL.BO.CustomerInParcel();
                    pt.sender = getCustomerInParcel(p.senderID);
                    pt.target = new IBL.BO.CustomerInParcel();
                    pt.target = getCustomerInParcel(p.targetId);
                    IDAL.DO.Customer sender = dl.findCustomer(p.senderID);
                    IDAL.DO.Customer target = dl.findCustomer(p.targetId);
                    pt.collectionLocation = new IBL.BO.Location();
                    pt.collectionLocation.longitude = sender.longitude;
                    pt.collectionLocation.latitude = sender.lattitude;
                    pt.targetLocation = new IBL.BO.Location();
                    pt.targetLocation.longitude = target.longitude;
                    pt.targetLocation.latitude = target.lattitude;
                    pt.distance = distance(pt.collectionLocation, pt.targetLocation);
                }
                d.parcel = new IBL.BO.ParcelInTransfer();
                d.parcel = pt;
                return d;
            }
            catch (Exception)
            {
                throw new BLgeneralException("Error!");
            }
        }

        /// <summary>
        /// return the customer with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IBL.BO.CustomerInParcel getCustomerInParcel(int id)
        {
            IBL.BO.CustomerInParcel c = new IBL.BO.CustomerInParcel();
            c.ID = id;
            IDAL.DO.Customer cs = dl.findCustomer(id);
            c.customerName = cs.name;
            return c;
        }
        
        /// <summary>
        /// release the drone from charge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void releaseFromCharge(int id, int time)
        {
            try
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
                    IDAL.DO.DroneCharge tmp = myDalObject.findStationOfDroneCharge(id);
                    IEnumerable<IDAL.DO.Station> tmpList = new List<IDAL.DO.Station>();
                    tmpList = myDalObject.getAllStations();
                    foreach (var item in tmpList)
                    {
                        if (item.ID == tmp.stationeld)
                        {
                            IDAL.DO.Station s = new IDAL.DO.Station();
                            s.chargeSlots = item.chargeSlots + 1;
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
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="deg"></param>
      /// <returns></returns>
        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        /// <summary>
        /// return a IEnumerable<IBL.BO.DroneToList>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.DroneToList> getAllDrones()
        {
            List<IBL.BO.DroneToList> lst = new List<IBL.BO.DroneToList>();
            foreach (var item in DroneArr)
                lst.Add(item);
            return lst;
        }

        /// <summary>
        /// return the drone with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IBL.BO.DroneToList findDroneDal(int id)
        {
            try
            {
                var v = DroneArr.Find(p => p.ID == id);
                return v;
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }


        /// <summary>
        /// send a drone to the closed station
        /// </summary>
        /// <param name="id"></param>
        public void sendToCharge(int id)
        {
            try
            {
                var myDrone = findDroneDal(id);
                if (myDrone.status != IBL.BO.DroneStatus.available)
                    throw new BLgeneralException("the drone isn'n  avilable");
                IBL.BO.Station closed = stationClose(myDrone.currentLocation);
                if ((myDrone.battery - (chargeCapacity[0] * distance(closed.location, myDrone.currentLocation)) < 0)
                    throw new BLgeneralException("the drone doesn't have enough charge");

                // drone
                myDrone.currentLocation = closed.location;
                myDrone.status = IBL.BO.DroneStatus.maintenace;
                myDrone.battery -= chargeCapacity[0] * distance(closed.location, myDrone.currentLocation);

                //הפונקציה הזו דואגת לשנות את עמדות הטעינה ולהוסיך יישות לרשימה המתאימה
                myDalObject.SendToCharge(id, closed.ID);
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// get a id of drone and find a  parcel 
        /// </summary>
        /// <param name="id"></param>
        public void parcelToDrone(int id)
        {
            try
            {
                var myDrone = findDroneDal(id);
                if (myDrone.status != IBL.BO.DroneStatus.available)
                    throw new BLgeneralException("the drone not avilable");

                IBL.BO.parcel myParcel = findTHEparcel(myDrone.currentLocation, myDrone.battery);
                myDalObject.ParcelDrone(myParcel.ID, myDrone.ID);
                myDrone.status = IBL.BO.DroneStatus.delivery;
                myParcel.requested = DateTime.Now;
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }


        /// <summary>
        /// מקבלת מיקום רחפן ומוצאת חבילה לשיוך לפי עדיפות ומיקום
        /// </summary>
        /// <param name="a"></param>
        /// <param name="buttery"></param>
        /// <returns></returns>
        private IBL.BO.parcel findTHEparcel(IBL.BO.Location a, double buttery)
        {
        }
            //private double distanceBetweenParcelToDrone(IBL.BO.parcel p, int id)
            //{
            //    IDAL.DO.Drone d = myDalObject.findDrone(id);
            //    IDAL.DO.Customer c = findCustomer(p.);

            //}
            private  int indexOfChargeCapacity(IDAL.DO.WeightCategories w)
            {
                if (w == IDAL.DO.WeightCategories.light)
                    return 1;
                if (w == IDAL.DO.WeightCategories.heavy)
                    return 2;
                if (w == IDAL.DO.WeightCategories.medium)
                    return 3;

                return 0;

            }
        }
    }
}
