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
                /// add parcel to the array
                /// </summary>
                /// <param name="Fedex"></param>
                public int addParcel(Parcel temp)
                {
                    temp.ID = DataSource.Config.runnerID;
                    DataSource.ParcelList.Add(temp);
                    DataSource.Config.runnerID++;
                    return temp.ID;
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
                    throw new generalException("ERROR! the value not found");
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
                    throw new generalException("ERROR! the value not found");
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
                    throw new generalException("ERROR! the value not found");
                }

                /// <summary>
                /// print a parcel
                /// </summary>
                /// <param name="id"></param>
                public Parcel findParcel(int id)
                {
                    foreach (Parcel item in DataSource.ParcelList)
                    {
                        if (item.ID==id)
                            return item;
                    }
                    throw new IdUnExistsException("ERROR! the parcel doesn't found");
                }

                /// <summary>
                /// print all parcels
                /// </summary>
                public IEnumerable<Parcel> getAllParcels()
                {
                    List<Parcel> lst = new List<Parcel>();
                    foreach (Parcel item in DataSource.ParcelList)
                        lst.Add(item);
                    return lst;
                }

                /// <summary>
                /// print all parcels that have no yet drone
                /// </summary>
                public IEnumerable<Parcel> getParcelsWithoutDrone()
                {
                    List<Parcel> lst = new List<Parcel>();
                    foreach (Parcel item in DataSource.ParcelList)
                    {
                        if (item.droneID == 0&&item.delivered==DateTime.MinValue)
                            lst.Add(item);
                    }
                    return lst;
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
                    throw new IdUnExistsException("ERROR! the parcel doesn't found");
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
                    throw new IdUnExistsException("ERROR! the parcel doesn't found");
                }
            }
        }
    }
}
