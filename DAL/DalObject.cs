using System;
using IDAL.DO;
using DAL;
namespace IDAL
{
    namespace DO
    {

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
                public  static void addStations(Station temp)
                {
                    if (DataSource.Config.stationIndex <= 4)
                    {
                        DataSource.StationsArr[DataSource.Config.stationIndex] = temp;
                        DataSource.Config.stationIndex++;
                    }
                    else
                        Console.WriteLine("ERROR! overflow in array");
                }
                /// <summary>
                /// add drone to the array
                /// </summary>
                /// <param name="spy"></param>
                public static void addDrone(Drone spy)
                {
                    if (DataSource.Config.droneIndex <= 9)
                    {
                        DataSource.DronesArr[DataSource.Config.droneIndex] = spy;
                        DataSource.Config.droneIndex++;
                    }
                    else
                        Console.WriteLine("ERROR! overflow in array");
                }
                /// <summary>
                /// add customer to the array
                /// </summary>
                /// <param name="c"></param>
                public static void addCustomer(Customer anonimos)
                {
                    if (DataSource.Config.customerIndex <100)
                    {
                        DataSource.CustomersArr[DataSource.Config.stationIndex] = anonimos;
                        DataSource.Config.customerIndex++;
                    }
                    else
                        Console.WriteLine("ERROR! overflow in array");
                }
                /// <summary>
                /// add parcel to the array
                /// </summary>
                /// <param name="Fedex"></param>
                public static void addParcel(Parcel wow)
                {
                    if (DataSource.Config.parcelIndex < 1000)
                    {
                        wow.ID = DataSource.Config.runnerID;
                        DataSource.Config.runnerID++;
                        DataSource.ParcelArr[DataSource.Config.parcelIndex] = wow;
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
                public static void ParcelDrone(int parcelID, int droneID)
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
                        DataSource.ParcelArr[index].droneID = droneID;
                    }
                }
                /// <summary>
                /// update the date that the parcel picked up
                /// </summary>
                /// <param name="parcelID"></param>
                /// <param name="day"></param>
                public static void ParcelPickedUp(int parcelID, DateTime day)
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
                public  static void ParcelReceived(int parcelID, DateTime day)
                {
                    int size = DataSource.Config.parcelIndex;
                    int index = -1;
                    for (int i = 0; i <=size; i++)
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
                public  static DroneCharge SendToCharge(int DroneID, int StationID)
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
                        Console.WriteLine("ERROR! drone not found");
                        return DC;
                    }
                    DataSource.DronesArr[index].status = (IDAL.DO.DroneStatus)1;
                    DC.droneID = DroneID;
                    DC.stationeld = StationID;
                    int index2 = 0;
                    for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    {
                        if (DataSource.StationsArr[i].ID == StationID)
                        {
                            index2 = i;
                            break;
                        }
                    }
                    DataSource.StationsArr[index2].chargeSlots = DataSource.StationsArr[index2].chargeSlots - 1;
                    return DC;
                }
                /// <summary>
                /// release the drone from the charge slote
                /// </summary>
                /// <param name="FuzzedUp"></param>
                public static void BatteryCharged(DroneCharge FuzzedUp)
                {
                    int size = DataSource.Config.droneIndex;
                    int index = -1;
                    for (int i = 0; i <= size; i++)
                    {
                        if (DataSource.DronesArr[i].ID == FuzzedUp.droneID)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        Console.WriteLine("ERROR! drone not found");
                    }
                    DataSource.DronesArr[index].status =(IDAL.DO.DroneStatus)0;
                    int index2 = 0;
                    for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    {
                        if (DataSource.StationsArr[i].ID == FuzzedUp.stationeld)
                        {
                            index2 = i;
                            break;
                        }
                    }
                    DataSource.StationsArr[index2].chargeSlots = DataSource.StationsArr[index2].chargeSlots + 1;

                }

                /// <summary>
                /// print a station
                /// </summary>
                /// <param name="id"></param>
                public static Station printStation(int id)
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
                    }
                    s = DataSource.StationsArr[index];
                    return s;
                }
                /// <summary>
                /// print a drone
                /// </summary>
                /// <param name="id"></param>
                public static Drone printDrone(int id)
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
                    }
                    d = DataSource.DronesArr[index];
                    return d;
                }
                /// <summary>
                /// print a customer
                /// </summary>
                /// <param name="id"></param>
                public static Customer printCustomer(int id)
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
                    }
                    c = DataSource.CustomersArr[index];
                    return c;
                }
                /// <summary>
                /// print a parcel
                /// </summary>
                /// <param name="id"></param>
                public static Parcel printParcel(int id)
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
                    }
                    p = DataSource.ParcelArr[index];
                    return p;
                }

                /// <summary>
                /// print all stations
                /// </summary>
                public static Station[] printAllStations()
                {
                    Station[] arr = new Station[DataSource.Config.stationIndex];
                    for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    {
                        arr[i] = DataSource.StationsArr[i];
                    }
                    return arr;
                }
                /// <summary>
                /// print all drones
                /// </summary>
                public static Drone[] printAllDrones()
                {
                    Drone[] arr = new Drone[DataSource.Config.droneIndex];
                    for (int i = 0; i < DataSource.Config.droneIndex; i++)
                    {
                        arr[i] = DataSource.DronesArr[i];
                    }
                    return arr;
                }
                /// <summary>
                /// print all customers
                /// </summary>
                public static Customer[] printAllCustomers()
                {
                    Customer[] arr = new Customer[DataSource.Config.customerIndex];
                    for (int i = 0; i < DataSource.Config.customerIndex; i++)
                    {
                        arr[i] = DataSource.CustomersArr[i];
                    }
                    return arr;
                }
                /// <summary>
                /// print all parcels
                /// </summary>
                public static Parcel[] printAllParcels()
                {
                    Parcel[] arr = new Parcel[DataSource.Config.parcelIndex];
                    for (int i = 0; i < DataSource.Config.parcelIndex; i++)
                    {
                        arr[i] = DataSource.ParcelArr[i];
                    }
                    return arr;
                }
                /// <summary>
                /// print all parcels that have no yet drone
                /// </summary>
                public static Parcel[] printParcelsWithoutDrone()
                {
                    int count = 0;
                    for (int i = 0; i < DataSource.Config.parcelIndex; i++)
                    {
                        //Parcel p = DataSource.ParcelArr[i];
                        if (DataSource.ParcelArr[i].droneID == 0)
                            count++;
                    }
                    Parcel[] arr = new Parcel[count];
                    count = 0;
                    for (int i = 0; i < DataSource.Config.parcelIndex; i++)
                    {
                        if (DataSource.ParcelArr[i].droneID == 0)
                        {
                            arr[count] = DataSource.ParcelArr[i];
                            count++;
                        }
                    }
                    return arr;
                }
                /// <summary>
                /// print all stations with charge slots available
                /// </summary>
                public static Station[] printStationsWithChargeSlots()
                {
                    int count = 0;
                    for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    {
                        if (DataSource.StationsArr[i].chargeSlots > 0)
                            count++;
                    }
                    Station[] arr = new Station[count];
                    count = 0;
                    for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    {
                        if (DataSource.StationsArr[i].chargeSlots > 0)
                        {
                            arr[count] = DataSource.StationsArr[i];
                            count++;
                        }
                    }
                    return arr;
                }

            }
        }
    }

}

