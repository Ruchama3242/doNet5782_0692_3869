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
             public partial class DalObject:interfaceIDal
            {
                /// <summary>
                /// constructor
                /// </summary>
                public DalObject() { DataSource.Initialize(); }

                /// <summary>
                /// reurn array of five values ​​in the following order: free, light, medium, heavy and load rate
                /// </summary>
                /// <returns></returns>
                public double[] chargeCapacity()
                {
                    double[] arr = new double[] { DataSource.Config.chargeClear, 
                                                  DataSource.Config.chargeLightWeight, 
                                                  DataSource.Config.chargeMediumWeight, 
                                                  DataSource.Config.chargeHavyWeight, 
                                                  DataSource.Config.chargineRate };
                    return arr;
                }

            }
        }
    }

}

