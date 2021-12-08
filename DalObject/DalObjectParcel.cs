//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DAL;
//using DalApi;

//namespace DalObject
//{
//    sealed partial class DalObject : IDal
//    {

//        ///// <summary>
//        /////מקבלת פרדיקט ומחזירה את כל האיברים העונים לפרדיקט
//        ///// </summary>
//        ///// <param name="StationCondition"></param>
//        ///// <returns></returns>
//        //public IEnumerable<Parcel> GetPartParcel(Func<Parcel,bool> predicate)
//        //{
//        //    var list = from Parcel in DataSource.ParcelList
//        //               where (predicate(Parcel))
//        //               select Parcel;

//        //    foreach (var item in list)
//        //    {
//        //        list.ToList().Add(item);
//        //    }
//        //    return list;
//        //}

//        public int addParcel(DO.Parcel temp)
//        {

//            temp.ID = DataSource.Config.runnerID;
//            DataSource.ParcelList.Add(temp);
//            DataSource.Config.runnerID++;

//            return temp.ID;
//        }

//        public void ParcelDrone(int parcelID, int droneID)
//        {
//            for (int i = 0; i < DataSource.ParcelList.Count; i++)
//            {
//                if (DataSource.ParcelList[i].ID == parcelID)
//                {
//                    DO.Parcel p = DataSource.ParcelList[i];
//                    p.droneID = droneID;
//                    p.scheduled = DateTime.Now;
//                    DataSource.ParcelList[i] = p;
//                    return;
//                }
//            }
//            throw new DO.generalException("ERROR! the value not found");
//        }
//        public void ParcelPickedUp(int parcelID, DateTime day)
//        {
//            for (int i = 0; i < DataSource.ParcelList.Count; i++)
//            {
//                if (DataSource.ParcelList[i].ID == parcelID)
//                {
//                    DO.Parcel p = DataSource.ParcelList[i];
//                    p.pickedUp = day;
//                    DataSource.ParcelList[i] = p;
//                    return;
//                }
//            }
//            throw new DO.generalException("ERROR! the value not found");
//        }

//        public void ParcelReceived(int parcelID, DateTime day)
//        {
//            for (int i = 0; i < DataSource.ParcelList.Count; i++)
//            {
//                if (DataSource.ParcelList[i].ID == parcelID)
//                {
//                    DO.Parcel p = DataSource.ParcelList[i];
//                    p.delivered = day;
//                    DataSource.ParcelList[i] = p;
//                    return;
//                }
//            }
//            throw new DO.generalException("ERROR! the value not found");
//        }

//        public DO.Parcel findParcel(int id)
//        {
//            foreach (DO.Parcel item in DataSource.ParcelList)
//            {
//                if (item.ID == id)
//                    return item;
//            }
//            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
//        }
//        /// <summary>
//        /// return IEnumerable<Parcel> of all the parcel
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<DO.Parcel> getAllParcels()
//        {
//            List<DO.Parcel> lst = new List<DO.Parcel>();
//            foreach (DO.Parcel item in DataSource.ParcelList)
//                lst.Add(item);
//            return lst;
//        }

//        /// <summary>
//        /// print all parcels that have no yet drone
//        /// </summary>
//        public IEnumerable<DO.Parcel> getParcelsWithoutDrone()
//        {
//            List<DO.Parcel> lst = new List<DO.Parcel>();
//            foreach (DO.Parcel item in DataSource.ParcelList)
//            {
//                if (item.droneID == 0 && item.delivered == null)
//                    lst.Add(item);
//            }
//            return lst;
//        }

//        public void deleteSParcel(int id)
//        {
//            foreach (DO.Parcel item in DataSource.ParcelList)
//            {
//                if (item.ID == id)
//                {
//                    DataSource.ParcelList.Remove(item);
//                    return;
//                }
//            }
//            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
//        }

//        public void updateParcel(int id, DO.Parcel p)
//        {
//            for (int i = 0; i < DataSource.ParcelList.Count; i++)
//            {
//                if (DataSource.ParcelList[i].ID == id)
//                {
//                    DataSource.ParcelList[i] = p;
//                    return;
//                }
//            }
//            throw new DO.IdUnExistsException("ERROR! the parcel doesn't found");
//        }
//    }
//}
