using System;
using IDAL.DO;
using IDAL.DO.DalObject;
namespace IDAL
{
    namespace DO
    { 
        namespace DalObject
        {
            public class DataSource
            {
                internal static Drone[] DronesArr = new Drone[10];
                internal static Station[] StationsArr = new Station[5];
                internal static Customer[] CustomersArr = new Customer[100];
                internal static Parcel[] ParcelArr = new Parcel[1000];

                internal class Config
                {

                    internal static int droneIndex = 0;
                    internal static int stationIndex = 0;
                    internal static int customerIndex = 0;
                    internal static int parcelIndex = 0;
                    internal static int runnerID=1000;

                }

                public static void Initialize()
                {
                    Random r = new Random();

                    //loop for updete 5 drone
                    for (int i = Config.droneIndex; i < 5; i++)
                    {
                        DronesArr[i].ID = r.Next(1111, 9999);
                        DronesArr[i].model = r.Next(1111, 9999);
                        DronesArr[i].weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        DronesArr[i].battery = r.Next(0, 100);
                        DronesArr[i].status = IDAL.DO.DroneStatus.available;
                    }


                    //loop for 2 station
                    for (int i = Config.stationIndex; i < 2; i++)
                    {
                        StationsArr[i].ID = r.Next(1111, 9999);
                        StationsArr[i].longitude = r.Next(-180, 180);
                        StationsArr[i].lattitude = r.Next(-90, 90);
                        StationsArr[i].chargeSlots = r.Next(0, 100);
                       }
                    StationsArr[Config.stationIndex++].name = "flower";
                    StationsArr[Config.stationIndex++].name = "gefen";

                    //lop for 10 customer
                    for (int i = Config.customerIndex; i < 10; i++)
                    {
                        CustomersArr[i].longitude = r.Next(-180, 180); ;
                        CustomersArr[i].lattitude = r.Next(-90, 90);
                        CustomersArr[i].ID = r.Next(212365428, 328987502);
                    }
                    CustomersArr[Config.customerIndex].phone = "0524453766";
                    CustomersArr[Config.customerIndex++].name = "Ori";
                    CustomersArr[Config.customerIndex].phone = "0538892543";
                    CustomersArr[Config.customerIndex++].name = "Tom";
                    CustomersArr[Config.customerIndex].phone = "0529165392";
                    CustomersArr[Config.customerIndex++].name = "May";
                    CustomersArr[Config.customerIndex].phone = "0524876359";
                    CustomersArr[Config.customerIndex++].name = "Noa";
                    CustomersArr[Config.customerIndex].phone = "0587363286";
                    CustomersArr[Config.customerIndex++].name = "Odeal";
                    CustomersArr[Config.customerIndex].phone = "0528362983";
                    CustomersArr[Config.customerIndex++].name = "Efrat";
                    CustomersArr[Config.customerIndex].phone = "0506376353";
                    CustomersArr[Config.customerIndex++].name = "Eliad";
                    CustomersArr[Config.customerIndex].phone = "0545273688";
                    CustomersArr[Config.customerIndex++].name = "Omer";
                    CustomersArr[Config.customerIndex].phone = "0542673566";
                    CustomersArr[Config.customerIndex++].name = "Iris";
                    CustomersArr[Config.customerIndex].phone = "0524443267";
                    CustomersArr[Config.customerIndex++].name = "Rachel";


                    for (int i = Config.parcelIndex; i < 10; i++)
                    {

                        ParcelArr[i].ID = Config.runnerID;
                        Config.runnerID++;
                        ParcelArr[i].senderID = CustomersArr[(i + 2) % 9].ID;
                        ParcelArr[i].targetId = CustomersArr[i].ID; 
                        ParcelArr[i].requested = new DateTime(2021,r.Next(1,12),r.Next(1,30));
                        ParcelArr[i].droneID = 0;
                        ParcelArr[i].scheduled = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        ParcelArr[i].pickedUp = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        ParcelArr[i].delivered = new DateTime(2021, r.Next(1, 12), r.Next(1, 30));
                        ParcelArr[i].weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                        ParcelArr[i].priority = (IDAL.DO.Priorities)(r.Next() % 3);
                    }
                    Config.parcelIndex += 10;


                }
            }
        }
    }
}

