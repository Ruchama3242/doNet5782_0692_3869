using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;

namespace BL
{
   partial class BL
    {
        Random rnd = new Random();
      private  IDAL.DO.Drone droneDal(int id)
        {

        }
        /// <summary>
        /// adding a drone to droneList
        /// </summary>
        /// <param name="drone"></param>
        public void addDrone(int id,int model,int weight,int stationId)
        {
            var flag = DroneArr.Find(p => p.ID == id);
            if (flag != null)
                throw new BLIdExistsException("the drone is alrady exists");
            else
            {

                IBL.BO.DroneToList d = new IBL.BO.DroneToList();
                d.ID = id;
                //d.parcelNumber = drone.parcel.ID;
                d.droneModel = model;
                d.weight = (IBL.BO.WeightCategories)weight;
                d.battery = rnd.Next(20, 40);
                d.status = IBL.BO.DroneStatus.maintenace;
                var s = dl.printStation(stationId);
                   
            }
            
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
    }
}
