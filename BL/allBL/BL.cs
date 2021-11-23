using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL.BO;
using IDAL.DO;
namespace BL
{
   partial class BL :InterfaceBL
    {
        public double [] chargeCapacity;
        IDAL.DO.DalObject.DalObject dl;
        IDAL.DO.DalObject.DalObject myDalObject;
        List<IBL.BO.DroneToList> DroneArr;

        /// <summary>
        /// constructor
        /// </summary>
        BL()
        {
           
           dl = new IDAL.DO.DalObject.DalObject();
            DroneArr = new List<DroneToList>();
            chargeCapacity = dl.chargeCapacity();
            IEnumerable<IDAL.DO.Drone> d = dl.getAllDrones();
            IEnumerable<IDAL.DO.Parcel> p = dl.getAllParcels();
            foreach(var item in d)
            {
                IBL.BO.DroneToList drt=new DroneToList();
                drt.ID = item.ID;
                foreach(var pr in p)
                {
                    if(pr.droneID==item.ID&&pr.delivered==DateTime.MinValue)
                    {
                        drt.status = DroneStatus.delivery;

                    }
                }
               
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

        }
    }
}
