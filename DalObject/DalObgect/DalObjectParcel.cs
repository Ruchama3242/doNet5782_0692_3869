using System;
using DalApi;
using System.Collections.Generic;
using DO;
using DAL;
using System.Linq;

namespace Dal
{
    sealed partial class DalObject 
    {

        /// <summary>
        ///מקבלת פרדיקט ומחזירה את כל האיברים העונים לפרדיקט
        /// </summary>
        /// <param name="StationCondition"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> GetPartParcel(Func<Parcel, bool> predicate = null)
        {
            var list = from Parcel in DataSource.ParcelList
                       where (predicate(Parcel))
                       select Parcel;

            foreach (var item in list)
            {
                list.ToList().Add(item);
            }
            return list;
        }

        public int addParcel(DO.Parcel temp)
            {


                temp.ID = DAL.DataSource.Config.runnerID;
                DAL.DataSource.ParcelList.Add(temp);
                DAL.DataSource.Config.runnerID++;

                return temp.ID;
            }

            public void ParcelDrone(int parcelID, int droneID)
            {
                for (int i = 0; i < DAL.DataSource.ParcelList.Count; i++)
                {
                    if (DAL.DataSource.ParcelList[i].ID == parcelID)
                    {
                        DO.Parcel p = DAL.DataSource.ParcelList[i];
                        p.droneID = droneID;
                        p.scheduled = DateTime.Now;
                        DAL.DataSource.ParcelList[i] = p;
                        return;
                    }
                }
                throw new DO.generalException("ERROR! the value not found");
            }
            public void ParcelPickedUp(int parcelID, DateTime day)
            {
                for (int i = 0; i < DAL.DataSource.ParcelList.Count; i++)
                {
                    if (DAL.DataSource.ParcelList[i].ID == parcelID)
                    {
                        DO.Parcel p = DAL.DataSource.ParcelList[i];
                        p.pickedUp = day;
                        DAL.DataSource.ParcelList[i] = p;
                        return;
                    }
                }
                throw new DO.generalException("ERROR! the value not found");
            }

            public void ParcelReceived(int parcelID, DateTime day)
            {
                for (int i = 0; i < DAL.DataSource.ParcelList.Count; i++)
                {
                    if (DAL.DataSource.ParcelList[i].ID == parcelID)
                    {
                        DO.Parcel p = DAL.DataSource.ParcelList[i];
                        p.delivered = day;
                        DAL.DataSource.ParcelList[i] = p;
                        return;
                    }
                }
                throw new DO.generalException("ERROR! the value not found");
            }

            public DO.Parcel findParcel(int id)
            {
                foreach (DO.Parcel item in DAL.DataSource.ParcelList)
                {
                    if (item.ID == id)
                        return item;
                }
                throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
            }
            /// <summary>
            /// return IEnumerable<Parcel> of all the parcel
            /// </summary>
            /// <returns></returns>
            public IEnumerable<DO.Parcel> getAllParcels()
            {
                List<DO.Parcel> lst = new List<DO.Parcel>();
                foreach (DO.Parcel item in DAL.DataSource.ParcelList)
                    lst.Add(item);
                return lst;
            }

            /// <summary>
            /// print all parcels that have no yet drone
            /// </summary>
            public IEnumerable<DO.Parcel> getParcelsWithoutDrone()
            {
                List<DO.Parcel> lst = new List<DO.Parcel>();
                foreach (DO.Parcel item in DAL.DataSource.ParcelList)
                {
                    if (item.droneID == 0 && item.delivered == null)
                        lst.Add(item);
                }
                return lst;
            }

            public void deleteSParcel(int id)
            {
                foreach (DO.Parcel item in DAL.DataSource.ParcelList)
                {
                    if (item.ID == id)
                    {
                        DAL.DataSource.ParcelList.Remove(item);
                        return;
                    }
                }
                throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
            }

            public void updateParcel(int id, DO.Parcel p)
            {
                for (int i = 0; i < DAL.DataSource.ParcelList.Count; i++)
                {
                    if (DAL.DataSource.ParcelList[i].ID == id)
                    {
                        DAL.DataSource.ParcelList[i] = p;
                        return;
                    }
                }
                throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
            }
        }
    }

