﻿using System;
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
        IDAL.DO.DalObject.DalObject dl;

        /// <summary>
        /// constructor
        /// </summary>
        BL()
        {

           dl = new IDAL.DO.DalObject.DalObject();
        }
        List<IBL.BO.DroneToList> DroneArr;

       
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
