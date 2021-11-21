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
    class BL
    {
        /// <summary>
        /// constructor
        /// </summary>
        BL()
        {
            
            
        }
        List<IBL.BO.DroneToList> DroneArr;

        /// <summary>
        /// adding a drone to droneList
        /// </summary>
        /// <param name="drone"></param>
        public void addDrone(IBL.BO.Drone drone)
        {
            var flug = DroneArr.Find(p => p.ID ==drone.ID);
            if (flug != null)
                throw new IdExistsException("the drone is alrady exists");
            else
            {
                
                IBL.BO.DroneToList d = new IBL.BO.DroneToList();
                d.ID = drone.ID;
                d.parcelNumber = drone.parcel.ID;
                d.droneModel = drone.model;
                d.weight = drone.weight;
                d.battery = drone.battery;
                d.status = drone.status;
                d.currentLocation = drone.location;
            }
                throw new IdUnExistsException("the ID don't mutch to any drone");
        }

        /// <summary>
        /// update the name of the drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void updateNameDrone(int ID, int model)
        {
            var flug = DroneArr.Find(p => p.ID == ID);
            if (flug != null)
                flug.droneModel = model;
            else
                throw new IdUnExistsException("the ID don't mutch to any drone");

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
