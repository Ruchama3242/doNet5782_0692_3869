using System;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
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

    }
}

