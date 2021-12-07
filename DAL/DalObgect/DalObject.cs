using System;
using DO;
using DAL;
using System.Collections.Generic;
using System.Collections;

namespace DO
{
    namespace DalObject
    {
        sealed partial class DalObject : IDal
        {
            //singelton

            private static readonly object l = new object();
            internal static DalObject instance = null;
            public static DalObject Instance
            {
                get
                {
                    lock (l)
                    {
                        if (instance == null)
                        {
                            instance = new DalObject();
                        }
                        return instance;
                    }
                }
            }

            /// <summary>
            /// constructor
            /// </summary>
            DalObject() { DataSource.Initialize(); }

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

