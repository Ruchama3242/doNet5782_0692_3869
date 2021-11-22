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
   partial class BL
    {
        public double [] chargeCapacity;
        IDAL.DO.DalObject.DalObject dl;
        List<IBL.BO.DroneToList> DroneArr;
        /// <summary>
        /// constructor
        /// </summary>
        BL()
        {
           
           dl = new IDAL.DO.DalObject.DalObject();
            DroneArr = new List<DroneToList>();
            chargeCapacity = dl.chargeCapacity();
            IEnumerable<IDAL.DO.Drone> d = dl.printAllDrones();
            IEnumerable<IDAL.DO.Parcel> p = dl.printAllParcels();
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

        private IBL.BO.Station stationCloseToCustomer(int id)
        {

        }
        
        private double distance(Location l1,Location l2)
        {
           
            return l1.
        }
       
        /// <summary>
        /// update some field in the station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="emptyChargeSlot"></param>
        public void updateStation(int Id ,string name, int emptyChargeSlot)
        {
            
        }
    }
}
