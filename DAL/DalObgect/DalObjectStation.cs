using System;
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
            public partial class DalObject 
            {
                /// <summary>
                /// add station to the array
                /// </summary>
                /// <param name="temp"></param>
                public void addStations(Station temp)
                {
                    bool flug = true;
                    foreach (Station item in DataSource.StationsList)
                    {

                        if (item.Equals(temp.ID)) //return true if the field is the same
                            flug = false;
                    }
                    if (flug)
                        DataSource.StationsList.Add(temp);
                    else
                        throw new IdExistsException("ERROR! the ID already exist");
                }

                /// <summary>
                /// print a station
                /// </summary>
                /// <param name="id"></param>
                public Station findStation(int id)
                {
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.Equals(id))
                            return item;
                    }
                    throw new IdUnExistsException("ERROR! the station doesn't exist");
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
                }
                /// <summary>
                /// print all stations with charge slots available
                /// </summary>
                public IEnumerable<Station> printStationsWithChargeSlots()
                {
                    List<Station> lst = new List<Station>();
                    foreach (Station item in DataSource.StationsList)
                    {
                        if (item.chargeSlots > 0)
                            lst.Add(item);
                    }
                    return lst;
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
                    throw new IdUnExistsException("ERROR! the station doesn't found");
                }

                /// <summary>
                /// update the details of a station
                /// </summary>
                /// <param name="id"></param>
                /// <param name="st"></param>
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
}
