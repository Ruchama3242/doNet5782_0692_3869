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
        public Simolatur(BlApi.IBL bl ,int id, Action myDelegate, Func<bool> flug)
        {
            Drone drone = new Drone();
            Parcel parcel = new Parcel();
            lock (bl) { drone = bl.findDrone(id); }
            
            while(!flug()==true)
            {
                if (drone.status == DroneStatus.Available)
                {
                    if ( true/*בודק אם יש לו מספיק סוללה)*/)
                    {
                        //send the drone to delivery
                        bl.parcelToDrone(id);
                        //collect the parcel from the customer
                        bl.packageCollection(id);
                        //send the parcel to customer
                        bl.packageDelivery(id);
                    }
                    else
                    {
                        bl.sendToCharge(id);
                    }
                    
                }
                
            }

        }
        #endregion
    }
}
