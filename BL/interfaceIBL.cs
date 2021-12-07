﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
   public interface interfaceIBL
    {
     
        #region - - - - - - - - - - - - customer- - - - - - - - - - - - - - - 
        /// <summary>
        /// get a customer of BL and add it to the list of DAL
        /// </summary>
        /// <param name="customerBL"></param>
        public void addCustomer(IBL.BO.Customer customerBL);

        /// <summary>
        /// update the name or the phone number of the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public void updateCustomer(int id, string name, string phoneNum);

        /// <summary>
        /// print all the list of the customer to list
        /// </summary>
        public IEnumerable<IBL.BO.CustomerToList> viewListCustomer();

        /// <summary>
        /// get a id of customer and return a customer of BL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Customer findCustomer(int id);
        #endregion

        #region - - - - - - - - - - - - drone - - - - - - - - - - - - - - - -
        /// <summary>
        /// Returns a filtered list by drone status
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public IEnumerable<IBL.BO.DroneToList> droneFilterStatus(IBL.BO.DroneStatus w);


        /// <summary>
        /// Returns a filtered list by weight category
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public IEnumerable<IBL.BO.DroneToList> droneFilterWheight(IBL.BO.WeightCategories w);

            /// <summary>
            /// adding a drone to droneList
            /// </summary>
            /// <param name="drone"></param>
        public void addDrone(int id, int model, int weight, int stationId);

        /// <summary>
        /// get a id and return the drone(of bl) with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Drone findDrone(int id);

        /// <summary>
        /// update the name of the drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void updateNameDrone(int ID, int model);

        /// <summary>
        /// release the drone from charge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void releaseFromCharge(int id, int time);

        /// <summary>
        /// return a IEnumerable<IBL.BO.DroneToList>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.DroneToList> getAllDrones();

        /// <summary>
        /// send a drone to the closed station
        /// </summary>
        /// <param name="id"></param>
        public void sendToCharge(int id);

        /// <summary>
        /// get a id of drone and find a  parcel 
        /// </summary>
        /// <param name="id"></param>
        public void parcelToDrone(int id);


        public IEnumerable<IBL.BO.DroneToList> droneFilterWheight(IBL.BO.WeightCategories w);

        public IEnumerable<IBL.BO.DroneToList> droneFilterStatus(IBL.BO.DroneStatus w);


        #endregion

        #region - - - - - - - - - - - - parcel - - - - - - - - - - - - - - - 
        /// <summary>
        /// add a parcel to the list in the DAL
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public int addParcel(int senderId, int targetId, int weight, int priority);

        /// <summary>
        /// return the list of all parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.ParcelToList> getAllParcels();

        // <summary>
        /// return a parcel with all details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Parcel findParcel(int id);

        /// <summary>
        /// return a list of parcels that have not a drone yet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.ParcelToList> parcelsWithoutDrone();

        /// <summary>
        /// the function update the parcel to be collected by the drone
        /// </summary>
        /// <param name="droneid"></param>
        public void packageCollection(int droneid);

        /// <summary>
        /// The function update the parcel to be delivered
        /// </summary>
        /// <param name="droneid"></param>
        public void packageDelivery(int droneid);
        #endregion

        #region       - - - - - - - - - - - - - station - - - - - - - - - - - - - - - 
        /// <summary>
        /// add a station to the list of dal.dataSource
        /// </summary>
        /// <param name="station"></param>
        public void addStation(IBL.BO.Station station);

        /// <summary>
        /// update some field in the station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="emptyChargeSlot"></param>
        public void updateStation(int id, string name, int chargeSlot);

        /// <summary>
        /// the func return all the list of the station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.StationToList> veiwListStation();

        /// <summary>
        /// get a id of station and return a station of BL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Station findStation(int id);

        /// <summary>
        /// return all the ststion with 1 or more avilable charge slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.Station> avilableCharginStation();

        #endregion
    }
}
