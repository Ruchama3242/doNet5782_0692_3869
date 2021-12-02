using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL.BO;
using IDAL.DO;
using IBL;
namespace BL
{
    public partial  class BL: IBL.interfaceIBL
    {
        public double[] chargeCapacity;
        DAL.interfaceIDal dl;
        //DAL.interfaceIDal dal;
        //IDAL.DO.DalObject.DalObject dl= new IDAL.DO.DalObject.DalObject();
        //IDAL.DO.DalObject.DalObject myDalObject = new IDAL.DO.DalObject.DalObject();
        List<IBL.BO.DroneToList> DroneArr;

        /// <summary>
        /// constructor
        /// </summary>
         public BL()
        {
            bool flag = false;
            Random rnd = new Random();
            double minBatery = 0;
            dl = new IDAL.DO.DalObject.DalObject();
            DroneArr = new List<DroneToList>();
            chargeCapacity = dl.chargeCapacity();
            IEnumerable<IDAL.DO.Drone> d = dl.getAllDrones();
            IEnumerable<IDAL.DO.Parcel> p = dl.getAllParcels();
            foreach (var item in d)
            {
                IBL.BO.DroneToList drt = new DroneToList();
                drt.ID = item.ID;
                drt.droneModel = item.model;
                foreach (var pr in p)
                {
                    if (pr.droneID == item.ID && pr.delivered == DateTime.MinValue)
                    {
                        IDAL.DO.Customer sender = dl.findCustomer(pr.senderID);
                        IDAL.DO.Customer target = dl.findCustomer(pr.targetId);
                        IBL.BO.Location senderLocation = new Location { latitude = sender.lattitude, longitude = sender.longitude };
                        IBL.BO.Location targetLocation = new Location { latitude = target.lattitude, longitude = target.longitude };
                        drt.status = DroneStatus.delivery;
                        if (pr.pickedUp == DateTime.MinValue && pr.scheduled != DateTime.MinValue)//החבילה שויכה אבל עדיין לא נאספה
                        {
                            drt.currentLocation = new Location { latitude = stationCloseToCustomer(pr.senderID).lattitude, longitude = stationCloseToCustomer(pr.senderID).longitude };
                            minBatery = distance(drt.currentLocation, senderLocation) * chargeCapacity[0];
                            minBatery += distance(senderLocation, targetLocation) * chargeCapacity[indexOfChargeCapacity(pr.weight)];
                            minBatery += distance(targetLocation, new Location { latitude = stationCloseToCustomer(pr.targetId).lattitude, longitude = stationCloseToCustomer(pr.targetId).longitude }) * chargeCapacity[0];
                        }
                        if (pr.pickedUp != DateTime.MinValue && pr.delivered == DateTime.MinValue)//החבילה נאספה אבל עדיין לא הגיעה ליעד
                        {
                            drt.currentLocation = new Location();
                            drt.currentLocation = senderLocation;
                            minBatery = distance(targetLocation, new Location { latitude = stationCloseToCustomer(pr.targetId).lattitude, longitude = stationCloseToCustomer(pr.targetId).longitude }) * chargeCapacity[0];
                            minBatery += distance(drt.currentLocation, targetLocation) * chargeCapacity[indexOfChargeCapacity(pr.weight)];               
                        }
                        drt.battery = rnd.Next((int)minBatery, 101) /*/ 100*/;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    int temp = rnd.Next(1, 3);
                    if (temp == 1)
                        drt.status = IBL.BO.DroneStatus.available;
                    else
                        drt.status = IBL.BO.DroneStatus.maintenace;
                    if (drt.status == IBL.BO.DroneStatus.maintenace)
                    {
                        int l = rnd.Next(0, dl.getAllStations().Count()), i = 0;
                        IDAL.DO.Station s = new IDAL.DO.Station();
                        foreach (var ite in dl.getAllStations())
                        {
                            s = ite;
                            if (i == l)
                                break;
                            i++;
                        }
                        drt.currentLocation = new Location { latitude = s.lattitude, longitude = s.longitude };
                        drt.battery = rnd.Next(0, 21) /*/ 100*/;
                    }
                    else
                    {
                        List<IDAL.DO.Customer> lst = new List<IDAL.DO.Customer>();
                        foreach (var pr in p)
                        {
                            if (pr.delivered != DateTime.MinValue)
                                lst.Add(dl.findCustomer(pr.targetId));
                        }

                        int l = rnd.Next(0, lst.Count());
                        drt.currentLocation = new Location { latitude = lst[l].lattitude, longitude = lst[l].longitude };
                        minBatery += distance(drt.currentLocation, new Location { longitude = stationCloseToCustomer(lst[l].ID).longitude, latitude = stationCloseToCustomer(lst[l].ID).lattitude }) * chargeCapacity[0];
                        drt.battery = rnd.Next((int)minBatery, 101)/* / 100*/;
                    }
                }
                DroneArr.Add(drt);
            }
        }

        /// <summary>
        /// calculate a distance between 2 points
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        private double distance(IBL.BO.Location l1, IBL.BO.Location l2)
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



        /// <summary>
        /// מחזירה את התחנה הקרובה ביותר שיש לה עמדות טעינה פנויות
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private IBL.BO.Station stationClose(Location a)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();//the closest station to the customer
            IBL.BO.Location l = new Location();
            l = a;
            IEnumerable<IDAL.DO.Station> st = dl.getAllStations();//the list of the stations
            double d = distance(l, new IBL.BO.Location { latitude = st.First().lattitude, longitude = st.First().longitude });//d is the smallest distance between the cudtomer and a station, now its the first statio in the list
            station = st.First();
            foreach (var item in st)
            {
                if (distance(l, new IBL.BO.Location { latitude = item.lattitude, longitude = item.longitude }) < d)
                {
                    d = distance(l, new IBL.BO.Location { latitude = item.lattitude, longitude = item.longitude });
                    station = item;
                }
            }
            return convertStation(station);
        }




        /// <summary>
        /// the function get an id of a customer and return the closest station to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IDAL.DO.Station stationCloseToCustomer(int id)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();//the closest station to the customer
            IDAL.DO.Customer c = new IDAL.DO.Customer();
            c = dl.findCustomer(id);//the customer
            IBL.BO.Location l = new Location { latitude = c.lattitude, longitude = c.longitude };//the location of the customer
            IEnumerable<IDAL.DO.Station> st = dl.getAllStations();//the list of the stations
            double d = distance(l, new IBL.BO.Location { latitude = st.First().lattitude, longitude = st.First().longitude });//d is the smallest distance between the cudtomer and a station, now its the first statio in the list
            station = st.First();
            foreach (var item in st)
            {
                if (distance(l, new IBL.BO.Location { latitude = item.lattitude, longitude = item.longitude }) < d)
                {
                    d = distance(l, new IBL.BO.Location { latitude = item.lattitude, longitude = item.longitude });
                    station = item;
                }
            }
            return station;
        }

    }
}


    

