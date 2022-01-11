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
        const int delay = 1000;
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

            while (!flug())
            {
                lock (bl) { drone = bl.findDrone(id); }

                switch(drone.status)
                {
                    case DroneStatus.Available:
                        try
                        {
                            //send the drone to delivery
                            bl.parcelToDrone(id);
                            myDelegate();
                            Thread.Sleep(delay);
                           
                        }
                        catch (Exception)
                        {
                            if (drone.battery != 100)
                            {
                                bl.sendToCharge(id);
                                myDelegate();
                                Thread.Sleep(delay);
                            }
                            
                        }
                        break;

                    case DroneStatus.Maintenace:
                    double x = (100 - drone.battery) * 10*10;
                   // Thread.Sleep(Convert.ToInt32( x));
                        //צריך קודם שיגמור את הטעינה
                        bl.releaseFromCharge(id,true);
                        myDelegate();
                        Thread.Sleep(delay);
                        break;

                    case DroneStatus.Delivery:
                        if (drone.parcel.status == true)
                            bl.packageDelivery(id);
                        else
                            bl.packageCollection(id,true);
                        myDelegate();
                        Thread.Sleep(delay);
                        break;
                }
               
                
            }

        }
        #endregion
    }
}
