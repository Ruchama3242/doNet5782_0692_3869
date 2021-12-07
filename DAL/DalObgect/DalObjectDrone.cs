using System;
using DO;
using DAL;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


    namespace DO
    {
        namespace DalObject
        {
            partial class DalObject 
            {
                public IEnumerable<Drone> GetPartOfDrone(Func<Drone, bool> droneCondition=null)
                {
                    if (droneCondition == null)
                        return DataSource.DronesList;
                    var list = from Drone in DataSource.DronesList
                               where (droneCondition(Drone))
                               select Drone;
                    foreach (var item in list)
                    {
                        list.ToList().Add(item);
                    }
                    return list;
                }

                public void addDrone(Drone temp)
                {
                    bool flag = true;
                    foreach (Drone item in DataSource.DronesList)
                    {

                        if (item.ID==temp.ID) //return true if the field is the same
                            flag = false;
                    }
                    if (flag)
                        DataSource.DronesList.Add(temp);
                    else
                        throw new IdExistsException();
                }

                public DroneCharge SendToCharge(int DroneID, int StationID)
                {
                    findDrone(DroneID);//throw exception if the drone doest exist
                    DroneCharge d = new DroneCharge();
                    d.droneID = DroneID;
                    d.stationeld = StationID;
                    int i = 0;
                    for (; i < DataSource.StationsList.Count; i++)
                    {
                        if (DataSource.StationsList[i].ID == StationID)
                        {
                            Station s = DataSource.StationsList[i];
                            s.chargeSlots--;
                            DataSource.StationsList[i] = s;
                            break;
                        }
                    }
                    if (i == DataSource.StationsList.Count)
                        throw new generalException("ERROR! value not found");
                    DataSource.DroneChargeList.Add(d);
                    return d;
                }
                public void BatteryCharged(DroneCharge dc)
                {
                    int count2 = 0;
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if (item.ID != dc.droneID)
                            count2++;
                    }
                    if (count2 == DataSource.DronesList.Count)
                        throw new generalException("ERROR! value not found");
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
                    if (i == DataSource.StationsList.Count )
                        throw new generalException("ERROR! value not found");
                    DataSource.DroneChargeList.Remove(dc);
                }

                public Drone findDrone(int id)
                {
                    foreach (Drone item in DataSource.DronesList)
                    {
                        if (item.ID==id)
                            return item;
                    }
                    throw new IdUnExistsException("ERROR! the drone doesn't exist");
                }

                /// <summary>
                /// return a list of all drones
                /// </summary>
                public IEnumerable<Drone> getAllDrones()
                {
                    List<Drone> lst = new List<Drone>();
                    foreach (Drone item in DataSource.DronesList)
                        lst.Add(item);
                    return lst;
                }

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
                    throw new IdUnExistsException("ERROR! the drone doesn't found");
                }

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
                    throw new IdUnExistsException("ERROR! the drone doesn't found");
                }
                public IEnumerable<DroneCharge> findDroneCharge(int id)
                {
                    //find all the station with empty charge slots
                    var lst = from item in DataSource.DroneChargeList
                              where item.stationeld == id
                              select item;
                    foreach (var item2 in lst)
                        lst.ToList().Add(item2);
                    return lst;
                    //List < DroneCharge > tempList = new List<DroneCharge>();
                    //foreach (DroneCharge item in DataSource.DroneChargeList)
                    //    if (item.stationeld == id) 
                    //        tempList.Add(item);

                    //return tempList;
                }

                public DroneCharge findStationOfDroneCharge(int id)
                {

                    DroneCharge temp = new DroneCharge();
                    foreach (DroneCharge item in DataSource.DroneChargeList)
                        if (item.droneID == id)
                            temp=item;

                    return temp;
                }
            }
        }
    }
