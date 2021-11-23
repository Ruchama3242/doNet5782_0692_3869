using System;
using IDAL.DO;
using IDAL.DO.DalObject;
using System.Collections.Generic;
namespace IDAL
{
    namespace DO
    { 
        namespace DalObject
        {
            public class DataSource
            {
                internal static List<Drone> DronesList = new List<Drone>();
                internal static List< Station> StationsList = new List<Station>();
                internal static List< Customer> CustomersList = new List<Customer>();
                internal static List <Parcel> ParcelList = new List<Parcel>();
                internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();
                internal class Config
                {
                    internal static double chargeClear=0.001;
                   internal static double chargeLightWeight=0.005;
                   internal static double chargeHavyWeight=0.05;
                   internal static double chargeMediumWeight=0.01;
                   internal static double chargineRate=0.25;
                    internal static int runnerID=1000;

                }

                public static void Initialize()
                {
                    Random r = new Random();

                    //loop for updete 5 drone
                    for (int i = 0; i < 5; i++)
                    {
                       Drone temp= new Drone();
                        temp.ID= r.Next(1111, 9999);
                        temp.model = r.Next(1111, 9999);
                        temp.weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        //temp.battery = r.Next(0, 100);
                        //temp.status = IDAL.DO.DroneStatus.available;
                        DronesList.Add(temp);
                    }

                    //loop for 2 station
                    for (int i =0; i < 2; i++)
                    {
                        Station temp = new Station();
                        temp.ID = r.Next(1111, 9999);
                        temp.longitude = r.Next(30,34 );
                        temp.lattitude = r.Next(34, 37);
                        temp.chargeSlots = r.Next(0, 100);
                        Names namesTmp = (IDAL.DO.Names)(i + 9);//for a diffrent name
                        temp.name = namesTmp.ToString();
                        StationsList.Add(temp);
                    }

                    //lop for 10 customer
                    for (int i = 0; i < 10; i++)
                    {
                        Customer temp = new Customer();
                        Names names= (IDAL.DO.Names)(i);//for a diffrent name
                        temp.name = names.ToString();
                        temp.longitude = r.Next(30, 34);
                        temp.lattitude = r.Next(34, 37);
                        temp.ID = r.Next(212365428, 328987502);
                        temp.phone = "0"+ r.Next(521121316, 549993899);
                        CustomersList.Add(temp);
                    }

                    //loop for 10 parcels
                    for (int i =0; i < 10; i++)
                    {
                        Parcel temp = new Parcel();
                        temp.ID = Config.runnerID;
                        temp.senderID = CustomersList[(i + 2) % 9].ID;
                        temp.targetId = CustomersList[i].ID;
                        temp.requested = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.droneID = 0;
                        temp.scheduled = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.pickedUp = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.delivered = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        temp.priority = (IDAL.DO.Priorities)(r.Next() % 3);
                        ParcelList.Add(temp);
                        Config.runnerID++;
                    }
                }
            }
        }
    }
}

