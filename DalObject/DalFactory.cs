﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace DAL
{


    static public class DalFactory
    {

        static public DalApi.IDal GetDal(string str)
        {
            DalObject dalObject = DalObject.Instance;
            return dalObject;
            //switch (str)
            //{
            //    case "DalObject":
            //        DO.DalObject.DalObject dalObject = DO.DalObject.DalObject.Instance;
            //        return dalObject;
            //        break;
            //    case "DALXML":
            //        break;
            //    default:
            //        break;
            //}



        }
    }
}
