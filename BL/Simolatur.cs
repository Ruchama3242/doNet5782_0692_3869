using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using BO;
using System.Threading;
using static BL.BL;


namespace BL
{
    class Simolatur
    {
        const int delay = 500;
        const double speed = 100;

        #region
        /// <summary>
        /// constructor
        /// </summary>
        public Simolatur(BlApi.IBL bl ,int id, Action myDelegate, bool flug)
        {
            Drone drone = new Drone();
            Parcel parcel = new Parcel();
            lock (bl) { drone = bl.findDrone(id); }
            
            while(flug==true)
            {
                lock (bl) { drone = bl.findDrone(id); }
                if (drone.status == DroneStatus.Available)
                {
                    try
                    { 
                        //send the drone to delivery
                        bl.parcelToDrone(id);
                        //collect the parcel from the customer

                        //send the parcel to customer
                        Thread.Sleep(delay);
                    }
                    catch(Exception)
                    { 
                        bl.sendToCharge(id);
                        Thread.Sleep(delay);
                    }
                    
                }
                if(drone.status==DroneStatus.Maintenace)
                {
                    //צריך קודם שיגמור את הטעינה
                    bl.releaseFromCharge(id);
                    Thread.Sleep(delay);
                }
                if(drone.status==DroneStatus.Delivery)
                {
                    if(drone.parcel.status==true)
                        bl.packageDelivery(id);
                    else
                        bl.packageCollection(id);
                    Thread.Sleep(delay);
                }
                
            }

        }
        #endregion
    }
}
