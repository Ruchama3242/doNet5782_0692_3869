using System;
using IDAL.DO;
using DalObject;


    public class DataSource
    {
             internal static Drone[]  DronesArr = new Drone[10];
             internal static Station[]  StationsArr = new Station[5];
             internal  static Customer[]  CustomersArr = new Customer[100];
             internal static Parcel[]  ParcelArr = new Parcel[1000];

        internal class Config
        {

            internal static int droneIndex = 0;
            internal static int stationIndex = 0;
            internal static int customerIndex = 0;
            internal static int parcelIndex = 0;
            public int runnerID;

        }

            public static void Initialize()
            {
                Random r = new Random();

                //lop for updete 5 drone
                for (int i = Config.droneIndex; i < 5; i++)
                {
                    DronesArr[i].ID = r.Next(1111, 9999);
                    DronesArr[i].model = r.Next(1111, 9999);
                    DronesArr[i].weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                    DronesArr[i].battery = r.Next(0,100);
                    DronesArr[i].status = (IDAL.DO.DroneStatus)(r.Next() % 3);
                }


                //lop for 2 station
                //string str = "A";
                for (int i = Config.stationIndex; i < 2; i++)
                {
                    StationsArr[i].ID = r.Next(1111, 9999);
                   // StationsArr[i].name = str;
                    StationsArr[i].longitude = r.Next(-180, 180);
                    StationsArr[i].lattitude = r.Next(-90, 90);
                    StationsArr[i].chargeSlots = r.Next(0, 100);
                    //str++;
                }

                //lop for 10 customer
                for (int i = Config.customerIndex; i < 10; i++)
                {
                    CustomersArr[i].longitude = r.Next(-180, 180); ;
                    CustomersArr[i].lattitude = r.Next(-90, 90);
                    CustomersArr[i].ID = r.Next(212365428, 328987502);
                    //add a phone number CustomersArr[i].phone = r.Next(531325422, 587721219);
                }

                CustomersArr[Config.customerIndex++].name = "Ori";
                CustomersArr[Config.customerIndex++].name = "Tom";
                CustomersArr[Config.customerIndex++].name = "May";
                CustomersArr[Config.customerIndex++].name = "Noa";
                CustomersArr[Config.customerIndex++].name = "Odeal";
                CustomersArr[Config.customerIndex++].name = "Efrat";
                CustomersArr[Config.customerIndex++].name = "Eliad";
                CustomersArr[Config.customerIndex++].name = "Omer";
                CustomersArr[Config.customerIndex++].name = "Iris";
                CustomersArr[Config.customerIndex++].name = "Rachel";


                for (int i = Config.parcelIndex; i < 10; i++)
                {
                    ParcelArr[i].ID = r.Next(1111, 9999);
                ParcelArr[i].senderid = r.Next(1111, 9999);
                ParcelArr[i].targetId = r.Next(111, 999);
                    DateTime currentDate = DateTime.Now;
               // ParcelArr[i].requested = currentDate + 10 * i;
                ParcelArr[i].drineld = 0;
              //  ParcelArr[i].scheduled = currentDate + 20 * i;
               // ParcelArr[i].pickedUp = currentDate + 30 * i;
              //  ParcelArr[i].delivered = currentDate + 40 * i;
                ParcelArr[i].weight = (IDAL.DO.WeightCategories)(r.Next() % 3);
                ParcelArr[i].priority = (IDAL.DO.Priorities)(r.Next() %3);
                }
            
        }
    }

