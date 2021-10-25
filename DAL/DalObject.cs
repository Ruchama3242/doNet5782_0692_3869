using System;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        /// <summary>
        /// constructor
        /// </summary>
        public DalObject() { DataSource.Initialize(); }
        /// <summary>
        /// add station to the array
        /// </summary>
        /// <param name="Victoria"></param>
        public void addStations(Station Victoria)
        {
            if (DataSource.Config.stationIndex <= 4)
            {
                DataSource.StationsArr[DataSource.Config.stationIndex] = Victoria;
                DataSource.Config.stationIndex++;
            }
            else
                Console.WriteLine("ERROR! overflow in array");
        }
        /// <summary>
        /// add drone to the array
        /// </summary>
        /// <param name="FlyBoy"></param>
        public void addDrone(Drone FlyBoy)
        {
            if (DataSource.Config.droneIndex <= 9)
            {
                DataSource.DronesArr[DataSource.Config.droneIndex] = FlyBoy;
                DataSource.Config.droneIndex++;
            }
            else
                Console.WriteLine("ERROR! overflow in array");
        }
        /// <summary>
        /// add customer to the array
        /// </summary>
        /// <param name="c"></param>
        public void addCustomer(Customer c)
        {
            if (DataSource.Config.customerIndex <= 100)
            {
                DataSource.CustomersArr[DataSource.Config.stationIndex] = c;
                DataSource.Config.customerIndex++;
            }
            else
                Console.WriteLine("ERROR! overflow in array");
        }
        /// <summary>
        /// add parcel to the array
        /// </summary>
        /// <param name="Fedex"></param>
        public void addParcel(Parcel Fedex)
        {
            if (DataSource.Config.parcelIndex <= 1000)
            {
                DataSource.ParcelArr[DataSource.Config.parcelIndex] = Fedex;
                DataSource.Config.parcelIndex++;
            }
            else
                Console.WriteLine("ERROR! overflow in array");
        }
        /// <summary>
        /// update the DroneId to the parcel
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="droneID"></param>
        public void ParcelDrone(int parcelID, int droneID)
        {
            int size = DataSource.Config.parcelIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.ParcelArr[i].ID == parcelID)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
                Console.WriteLine("ERROR! parcel not found");
            else
            {
                DataSource.ParcelArr[index].droneld = droneID;
            }
        }
        /// <summary>
        /// update the date that the parcel picked up
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="day"></param>
        public void ParcelPickedUp(int parcelID, DateTime day)
        {
            int size = DataSource.Config.parcelIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.ParcelArr[i].ID == parcelID)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
                Console.WriteLine("ERROR! parcel not found");
            else
            {
                DataSource.ParcelArr[index].pickedUp = day;
            }
        }
        /// <summary>
        /// update the date that the parcel delivered
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="day"></param>
        public void ParcelReceived(int parcelID, DateTime day)
        {
            int size = DataSource.Config.parcelIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.ParcelArr[i].ID == parcelID)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
                Console.WriteLine("ERROR! parcel not found");
            else
            {
                DataSource.ParcelArr[index].delivered = day;
            }
        }
        /// <summary>
        /// send the drone to charge in a station
        /// </summary>
        /// <param name="DroneID"></param>
        /// <param name="StationID"></param>
        /// <returns></returns>
        public DroneCharge SendToCharge(int DroneID, int StationID)
        {
            int size = DataSource.Config.droneIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.DronesArr[i].ID == DroneID)
                {
                    index = i;
                    break;
                }
            }
            DroneCharge DC = new DroneCharge();
            if (index == -1)
            {
                Console.WriteLine("ERROR! parcel not found");
                return DC;
            }
            DataSource.DronesArr[index].status = (IDAL.DO.DroneStatus)1;
            DC.droneID = DroneID;
            DC.stationeld = StationID;
            return DC;
        }

        public void BatteryCharged(DroneCharge FuzzedUp)
        {
        }

        /// <summary>
        /// print a station
        /// </summary>
        /// <param name="id"></param>
        public void printStation(int id)
        {
            Station s;
            int size = DataSource.Config.stationIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.StationsArr[i].ID == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                Console.WriteLine("ERROR! station not found");
                return;
            }
            s = DataSource.StationsArr[index];
              Console.WriteLine(s);
        }
        /// <summary>
        /// print a drone
        /// </summary>
        /// <param name="id"></param>
        public void printDrone(int id)
        {
            Drone d;
            int size = DataSource.Config.droneIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.DronesArr[i].ID == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                Console.WriteLine("ERROR! Drone not found");
                return;
            }
            d = DataSource.DronesArr[index];
            Console.WriteLine(d);
        }
        /// <summary>
        /// print a customer
        /// </summary>
        /// <param name="id"></param>
        public void printCustomer(int id)
        {
            Customer c;
            int size = DataSource.Config.customerIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.CustomersArr[i].ID == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                Console.WriteLine("ERROR! customer not found");
                return;
            }
            c = DataSource.CustomersArr[index];
            Console.WriteLine(c);
        }
        /// <summary>
        /// print a parcel
        /// </summary>
        /// <param name="id"></param>
        public void printParcel(int id)
        {
            Parcel p;
            int size = DataSource.Config.parcelIndex;
            int index = -1;
            for (int i = 0; i <= size; i++)
            {
                if (DataSource.ParcelArr[i].ID == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                Console.WriteLine("ERROR! parcel not found");
                return;
            }
            p = DataSource.ParcelArr[index];
            Console.WriteLine(p);
        }

        /// <summary>
        /// print all stations
        /// </summary>
        public void printAllStations()
        {
            for(int i=0;i<DataSource.Config.stationIndex;i++)
            {
                Station s = DataSource.StationsArr[i];
                Console.WriteLine(s);
            }
        }
        /// <summary>
        /// print all drones
        /// </summary>
        public void printAllDrones()
        {
            for (int i = 0; i < DataSource.Config.droneIndex; i++)
            {
                Drone d = DataSource.DronesArr[i];
                Console.WriteLine(d);
            }
        }
        /// <summary>
        /// print all customers
        /// </summary>
        public void printAllCustomers()
        {
            for (int i = 0; i < DataSource.Config.customerIndex; i++)
            {
                Customer c = DataSource.CustomersArr[i];
                Console.WriteLine(c);
            }
        }
        /// <summary>
        /// print all parcels
        /// </summary>
        public void printAllParcels()
        {
            for (int i = 0; i < DataSource.Config.parcelIndex; i++)
            {
                Parcel p = DataSource.ParcelArr[i];
                Console.WriteLine(p);
            }
        }
        /// <summary>
        /// print all parcels that have no yet drone
        /// </summary>
        public void printParcelsWithoutDrone()
        {
            for(int i=0;i<DataSource.Config.parcelIndex;i++)
            {
                Parcel p = DataSource.ParcelArr[i];
                if(p.droneld==0)
                    Console.WriteLine(p);
            }
        }
        /// <summary>
        /// print all stations with charge slots available
        /// </summary>
        public void printStationsWithChargeSlots()
        {
            for(int i=0;i<DataSource.Config.stationIndex;i++)
            {
                Station s = DataSource.StationsArr[i];
                if(s.chargeSlots>0)
                    Console.WriteLine(s);
            }
        }

    }
}

