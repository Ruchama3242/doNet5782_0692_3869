﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    internal partial class BL 
    {
        
        Random rnd = new Random();

        public IEnumerable<DroneToList> droneFilterWheight(WeightCategorie w)
        {
            DO.WeightCategories x= DO.WeightCategories.light;//הקומפיילר מחייב לאתחל תמשתנה כדי ששאר הפונקציה תהיה תקינה
            List<DroneToList> lst = new List<DroneToList>();
            if (w == WeightCategorie.Heavy)
                 x = DO.WeightCategories.heavy;

            if (w == WeightCategorie.Light)
                x = DO.WeightCategories.light;

            if (w == WeightCategorie.Medium)
                x = DO.WeightCategories.medium;

            foreach (var item in DroneArr)
            {
                if (item.weight == w)
                    lst.Add(item);
            }
            return lst;
        }

        public IEnumerable<DroneToList> droneFilterStatus(DroneStatus w)
        {
            List<DroneToList> lst = new List<DroneToList>();

            foreach (var item in DroneArr)
            {
                if (item.status == w)
                    lst.Add(item);
            }
            return lst;
        }

        /// <summary>
        /// "convert" a drone from BL type to DAL type
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private DO.Drone droneDal(DroneToList d)
        {
            DO.Drone dr = new DO.Drone { ID = d.ID, model = d.droneModel, weight = (DO.WeightCategories)d.weight };
            return dr;
        }

        public void addDrone(int id, int model, int weight, int stationId)
        {
            if (id < 10000 || id > 99999)
                throw new BLgeneralException("ERROR! the id must be with 5 digits");
            if (model <= 0)
                throw new BLgeneralException("ERROR! the model must be a positive number");

            try
            {
                DroneToList d = new DroneToList();
                d.ID = id;
                d.droneModel = model;
                d.weight = (WeightCategorie)weight;
                d.battery = rnd.Next(20, 40);
                d.status = DroneStatus.Maintenace;
               
                try
                {
                    DO.Station s = dl.findStation(stationId);
                    d.currentLocation = new Location();
                    d.currentLocation.latitude = s.lattitude;
                    d.currentLocation.longitude = s.longitude;
                }
                catch (Exception e )
                {
                    throw new BLIdUnExistsException(e.Message/*,e*/);
                }
                DO.Drone dr = droneDal(d);//drone of "DAL" type
                dl.addDrone(dr);//add drone to the list in the dal
                DroneArr.Add(d);//add drone to the list of the drones in the BL
                DO.DroneCharge dc = new DO.DroneCharge { droneID = id, stationeld = stationId };
                dl.SendToCharge(id,stationId);//send the drone to charge
            }
            catch (Exception e)
            {
                throw new BLIdExistsException(e.Message/*,e*/);
            }
        }

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
                    DroneToList dr = DroneArr.Find(p => p.ID == ID);
                    DroneArr.Remove(dr);
                    dr.droneModel = model;
                    DroneArr.Add(dr);
                }
            }
            catch (Exception e)
            {
                throw new BLIdUnExistsException(e.Message/*,e*/);
            }

        }

         public Drone findDrone(int id)
        {
            try
            {
                var drn = DroneArr.Find(x => x.ID == id);
                if (drn == null)
                    throw new BLIdUnExistsException("Error! the drone doesn't found");
                Drone d = new Drone();
                d.ID = drn.ID;
                d.model = drn.droneModel;
                d.weight = drn.weight;
                d.status = drn.status;
                d.battery = drn.battery;
                d.location = new Location();
                d.location = drn.currentLocation;
                ParcelInTransfer pt = new ParcelInTransfer();
                if (drn.status == DroneStatus.Delivery)
                {
                    pt.ID = drn.parcelNumber;
                    DO.Parcel p=new DO.Parcel();
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
                    pt.priority = (Priorities)p.priority;
                    pt.weight = (WeightCategorie)p.weight;
                    pt.sender = new CustomerInParcel();
                    pt.sender = getCustomerInParcel(p.senderID);
                    pt.target = new CustomerInParcel();
                    pt.target = getCustomerInParcel(p.targetId);
                    DO.Customer sender = dl.findCustomer(p.senderID);
                    DO.Customer target = dl.findCustomer(p.targetId);
                    pt.collectionLocation = new Location();
                    pt.collectionLocation.longitude = sender.longitude;
                    pt.collectionLocation.latitude = sender.lattitude;
                    pt.targetLocation = new Location();
                    pt.targetLocation.longitude = target.longitude;
                    pt.targetLocation.latitude = target.lattitude;
                    pt.distance = distance(pt.collectionLocation, pt.targetLocation);

                    d.parcel = new ParcelInTransfer();
                    d.parcel = pt;
                }
                return d;
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message/*,e*/);
            }
        }

        /// <summary>
        /// return the customer with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private CustomerInParcel getCustomerInParcel(int id)
        {
            CustomerInParcel c = new CustomerInParcel();
            c.ID = id;
            DO.Customer cs = dl.findCustomer(id);
            c.customerName = cs.name;
            return c;
        }

        public void releaseFromCharge(int id, double time)
        {
            try
            {
                DroneToList d = DroneArr.Find(p => p.ID == id);

                if (d.status == DroneStatus.Maintenace)
                {
                    //the time* charging rate per hour, couldnt be more then 100%
                    d.battery += time * chargeCapacity[4];
                    if (d.battery > 100)
                        d.battery = 100;

                    d.status = DroneStatus.Available;

                    // up the number of the empty charge slots
                    DO.DroneCharge tmp = dl.findStationOfDroneCharge(id);
                    
                    //remove the drone frome the list of the droneCharge
                    dl.BatteryCharged(tmp);
                }
                else throw new BLgeneralException("Error! the drone dont was in charge");
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message/*, e*/);
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
        public IEnumerable<DroneToList> getAllDrones()
        {
            List<DroneToList> lst = new List<DroneToList>();
            foreach (var item in DroneArr)
                lst.Add(item);
            return lst;
        }

        /// <summary>
        /// return the drone with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private DroneToList findDroneDal(int id)
        {
            try
            {
                var v = DroneArr.Find(p => p.ID == id);
                return v;
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message/*,e*/);
            }
        }

        public void sendToCharge(int id)
        {
            try
            {
                var myDrone = findDroneDal(id);
                if (myDrone.status != DroneStatus.Available)
                    throw new BLgeneralException("the drone isn'n  avilable");
                Station closed = stationClose(myDrone.currentLocation);
                if ((myDrone.battery - (chargeCapacity[0] * distance(closed.location, myDrone.currentLocation)) < 0))
                    throw new BLgeneralException("the drone doesn't have enough charge");

                // drone
                myDrone.currentLocation = closed.location;
                myDrone.status = DroneStatus.Maintenace;
                myDrone.battery -= chargeCapacity[0] * distance(closed.location, myDrone.currentLocation);

                //הפונקציה הזו דואגת לשנות את עמדות הטעינה ולהוסיך יישות לרשימה המתאימה
                dl.SendToCharge(id, closed.ID);
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

       public void parcelToDrone(int id)
        {
            try
            {
                var myDrone = findDroneDal(id);
                if (myDrone.status != DroneStatus.Available)
                    throw new BLgeneralException("the drone not avilable");

                DO.Parcel myParcel = findTheParcel(myDrone.weight,myDrone.currentLocation, myDrone.battery,DO.Priorities.emergency);
                dl.ParcelDrone(myParcel.ID, myDrone.ID);
                DroneArr.Remove(myDrone);
                myDrone.status = DroneStatus.Delivery;
                myDrone.parcelNumber = myParcel.ID;
                DroneArr.Add(myDrone);
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message,e);
            }
        }


        /// <summary>
        /// מקבלת מיקום רחפן ומוצאת חבילה לשיוך לפי עדיפות(חירןם) ומיקום
        /// </summary>
        /// <param name="a"></param>
        /// <param name="buttery"></param>
        /// <returns></returns>
        private DO.Parcel findTheParcel(BO.WeightCategorie we,BO.Location a, double buttery, DO.Priorities pri)
        {
        

            double d,x;
            DO.Parcel theParcel = new DO.Parcel();
          
           Location b = new Location();
            DO.Customer c = new DO.Customer();
            double far = 1000000;
           // bool flug = false;

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
                d = distance(a, b);//המרחק בין מיקום נוכחי למיקום השולח
                x = distance(b, new Location { longitude = dl.findCustomer(item.targetId).longitude, latitude = dl.findCustomer(item.targetId).lattitude });//המרחק בין מיקום שולח למיקום יעד
                double fromCusToSta = distance(new Location { longitude = dl.findCustomer(item.targetId).longitude, latitude = dl.findCustomer(item.targetId).lattitude }, stationClose(new Location { longitude = dl.findCustomer(item.targetId).longitude, latitude = dl.findCustomer(item.targetId).lattitude }).location);
                double butteryUse = x * chargeCapacity[indexOfChargeCapacity(item.weight)] + fromCusToSta * chargeCapacity[0]+d*chargeCapacity[0];
                if (d < far && (buttery - butteryUse) > 0&&item.scheduled==DateTime.MinValue&&weight(we,(WeightCategorie)item.weight)==true)
                {
                    far = d;
                    theParcel = item;
                    return theParcel;
                }
            }

            if ( pri == DO.Priorities.emergency)//אם לא מצא בעדיפות הכי גבוהה מחפש בעדיפות מתחתיה
                theParcel = findTheParcel(we,a, buttery, DO.Priorities.fast);

            if (pri == DO.Priorities.fast)
                theParcel = findTheParcel(we,a, buttery, DO.Priorities.normal);
            if (theParcel.ID==0)
                throw new BLgeneralException("ERROR! there is not a parcel that match to the drone ");
            return theParcel;

        }

        /// <summary>
        /// ruturn true if the two parameter of weight the same
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        private bool weight(WeightCategorie dr,WeightCategorie pa)
        {
            if (dr == WeightCategorie.Heavy)
                return true;
            if (dr == WeightCategorie.Medium && (pa == WeightCategorie.Medium || pa == WeightCategorie.Light))
                return true;
            if (dr == WeightCategorie.Light && pa == WeightCategorie.Light)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the index in an array that calculates the battery consumption
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        private int indexOfChargeCapacity(DO.WeightCategories w)
        {
            if (w == DO.WeightCategories.light)
                return 1;
            if (w == DO.WeightCategories.heavy)
                return 2;
            if (w == DO.WeightCategories.medium)
                return 3;

            return 0;

        }
    }

}
 