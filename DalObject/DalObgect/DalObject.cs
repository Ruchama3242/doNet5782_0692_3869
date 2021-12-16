using System;
using DalApi;

namespace Dal
{
    sealed partial class DalObject : DalApi.IDal
    {
            //singelton

            static readonly DalObject instance = new DalObject();
            internal static DalObject Instance { get { return instance; } }

            /// <summary>
            /// constructor
            /// </summary>
            DalObject() { DAL.DataSource.Initialize(); }

            /// <summary>
            /// reurn array of five values ​​in the following order: free, light, medium, heavy and load rate
            /// </summary>
            /// <returns></returns>
            public double[] chargeCapacity()
            {
                double[] arr = new double[] { DAL.DataSource.Config.chargeClear,
                                                  DAL.DataSource.Config.chargeLightWeight,
                                                  DAL.DataSource.Config.chargeMediumWeight,
                                                  DAL.DataSource.Config.chargeHavyWeight,
                                                  DAL.DataSource.Config.chargineRate };
                return arr;
            }

        }
    }

