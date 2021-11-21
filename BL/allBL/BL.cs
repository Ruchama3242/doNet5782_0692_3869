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
        }
        
       
        
    }
}
