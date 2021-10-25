using System;


namespace DalObject
{
    public class DataSource
    {
        Drone[] static DronesArr = new IDAL.DO.Drone[10];
               Station[] static StationsArr = new IDAL.DO.Station[5];
               Customer[] static CustomersArr = new IDAL.DO.Customer[100];
               Parcel[] static ParcelArr = new IDAL.DO.Parcel[1000];

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
            for (int i = DroneIndex; i < 5; i++)
            {
                DronesArr[i].id = r.Next(1111, 9999);
                DronesArr[i].model = r.Next(1111, 9999);
                DronesArr[i].weight = r.Next() % 3;
                DronesArr[i].battery = r.Next() % 3;
                DronesArr[i].status = r.Next() % 3;
            }


            //lop for 2 station
            for (int i = stationIndex, string str = 'A' ; i < 2; i++,str++)
			            {
                StationArr[i].id = r.Next(1111, 9999);
                StationArr[i].name = str;
                StationArr[i].longitude = r.Next(-180, 180);
                StationArr[i].lattitude = r.Next(-90, 90);
                StationArr[i].chargeSlots = r.Next(0, 100); ;
            }

            //lop for 10 customer
            for (int i = customerIndex; i < 10; i++)
            {
                CustomerArr[i].longitude = r.Next(-180, 180); ;
                CustomerArr[i].lattitude = r.Next(-90, 90);
                CustomerArr[i].id = r.Next(212365428, 328987502);
                CustomerArr[i].phone = r.Next(531325422, 587721219);
            }

            CustomerArr[customerIndex++].name = Ori;
            CustomerArr[customerIndex++].name = Tom;
            CustomerArr[customerIndex++].name = May;
            CustomerArr[customerIndex++].name = Noa;
            CustomerArr[customerIndex++].name = Odael;
            CustomerArr[customerIndex++].name = Efrat;
            CustomerArr[customerIndex++].name = Eliad;
            CustomerArr[customerIndex++].name = Omer;
            CustomerArr[customerIndex++].name = Iris;
            CustomerArr[customerIndex++].name = Rachel;


            for (int i = parcelIndex; i < 10; i++)
            {
                PercalArr[i].id = r.Next(1111, 9999);
                PercalArr[i].senderid = r.Next(1111, 9999);
                PercalArr[i].targetId = r.Next(111, 999);
                DateTime currentDate = DateTime.Now;
                PercalArr[i].requested = currentDate + 10 * i;
                PercalArr[i].drineld = 0;
                PercalArr[i].scheduled = currentDate + 20 * i;
                PercalArr[i].pickedUp = currentDate + 30 * i;
                PercalArr[i].delivered = currentDate + 40 * i;
                PercalArr[i].weight = r.Next() % 3;
                PercalArr[i].priority = r.Next() % 3;
            }
        }
    }
}
