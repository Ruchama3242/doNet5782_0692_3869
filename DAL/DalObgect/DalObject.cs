using System;
using IDAL.DO;
using DAL;
using System.Collections.Generic;
using System.Collections;
namespace IDAL

{
  namespace DO
  { 
      namespace DalObject
        {
             public partial class DalObject:IDal
            {
                /// <summary>
                /// constructor
                /// </summary>
                public DalObject() { DataSource.Initialize(); }
                public double[] chargeCapacity()
                {
                    double[] arr = new double[] { DataSource.Config.chargeClear, DataSource.Config.chargeLightWeight, DataSource.Config.chargeMediumWeight, DataSource.Config.chargeHavyWeight, DataSource.Config.chargineRate };
                    return arr;
                }

            }
        }
    }

}

