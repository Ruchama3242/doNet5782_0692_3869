using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL;
namespace BL
{
    public partial class BL : IBL.interfaceIBL
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
        public void addDrone(int id, int model, int weight, int stationId)
        {
            if (id < 10000 || id > 99999)
                throw new BLgeneralException("ERROR! the id must be with 5 digits");
            if (model <= 0)
                throw new BLgeneralException("ERROR! the model must be a positive number");

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
                catch (Exception e )
                {
                    throw new BLIdUnExistsException(e.Message);
                }
                IDAL.DO.Drone dr = droneDal(d);//drone of "DAL" type
                dl.addDrone(dr);//add drone to the list in the dal
                DroneArr.Add(d);//add drone to the list of the drones in the BL
                IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge { droneID = id, stationeld = stationId };
                dl.SendToCharge(id,stationId);//send the drone to charge
            }
            catch (Exception e)
            {
                throw new BLIdExistsException(e.Message);
            }
        }

        /// <summary>
        /// update the name of the drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void updateNameDrone(int ID, int model)
        {
            try
            {
                if (model !=0)
                {
                    int m = model;
                    if (m <= 0)
                        throw new BLgeneralException("ERROR! the model must be a positive number");
                    dl.updateDrone(ID, m);
                    IBL.BO.DroneToList dr = DroneArr.Find(p => p.ID == ID);
                    DroneArr.Remove(dr);
                    dr.droneModel = model;
                    DroneArr.Add(dr);
                }
            }
            catch (Exception e)
            {
                throw new BLIdUnExistsException(e.Message);
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

                    d.parcel = new IBL.BO.ParcelInTransfer();
                    d.parcel = pt;
                }
                return d;
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message);
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
                    IDAL.DO.DroneCharge tmp = dl.findStationOfDroneCharge(id);
                    //IEnumerable<IDAL.DO.Station> tmpList = new List<IDAL.DO.Station>();
                    //tmpList = dl.getAllStations();
                    //foreach (var item in tmpList)
                    //{
                    //    if (item.ID == tmp.stationeld)
                    //    {
                    //        IDAL.DO.Station s = new IDAL.DO.Station();
                    //        s.chargeSlots = item.chargeSlots + 1;
                    //        s.ID = item.ID;
                    //        s.lattitude = item.lattitude;
                    //        s.longitude = item.longitude;
                    //        s.name = item.name;
                    //        dl.updateStation(tmp.stationeld, s);
                    //    }
                    //}
                    //remove the drone frome the list of the droneCharge
                    dl.BatteryCharged(tmp);
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
                if ((myDrone.battery - (chargeCapacity[0] * distance(closed.location, myDrone.currentLocation)) < 0))
                    throw new BLgeneralException("the drone doesn't have enough charge");

                // drone
                myDrone.currentLocation = closed.location;
                myDrone.status = IBL.BO.DroneStatus.maintenace;
                myDrone.battery -= chargeCapacity[0] * distance(closed.location, myDrone.currentLocation);

                //הפונקציה הזו דואגת לשנות את עמדות הטעינה ולהוסיך יישות לרשימה המתאימה
                dl.SendToCharge(id, closed.ID);
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

                IDAL.DO.Parcel myParcel = findTheParcel(myDrone.currentLocation, myDrone.battery, IDAL.DO.Priorities.emergency);
                dl.ParcelDrone(myParcel.ID, myDrone.ID);
                myDrone.status = IBL.BO.DroneStatus.delivery;
                myParcel.requested = DateTime.Now;
                myParcel.droneID = myDrone.ID;
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }


        /// <summary>
        /// מקבלת מיקום רחפן ומוצאת חבילה לשיוך לפי עדיפות(חירןם) ומיקום
        /// </summary>
        /// <param name="a"></param>
        /// <param name="buttery"></param>
        /// <returns></returns>
        private IDAL.DO.Parcel findTheParcel(IBL.BO.Location a, double buttery, IDAL.DO.Priorities pri)
        {
            double d;
            IDAL.DO.Parcel theParcel = new IDAL.DO.Parcel();
            IBL.BO.Location b = new IBL.BO.Location();
            IDAL.DO.Customer c = new IDAL.DO.Customer();
            double far = 1000000;
            bool flug = false;

            //השאילתא אחראית למצוא את כל החבילות בעדיפות המבוקשת
            var p = dl.getAllParcels();
            var v = from item in p
                    where item.priority == pri
                    select item;

            foreach (var item in v)
            {
                c = dl.findCustomer(item.senderID);
                b.latitude = c.lattitude;
                b.longitude = c.longitude;
                d = distance(a, b);
                double fromCusToSta = distance(b, stationClose(b).location);
                double butteryUse = d * chargeCapacity[indexOfChargeCapacity(item.weight)] + fromCusToSta * chargeCapacity[0];
                if (d < far && (buttery - butteryUse) > 0&&item.delivered==DateTime.MinValue)
                {
                    far = d;
                    theParcel = item;
                }
            }
            if (v.Count() > 0)//if there is a parcel.priority. ....
                flug = true;

            if (!flug && pri == IDAL.DO.Priorities.emergency)//אם לא מצא בעדיפות הכי גבוהה מחפש בעדיפות מתחתיה
                theParcel = findTheParcel(a, buttery, IDAL.DO.Priorities.fast);

            if (pri == IDAL.DO.Priorities.fast && !flug)
                theParcel = findTheParcel(a, buttery, IDAL.DO.Priorities.normal);

            return theParcel;
        }
        private int indexOfChargeCapacity(IDAL.DO.WeightCategories w)
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
 
