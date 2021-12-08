﻿using System;
using DO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


namespace DO
{
    namespace DalObject
    {
        sealed partial class DalObject
        {

            ///// <summary>
            /////מקבלת פרדיקט ומחזירה את כל האיברים העונים לפרדיקט
            ///// </summary>
            ///// <param name="StationCondition"></param>
            ///// <returns></returns>
            //public IEnumerable<Parcel> GetPartParcel(Func<Parcel,bool> predicate)
            //{
            //    var list = from Parcel in DataSource.ParcelList
            //               where (predicate(Parcel))
            //               select Parcel;

            //    foreach (var item in list)
            //    {
            //        list.ToList().Add(item);
            //    }
            //    return list;
            //}

            public int addParcel(Parcel temp)
            {


                temp.ID = DataSource.Config.runnerID;
                DataSource.ParcelList.Add(temp);
                DataSource.Config.runnerID++;

                return temp.ID;
            }

            public void ParcelDrone(int parcelID, int droneID)
            {
                for (int i = 0; i < DataSource.ParcelList.Count; i++)
                {
                    if (DataSource.ParcelList[i].ID == parcelID)
                    {
                        Parcel p = DataSource.ParcelList[i];
                        p.droneID = droneID;
                        p.scheduled = DateTime.Now;
                        DataSource.ParcelList[i] = p;
                        return;
                    }
                }
                throw new generalException("ERROR! the value not found");
            }
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

            public Parcel findParcel(int id)
            {
                foreach (Parcel item in DataSource.ParcelList)
                {
                    if (item.ID == id)
                        return item;
                }
                throw new IdUnExistsException("ERROR! the parcel doesn't found");
            }
            /// <summary>
            /// return IEnumerable<Parcel> of all the parcel
            /// </summary>
            /// <returns></returns>
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
                    if (item.droneID == 0 && item.delivered == null)
                        lst.Add(item);
                }
                return lst;
            }

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
