using System;
using DalApi;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetPartParcel()
        {
            var list = from Parcel in DataSource.ParcelList

                       select Parcel;

            foreach (var item in list)
            {
                list.ToList().Add(item);
            }
            return list;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int addParcel(DO.Parcel temp)
        {


            temp.ID = DAL.DataSource.Config.runnerID;
            DAL.DataSource.ParcelList.Add(temp);
            DAL.DataSource.Config.runnerID++;

            return temp.ID;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel findParcel(int id)
        {
            foreach (DO.Parcel item in DAL.DataSource.ParcelList)
            {
                if (item.ID == id)
                    return item;
            }
            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<DO.Parcel> getAllParcels()
        {
            List<DO.Parcel> lst = new List<DO.Parcel>();
            foreach (DO.Parcel item in DAL.DataSource.ParcelList)
                lst.Add(item);
            return lst;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

