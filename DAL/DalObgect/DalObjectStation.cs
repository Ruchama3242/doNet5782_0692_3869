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
                public void addStations(Station temp)
                {
                    bool flug = true;
                    foreach (Station item in DataSource.StationsList)
                    {

                        if (item.ID==temp.ID) //return true if the field is the same
                            flug = false;
                    }
                    if (flug)
                        DataSource.StationsList.Add(temp);
                    else
                        throw new IdExistsException("ERROR! the ID already exist");
                }

                public Station findStation(int id)
                {
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.ID==id)
                            return item;
                    }
                    throw new IdUnExistsException("ERROR! the station doesn't exist");
                }

                /// <summary>
                /// ruturn all stations
                /// </summary>
                public IEnumerable<Station> getAllStations()
                {
                    List<Station> list = new List<Station>();
                    foreach (Station item in DataSource.StationsList)
                    {
                        list.Add(item);
                    }
                    return list;
                }

                /// <summary>
                /// print all stations with charge slots available
                /// </summary>
                public IEnumerable<Station> getStationsWithChargeSlots()
                {
                    List<Station> lst = new List<Station>();
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.chargeSlots > 0)
                            lst.Add(item);
                    }
                    return lst;
                }

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
                    throw new IdUnExistsException("ERROR! the station doesn't found");
                }

               public void updateStation(int id, Station st)
                {
                    for (int i = 0; i < DataSource.StationsList.Count; i++)
                    {
                        if (DataSource.StationsList[i].ID == id)
                        {
                            DataSource.StationsList[i] = st;
                            return;
                        }
                    }
                    throw new IdUnExistsException("ERROR! the station doesn't found");
                }
            }
        }
    }
