using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
namespace BL
{
    class BL
    {
        BL()
        {
            
        }
        List<IBL.BO.DroneToList> DroneArr;
        public void updateNameDrone(int ID, int model)
        {
            var flug = DroneArr.Find(p => p.ID == ID);
            if (flug != null)
                flug.droneModel = model;
            else 
                throw new IdUnExistsException("the ID don't match to any drone");
            
        }

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
        }
    }
}
