using System;
using DalApi;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{
    sealed partial class DalObject 
    {
            public IEnumerable<DO.Drone> GetPartOfDrone(Func<DO.Drone, bool> droneCondition = null)
            {
                if (droneCondition == null)
                    return DAL.DataSource.DronesList;
                var list = from Drone in DAL.DataSource.DronesList
                           where (droneCondition(Drone))
                           select Drone;
                foreach (var item in list)
                {
                    list.ToList().Add(item);
                }
                return list;
            }

            public void addDrone(DO.Drone temp)
            {
                bool flag = true;
                foreach (DO.Drone item in DAL.DataSource.DronesList)
                {

                    if (item.ID == temp.ID) //return true if the field is the same
                        flag = false;
                }
                if (flag)
                    DAL.DataSource.DronesList.Add(temp);
                else
                    throw new DO.IdExistsException();
            }

            public DO.DroneCharge SendToCharge(int DroneID, int StationID)
            {
                findDrone(DroneID);//throw exception if the drone doest exist
                DO.DroneCharge d = new DO.DroneCharge();
                d.droneID = DroneID;
                d.stationeld = StationID;
                int i = 0;
                for (; i < DAL.DataSource.StationsList.Count; i++)
                {
                    if (DAL.DataSource.StationsList[i].ID == StationID)
                    {
                        DO.Station s = DAL.DataSource.StationsList[i];
                        s.chargeSlots--;
                        DAL.DataSource.StationsList[i] = s;
                        break;
                    }
                }
                if (i == DAL.DataSource.StationsList.Count)
                    throw new DO.generalException("ERROR! value not found");
                DAL.DataSource.DroneChargeList.Add(d);
                return d;
            }
            public void BatteryCharged(DO.DroneCharge dc)
            {
                int count2 = 0;
                foreach (DO.Drone item in DAL.DataSource.DronesList)
                {
                    if (item.ID != dc.droneID)
                        count2++;
                }
                if (count2 == DAL.DataSource.DronesList.Count)
                    throw new DO.generalException("ERROR! value not found");
                int i = 0;
                for (; i < DAL.DataSource.StationsList.Count; i++)
                {
                    if (DAL.DataSource.StationsList[i].ID == dc.stationeld)
                    {
                        DO.Station s = DAL.DataSource.StationsList[i];
                        s.chargeSlots++;
                        DAL.DataSource.StationsList[i] = s;
                        break;
                    }
                }
                if (i ==DAL.DataSource.StationsList.Count)
                    throw new DO.generalException("ERROR! value not found");
                DAL.DataSource.DroneChargeList.Remove(dc);
            }

            public DO.Drone findDrone(int id)
            {
                foreach (DO.Drone item in DAL.DataSource.DronesList)
                {
                    if (item.ID == id)
                        return item;
                }
                throw new DO.IdUnExistsException("ERROR! the drone doesn't exist");
            }

            /// <summary>
            /// return a list of all drones
            /// </summary>
            public IEnumerable<DO.Drone> getAllDrones()
            {
                List<DO.Drone> lst = new List<DO.Drone>();
                foreach (DO.Drone item in DAL.DataSource.DronesList)
                    lst.Add(item);
                return lst;
            }

            public void deleteDrone(int id)
            {
                foreach (DO.Drone item in DAL.DataSource.DronesList)
                {
                    if (item.ID == id)
                    {
                        DAL.DataSource.DronesList.Remove(item);
                        return;
                    }
                }
                throw new DO.IdUnExistsException("ERROR! the drone doesn't found");
            }

            public void updateDrone(int id, int mod)
            {
                for (int i = 0; i < DAL.DataSource.DronesList.Count; i++)
                {
                    if (DAL.DataSource.DronesList[i].ID == id)
                    {

                        DO.Drone d = DAL.DataSource.DronesList[i];
                        d.model = mod;
                        DAL.DataSource.DronesList[i] = d;
                        return;

                    }
                }
                throw new DO.IdUnExistsException("ERROR! the drone doesn't found");
            }
            public IEnumerable<DO.DroneCharge> findDroneCharge(int id)
            {
                //find all the station with empty charge slots
                var lst = from item in DAL.DataSource.DroneChargeList
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

            public DO.DroneCharge findStationOfDroneCharge(int id)
            {

                DO.DroneCharge temp = new DO.DroneCharge();
                foreach (DO.DroneCharge item in DAL.DataSource.DroneChargeList)
                    if (item.droneID == id)
                        temp = item;

                return temp;
            }
        }
    }

