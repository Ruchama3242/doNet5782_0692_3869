﻿using System;
using IDAL.DO;
using DAL;
using System.Collections.Generic;
using System.Collections;
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

                public Station searchStation(int id)
                {
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.ID == id)
                            return item;
                    }
                    throw new Exception("ERROR! the station doesn't exist");
                }

                public Drone searchDrone(int id)
                {
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if (item.ID == id)
                            return item;
                    }
                    throw new Exception("ERROR! the drone doesn't exist");
                }

                public Parcel searchParcel(int id)
                {
                    foreach (Parcel item in DataSource.ParcelList)
                    {
                        if (item.ID == id)
                            return item;
                    }
                    throw new Exception("ERROR! the parcel doesn't exist");
                }

                public Customer searchCustomer(int id)
                {
                    foreach (Customer item in DataSource.CustomersList)
                    {
                        if (item.ID == id)
                            return item;
                    }
                    throw new Exception("ERROR! the customer doesn't exist");
                }
                /// <summary>
                /// add station to the array
                /// </summary>
                /// <param name="Victoria"></param>
                public void addStations(Station temp)
                {
                    //if (DataSource.Config.stationIndex <= 4)
                    //{
                    bool flug = true;
                    foreach (Station item in DataSource.StationsList)
                    {

                        if (item.Equals(temp.ID)) //return true if the field is the same
                            flug = false;
                    }
                    if (flug)
                        DataSource.StationsList.Add(temp);
                    else
                        throw new Exception("ERROR! the ID already exist");
                    //DataSource.StationsArr[DataSource.Config.stationIndex] = temp;
                    //DataSource.Config.stationIndex++;
                    //}
                    //else
                    //Console.WriteLine("ERROR! overflow in array");
                }
                /// <summary>
                /// add drone to the array
                /// </summary>
                /// <param name="spy"></param>
                public void addDrone(Drone temp)
                {
                    bool flag = true;
                    foreach (Drone item in DataSource.DronesList)
                    {

                        if (item.Equals(temp.ID)) //return true if the field is the same
                            flag = false;
                    }
                    if (flag)
                        DataSource.DronesList.Add(temp);
                    else
                        throw new Exception("ERROR! the ID already exist");
                    //if (DataSource.Config.droneIndex <= 9)
                    //{
                    //    DataSource.DronesArr[DataSource.Config.droneIndex] = spy;
                    //    DataSource.Config.droneIndex++;
                    //}
                    //else
                    //    Console.WriteLine("ERROR! overflow in array");
                }
                /// <summary>
                /// add customer to the array
                /// </summary>
                /// <param name="c"></param>
                public void addCustomer(Customer temp)
                {
                    bool flag = true;
                    foreach (Customer item in DataSource.CustomersList)
                    {

                        if (item.Equals(temp.ID)) //return true if the field is the same
                            flag = false;
                    }
                    if (flag)
                        DataSource.CustomersList.Add(temp);
                    else
                        throw new Exception("ERROR! the ID already exist");
                    //if (DataSource.Config.customerIndex <100)
                    //{
                    //    DataSource.CustomersArr[DataSource.Config.customerIndex] = anonimos;
                    //    DataSource.Config.customerIndex++;
                    //}
                    //else
                    //    Console.WriteLine("ERROR! overflow in array");
                }
                /// <summary>
                /// add parcel to the array
                /// </summary>
                /// <param name="Fedex"></param>
                public int addParcel(Parcel temp)
                {
                    temp.ID = DataSource.Config.runnerID;
                    DataSource.ParcelList.Add(temp);
                    DataSource.Config.runnerID++;
                    return temp.ID;
                    //if (DataSource.Config.parcelIndex < 1000)
                    //{
                    //    wow.ID = DataSource.Config.runnerID;
                    //    DataSource.Config.runnerID++;
                    //    DataSource.ParcelArr[DataSource.Config.parcelIndex] = wow;
                    //    DataSource.Config.parcelIndex++;

                    //}
                    //else
                    //    Console.WriteLine("ERROR! overflow in array");
                    //return wow.ID;
                }
                /// <summary>
                /// update the DroneId to the parcel
                /// </summary>
                /// <param name="parcelID"></param>
                /// <param name="droneID"></param>
                public void ParcelDrone(int parcelID, int droneID)
                {
                    for (int i = 0; i < DataSource.ParcelList.Count; i++)
                    {
                        if (DataSource.ParcelList[i].ID == parcelID)
                        {
                            Parcel p = DataSource.ParcelList[i];
                            p.droneID = droneID;
                            DataSource.ParcelList[i] = p;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the value not found");
                    //    int size = DataSource.Config.parcelIndex;
                    //    int index = -1;
                    //    for (int i = 0; i <= size; i++)
                    //    {
                    //        if (DataSource.ParcelArr[i].ID == parcelID)
                    //        {
                    //            index = i;
                    //            break;
                    //        }
                    //    }
                    //    if (index == -1)
                    //        Console.WriteLine("ERROR! parcel not found");
                    //    else
                    //    {
                    //        DataSource.ParcelArr[index].droneID = droneID;
                    //    }
                }
                /// <summary>
                /// update the date that the parcel picked up
                /// </summary>
                /// <param name="parcelID"></param>
                /// <param name="day"></param>
                public void ParcelPickedUp(int parcelID, DateTime day)
                {
                    for (int i = 0; i < DataSource.ParcelList.Count; i++)
                    {
                        if (DataSource.ParcelList[i].ID == parcelID)
                        {
                            Parcel p = DataSource.ParcelList[i];
                            p.pickedUp = day;
                            DataSource.ParcelList[i] = p;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the value not found");

                    //    int size = DataSource.Config.parcelIndex;
                    //    int index = -1;
                    //    for (int i = 0; i <= size; i++)
                    //    {
                    //        if (DataSource.ParcelArr[i].ID == parcelID)
                    //        {
                    //            index = i;
                    //            break;
                    //        }
                    //    }
                    //    if (index == -1)
                    //        Console.WriteLine("ERROR! parcel not found");
                    //    else
                    //{

                    //    int ind,j;
                    //    int droneid = DataSource.ParcelArr[index].droneID;
                    //    int size2 = DataSource.Config.droneIndex;
                    //    for ( j = 0; j <= size2; j++)
                    //    {
                    //        if (DataSource.DronesArr[j].ID == droneid)
                    //        {
                    //            ind = j;
                    //            break;
                    //        }
                    //    }
                    //    DataSource.DronesArr[j].status = IDAL.DO.DroneStatus.delivery;
                    //    DataSource.ParcelArr[index].pickedUp = day;
                    //    }
                }
                /// <summary>
                /// update the date that the parcel delivered
                /// </summary>
                /// <param name="parcelID"></param>
                /// <param name="day"></param>
                public void ParcelReceived(int parcelID, DateTime day)
                {
                    for (int i = 0; i < DataSource.ParcelList.Count; i++)
                    {
                        if (DataSource.ParcelList[i].ID == parcelID)
                        {
                            Parcel p = DataSource.ParcelList[i];
                            p.delivered = day;
                            DataSource.ParcelList[i] = p;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the value not found");

                    //int size = DataSource.Config.parcelIndex;
                    //int index = -1;
                    //for (int i = 0; i <=size; i++)
                    //{
                    //    if (DataSource.ParcelArr[i].ID == parcelID)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index == -1)
                    //    Console.WriteLine("ERROR! parcel not found");
                    //else
                    //{
                    //int ind, j;
                    //int droneid = DataSource.ParcelArr[index].droneID;
                    //int size2 = DataSource.Config.droneIndex;
                    //for (j = 0; j <= size2; j++)
                    //{
                    //    if (DataSource.DronesArr[j].ID == droneid)
                    //    {
                    //        ind = j;
                    //        break;
                    //    }
                    //}
                    //DataSource.DronesArr[j].status = IDAL.DO.DroneStatus.available;
                    //DataSource.ParcelArr[index].delivered = day;
                    //}
                }
                /// <summary>
                /// send the drone to charge in a station
                /// </summary>
                /// <param name="DroneID"></param>
                /// <param name="StationID"></param>
                /// <returns></returns>
                public DroneCharge SendToCharge(int DroneID, int StationID)
                {
                    int count2 = 0;
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if(item.ID!=DroneID)
                            count2++;
                    }
                    if (count2 == DataSource.DronesList.Count)
                        throw new Exception("ERROR! value not found");
                    DroneCharge d = new DroneCharge();
                    d.droneID = DroneID;
                    d.stationeld = StationID;
                    int i = 0;
                    for(;i<DataSource.StationsList.Count;i++)
                    {
                        if(DataSource.StationsList[i].ID == StationID)
                        {
                            Station s = DataSource.StationsList[i];
                            s.chargeSlots--;
                            DataSource.StationsList[i] = s;
                            break;
                        }
                    }
                    if(i==DataSource.StationsList.Count-1)
                        throw new Exception("ERROR! value not found");
                    DataSource.DroneChargeList.Add(d);
                    return d;
                    //int size = DataSource.Config.droneIndex;
                    //int index = -1;
                    //for (int i = 0; i <= size; i++)
                    //{
                    //    if (DataSource.DronesArr[i].ID == DroneID)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //DroneCharge DC = new DroneCharge();
                    //if (index == -1)
                    //{
                    //    Console.WriteLine("ERROR! drone not found");
                    //    return DC;
                    //}
                    //DataSource.DronesArr[index].status = IDAL.DO.DroneStatus.maintenace;
                    //DC.droneID = DroneID;
                    //DC.stationeld = StationID;
                    //int index2 = 0;
                    //for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    //{
                    //    if (DataSource.StationsArr[i].ID == StationID)
                    //    {
                    //        index2 = i;
                    //        break;
                    //    }
                    //}
                    //DataSource.StationsArr[index2].chargeSlots = DataSource.StationsArr[index2].chargeSlots - 1;
                    //DroneCharge d = new DroneCharge();
                    //d.droneID = DroneID;
                    //d.stationeld = StationID;
                    //DataSource.DroneChargeList.Add(d);
                    //return DC;
                }
                /// <summary>
                /// release the drone from the charge slote
                /// </summary>
                /// <param name="FuzzedUp"></param>
                public void BatteryCharged(DroneCharge dc)
                {
                    int count2 = 0;
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if (item.ID != dc.droneID)
                            count2++;
                    }
                    if (count2 == DataSource.DronesList.Count)
                        throw new Exception("ERROR! value not found");
                    int i = 0;
                    for (; i < DataSource.StationsList.Count; i++)
                    {
                        if (DataSource.StationsList[i].ID == dc.stationeld)
                        {
                            Station s = DataSource.StationsList[i];
                            s.chargeSlots++;
                            DataSource.StationsList[i] = s;
                            break;
                        }
                    }
                    if (i == DataSource.StationsList.Count - 1)
                        throw new Exception("ERROR! value not found");
                    DataSource.DroneChargeList.Remove(dc);
                    
                    //int size = DataSource.Config.droneIndex;
                    //int index = -1;
                    //for (int i = 0; i <= size; i++)
                    //{
                    //    if (DataSource.DronesArr[i].ID == FuzzedUp.droneID)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index == -1)
                    //{
                    //    Console.WriteLine("ERROR! drone not found");
                    //}
                    //else
                    //{
                    //    DataSource.DronesArr[index].status = IDAL.DO.DroneStatus.available;
                    //    int index2 = 0;
                    //    for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    //    {
                    //        if (DataSource.StationsArr[i].ID == FuzzedUp.stationeld)
                    //        {
                    //            index2 = i;
                    //            break;
                    //        }
                    //    }
                    //    DataSource.StationsArr[index2].chargeSlots = DataSource.StationsArr[index2].chargeSlots + 1;
                    //}
                }

                /// <summary>
                /// print a station
                /// </summary>
                /// <param name="id"></param>
                public Station printStation(int id)
                {
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.Equals(id))
                            return item;
                    }
                    throw new Exception("ERROR! the station doesn't exist");
                    //return default(Station);
                    // Station s;
                    //int size = DataSource.Config.stationIndex;
                    //int index = -1;
                    //for (int i = 0; i <= size; i++)
                    //{
                    //    if (DataSource.StationsArr[i].ID == id)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index == -1)
                    //    throw new Exception("ERROR! station not found");
                    //else
                    //{
                    //    s = DataSource.StationsArr[index];
                    //    return s;
                    //}

                }
                /// <summary>
                /// print a drone
                /// </summary>
                /// <param name="id"></param>
                public Drone printDrone(int id)
                {
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if (item.Equals(id))
                            return item;
                    }
                    throw new Exception("ERROR! the drone doesn't exist");
                    //Drone d;
                    //int size = DataSource.Config.droneIndex;
                    //int index = -1;
                    //for (int i = 0; i <= size; i++)
                    //{
                    //    if (DataSource.DronesArr[i].ID == id)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index == -1)
                    //    throw new Exception("ERROR! drone not found");
                    //else
                    //{
                    //    d = DataSource.DronesArr[index];
                    //    return d;
                    //}
                }
                /// <summary>
                /// print a customer
                /// </summary>
                /// <param name="id"></param>
                public Customer printCustomer(int id)
                {
                    foreach (Customer item in DataSource.CustomersList)
                    {
                        if (item.Equals(id))
                            return item;
                    }
                    throw new Exception("ERROR! the customer doesn't found");
                    //Customer c;
                    //int size = DataSource.Config.customerIndex;
                    //int index = -1;
                    //for (int i = 0; i <= size; i++)
                    //{
                    //    if (DataSource.CustomersArr[i].ID == id)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index == -1)
                    //    throw new Exception("ERROR! customer not found");
                    //else
                    //{
                    //    c = DataSource.CustomersArr[index];
                    //    return c;
                    //}
                }
                /// <summary>
                /// print a parcel
                /// </summary>
                /// <param name="id"></param>
                public Parcel printParcel(int id)
                {
                    foreach (Parcel item in DataSource.ParcelList)
                    {
                        if (item.Equals(id))
                            return item;
                    }
                    throw new Exception("ERROR! the parcel doesn't found");
                    //Parcel p;
                    //int size = DataSource.Config.parcelIndex;
                    //int index = -1;
                    //for (int i = 0; i <= size; i++)
                    //{
                    //    if (DataSource.ParcelArr[i].ID == id)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index == -1) 
                    //    throw new Exception("ERROR! parcel not found");
                    //else
                    //{
                    //    p = DataSource.ParcelArr[index];
                    //    return p;
                    //}
                }

                /// <summary>
                /// print all stations
                /// </summary>
                public IEnumerable<Station> printAllStations()
                {
                    List<Station> list = new List<Station>();
                    foreach (Station item in DataSource.StationsList)
                    {
                        list.Add(item);
                    }
                    return list;
                    //Station[] arr = new Station[DataSource.Config.stationIndex];
                    //for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    //{
                    //    arr[i] = DataSource.StationsArr[i];
                    //}
                    //return arr;
                }
                /// <summary>
                /// print all drones
                /// </summary>
                public IEnumerable<Drone> printAllDrones()
                {
                    List<Drone> lst = new List<Drone>();
                    foreach (Drone item in DataSource.DronesList)
                        lst.Add(item);
                    return lst;
                    //Drone[] arr = new Drone[DataSource.Config.droneIndex];
                    //for (int i = 0; i < DataSource.Config.droneIndex; i++)
                    //{
                    //    arr[i] = DataSource.DronesArr[i];
                    //}
                    //return arr;
                }
                /// <summary>
                /// print all customers
                /// </summary>
                public IEnumerable<Customer> printAllCustomers()
                {
                    List<Customer> lst = new List<Customer>();
                    foreach (Customer item in DataSource.CustomersList)
                        lst.Add(item);
                    return lst;
                    //Customer[] arr = new Customer[DataSource.Config.customerIndex];
                    //for (int i = 0; i < DataSource.Config.customerIndex; i++)
                    //{
                    //    arr[i] = DataSource.CustomersArr[i];
                    //}
                    //return arr;
                }
                /// <summary>
                /// print all parcels
                /// </summary>
                public IEnumerable<Parcel> printAllParcels()
                {
                    List<Parcel> lst = new List<Parcel>();
                    foreach (Parcel item in DataSource.ParcelList)
                        lst.Add(item);
                    return lst;
                    //Parcel[] arr = new Parcel[DataSource.Config.parcelIndex];
                    //for (int i = 0; i < DataSource.Config.parcelIndex; i++)
                    //{
                    //    arr[i] = DataSource.ParcelArr[i];
                    //}
                    //return arr;
                }
                /// <summary>
                /// print all parcels that have no yet drone
                /// </summary>
                public IEnumerable<Parcel> printParcelsWithoutDrone()
                {
                    List<Parcel> lst = new List<Parcel>();
                    foreach(Parcel item in DataSource.ParcelList)
                    {
                        if (item.droneID == 0)
                            lst.Add(item);
                    }
                    return lst;
                    //int count = 0;
                    //for (int i = 0; i < DataSource.Config.parcelIndex; i++)
                    //{
                    //    //Parcel p = DataSource.ParcelArr[i];
                    //    if (DataSource.ParcelArr[i].droneID == 0)
                    //        count++;
                    //}
                    //Parcel[] arr = new Parcel[count];
                    //count = 0;
                    //for (int i = 0; i < DataSource.Config.parcelIndex; i++)
                    //{
                    //    if (DataSource.ParcelArr[i].droneID == 0)
                    //    {
                    //        arr[count] = DataSource.ParcelArr[i];
                    //        count++;
                    //    }
                    //}
                    //return arr;
                }
                /// <summary>
                /// print all stations with charge slots available
                /// </summary>
                public IEnumerable<Station> printStationsWithChargeSlots()
                {
                    List<Station> lst = new List<Station>();
                    foreach(Station item in DataSource.StationsList)
                    {
                        if (item.chargeSlots > 0)
                            lst.Add(item);
                    }
                    return lst;
                    //int count = 0;
                    //for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    //{
                    //    if (DataSource.StationsArr[i].chargeSlots > 0)
                    //        count++;
                    //}
                    //Station[] arr = new Station[count];
                    //count = 0;
                    //for (int i = 0; i < DataSource.Config.stationIndex; i++)
                    //{
                    //    if (DataSource.StationsArr[i].chargeSlots > 0)
                    //    {
                    //        arr[count] = DataSource.StationsArr[i];
                    //        count++;
                    //    }
                    //}
                    //return arr;
                }
                /// <summary>
                /// delete a station from the list
                /// </summary>
                /// <param name="id"></param>
                public void deleteStation(int id)
                {
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.ID == id)
                        {
                            DataSource.StationsList.Remove(item);
                            return;
                        }
                    }
                    throw new Exception("ERROR! the station doesn't found");
                }
                /// <summary>
                /// delete a parcel from the list
                /// </summary>
                /// <param name="id"></param>
                public void deleteSParcel(int id)
                {
                    foreach (Parcel item in DataSource.ParcelList)
                    {
                        if (item.ID == id)
                        {
                            DataSource.ParcelList.Remove(item);
                            return;
                        }
                    }
                    throw new Exception("ERROR! the parcel doesn't found");
                }

                /// <summary>
                /// delete a drone from the list
                /// </summary>
                /// <param name="id"></param>
                public void deleteDrone(int id)
                {
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if (item.ID == id)
                        {
                            DataSource.DronesList.Remove(item);
                            return;
                        }
                    }
                    throw new Exception("ERROR! the drone doesn't found");
                }

                /// <summary>
                /// delete a customer from the list
                /// </summary>
                /// <param name="id"></param>
                public void deleteSCustomer(int id)
                {
                    foreach (Customer item in DataSource.CustomersList)
                    {
                        if (item.ID == id)
                        {
                            DataSource.CustomersList.Remove(item);
                            return;
                        }
                    }
                    throw new Exception("ERROR! the customer doesn't found");
                }

                /// <summary>
                /// update the details of a station
                /// </summary>
                /// <param name="id"></param>
                /// <param name="st"></param>
                public void updateStation(int id,Station st)
                {
                    for(int i=0;i<DataSource.StationsList.Count;i++)
                    {
                        if(DataSource.StationsList[i].ID==id)
                        {
                            DataSource.StationsList[i] = st;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the station doesn't found");
                }

                /// <summary>
                /// update the details of a parcel
                /// </summary>
                /// <param name="id"></param>
                /// <param name="p"></param>
                public void updateParcel(int id, Parcel p)
                {
                    for (int i = 0; i < DataSource.ParcelList.Count; i++)
                    {
                        if (DataSource.ParcelList[i].ID == id)
                        {
                            DataSource.ParcelList[i] = p;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the parcel doesn't found");
                }

                /// <summary>
                /// update the details of a drone
                /// </summary>
                /// <param name="id"></param>
                /// <param name="d"></param>
                public void updateDrone(int id, int mod)
                {
                    for (int i = 0; i < DataSource.DronesList.Count; i++)
                    {
                        if (DataSource.DronesList[i].ID == id)
                        {
                            Drone d = DataSource.DronesList[i];
                            d.model = mod;
                            DataSource.DronesList[i] = d;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the drone doesn't found");
                }

                /// <summary>
                /// update the details of a customer
                /// </summary>
                /// <param name="id"></param>
                /// <param name="c"></param>
                public void updateCustomer(int id, Customer c)
                {
                    for (int i = 0; i < DataSource.CustomersList.Count; i++)
                    {
                        if (DataSource.CustomersList[i].ID == id)
                        {
                            DataSource.CustomersList[i] = c;
                            return;
                        }
                    }
                    throw new Exception("ERROR! the customer doesn't found");
                }
            }
        }
    }

}

