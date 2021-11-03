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
                internal static List<Drone> DronesArr = new List<Drone>();
                internal static List< Station> StationsArr = new List<Station>();
                internal static List< Customer> CustomersArr = new List<Customer>();
                internal static List <Parcel> ParcelArr = new List<Parcel>();
                internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();
                // internal static Drone[] DronesArr = new Drone[10];
                //internal static Station[] StationsArr = new Station[5];
                //internal static Customer[] CustomersArr = new Customer[100];
                //internal static Parcel[] ParcelArr = new Parcel[1000];
               
                internal class Config
                {

                    //internal static int droneIndex = 0;
                    //internal static int stationIndex = 0;
                    //internal static int customerIndex = 0;
                    //internal static int parcelIndex = 0;
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
                        DronesArr.Add(temp);
                        //DronesArr[i].ID = r.Next(1111, 9999);
                        //DronesArr[i].model = r.Next(1111, 9999);
                        //DronesArr[i].weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        //DronesArr[i].battery = r.Next(0, 100);
                        //DronesArr[i].status = IDAL.DO.DroneStatus.available;
                        //Config.droneIndex++;
                    }


                    //loop for 2 station
                    for (int i =0; i < 2; i++)
                    {
                        Station temp = new Station();
                        temp.ID = r.Next(1111, 9999);
                        temp.longitude = r.Next(-180, 180);
                        temp.lattitude = r.Next(-90, 90);
                        temp.chargeSlots = r.Next(0, 100);
                        Names namesTmp = (IDAL.DO.Names)(i + 9);//for a diffrent name
                        string.Format(temp.name, namesTmp);
                        StationsArr.Add(temp);
                        //    StationsArr[i].ID = r.Next(1111, 9999);
                        //    StationsArr[i].longitude = r.Next(-180, 180);
                        //    StationsArr[i].lattitude = r.Next(-90, 90);
                        //    StationsArr[i].chargeSlots = r.Next(0, 100);
                    }
                    //StationsArr[Config.stationIndex++].name = "flower";
                    //StationsArr[Config.stationIndex++].name = "gefen";

                    //lop for 10 customer
                    for (int i = 0; i < 10; i++)
                    {
                        Customer temp = new Customer();
                        Names names= (IDAL.DO.Names)(i);//for a diffrent name
                        string.Format(temp.name,names);
                        temp.longitude= r.Next(-180, 180);
                        temp.lattitude = r.Next(-90, 90);
                        temp.ID = r.Next(212365428, 328987502);
                        temp.phone = "0"+ r.Next(521121316, 549993899);
                        CustomersArr.Add(temp);
                        //CustomersArr[i].longitude = r.Next(-180, 180); ;
                        //CustomersArr[i].lattitude = r.Next(-90, 90);
                        //CustomersArr[i].ID = r.Next(212365428, 328987502);
                    }
                    //CustomersArr[Config.customerIndex].phone = "0524453766";
                    //CustomersArr[Config.customerIndex++].name = "Ori";
                    //CustomersArr[Config.customerIndex].phone = "0538892543";
                    //CustomersArr[Config.customerIndex++].name = "Tom";
                    //CustomersArr[Config.customerIndex].phone = "0529165392";
                    //CustomersArr[Config.customerIndex++].name = "May";
                    //CustomersArr[Config.customerIndex].phone = "0524876359";
                    //CustomersArr[Config.customerIndex++].name = "Noa";
                    //CustomersArr[Config.customerIndex].phone = "0587363286";
                    //CustomersArr[Config.customerIndex++].name = "Odeal";
                    //CustomersArr[Config.customerIndex].phone = "0528362983";
                    //CustomersArr[Config.customerIndex++].name = "Efrat";
                    //CustomersArr[Config.customerIndex].phone = "0506376353";
                    //CustomersArr[Config.customerIndex++].name = "Eliad";
                    //CustomersArr[Config.customerIndex].phone = "0545273688";
                    //CustomersArr[Config.customerIndex++].name = "Omer";
                    //CustomersArr[Config.customerIndex].phone = "0542673566";
                    //CustomersArr[Config.customerIndex++].name = "Iris";
                    //CustomersArr[Config.customerIndex].phone = "0524443267";
                    //CustomersArr[Config.customerIndex++].name = "Rachel";

                    //loop for 10 parcels
                    for (int i =0; i < 10; i++)
                    {
                        Parcel temp = new Parcel();
                        temp.ID = Config.runnerID;
                        temp.senderID = CustomersArr[(i + 2) % 9].ID;
                        temp.targetId = CustomersArr[i].ID;
                        temp.requested = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.droneID = 0;
                        temp.scheduled = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.pickedUp = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.delivered = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        temp.weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        temp.priority = (IDAL.DO.Priorities)(r.Next() % 3);
                        ParcelArr.Add(temp);
                        //ParcelArr[i].ID = Config.runnerID;
                        Config.runnerID++;
                        //ParcelArr[i].senderID = CustomersArr[(i + 2) % 9].ID;
                        //ParcelArr[i].targetId = CustomersArr[i].ID; 
                        //ParcelArr[i].requested = new DateTime(2021,r.Next(1,12),r.Next(1,30));
                        //ParcelArr[i].droneID = 0;
                        //ParcelArr[i].scheduled = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        //ParcelArr[i].pickedUp = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        //ParcelArr[i].delivered = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        //ParcelArr[i].weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        //ParcelArr[i].priority = (IDAL.DO.Priorities)(r.Next() % 3);
                    }
                   // Config.parcelIndex += 10;


                }
            }
        }
    }
}

