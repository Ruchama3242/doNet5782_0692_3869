using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL.BO;
namespace BL
{
    class BL
    {
       
        List<IBL.BO.DroneToList> DroneArr;
        public void updateNameDrone(int ID, int model)
        {
            var flug = DroneArr.Find(p => p.ID == ID);
            if (flug != null)
                flug.droneModel = model;
            else 
                throw new IdUnExistsException("the ID don't mutch to any drone");
        }

        public void updateStation(int Id ,string name, int emptyChargeSlot)
        {
            
        }
    }
}
