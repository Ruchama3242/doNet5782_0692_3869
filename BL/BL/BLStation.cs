﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BL;

namespace BL
{
   internal partial class BL 
    {
        
        public void addStation(Station station)
        {
            try
            {
                if (station.location.longitude < 29.3 || station.location.longitude > 33.5)
                    throw new BLgeneralException("Error! the longitude is incorrect");
                if (station.location.latitude < 33.7 || station.location.latitude > 36.3)
                    throw new BLgeneralException("Error! the latitude is incorrect");
                if (station.ID < 1000 || station.ID > 9999)
                    throw new BLgeneralException("ERROR! the id must be with 4 digits ");
                if (station.chargeSlots <= 0)
                    throw new BLgeneralException("ERROR! the number og charge slots must be positive");
                DO.Station stationDal = new DO.Station();
                stationDal.ID = station.ID;
                stationDal.lattitude = station.location.latitude;
                stationDal.longitude = station.location.longitude;
                stationDal.name = station.name;
                stationDal.chargeSlots = station.chargeSlots;
                dl.addStations(stationDal);
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message/*,e*/);
            }
        }

        public void updateStation(int id, string name, int chargeSlot)
        {
            if (chargeSlot <= 0)
                throw new BLgeneralException("ERROR! the number og charge slots must be positive");
            try
            {
                DO.Station stationDal = new DO.Station();

                stationDal = dl.findStation(id);
                if (name != "")
                    stationDal.name = name;
                if (chargeSlot != 0)
                    stationDal.chargeSlots = chargeSlot;

                dl.updateStation(id, stationDal);
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

       public IEnumerable<StationToList> veiwListStation()
        {
           //get the list of the station from dal
            IEnumerable<DO.Station> lstD= new List<DO.Station>();
            lstD = dl.getAllStations();

            //copy all the dal station to bl statio
            List<StationToList> lstBL= new List<StationToList>();
           
            foreach (var item in lstD)
            {
               StationToList temp = new StationToList();
                temp.ID = item.ID;
                temp.name = item.name;
                temp.availableChargeSlots = item.chargeSlots;
                //findDroneCharge return a list that contain all the drone in charge 
                temp.notAvailableChargeSlots = dl.findDroneCharge(item.ID).Count();
               

                lstBL.Add(temp);
            }
            return lstBL;
        }

        public Station findStation(int id)
        {
            try
            {
                Station s = new Station();
                DO.Station sD = dl.findStation(id);
                s = convertStation(sD);
                List<DO.DroneCharge> drCh = new List<DO.DroneCharge>();
                return s;
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

        public IEnumerable<StationToList> avilableCharginStation()
        {
            //IEnumerable<DO.Station> stations = dl.getAllStations();
            IEnumerable<StationToList> stations = veiwListStation();
            List<StationToList> avilable = new List<StationToList>();
            foreach (var item in stations)
            {
                if (item.availableChargeSlots > 0)
                    avilable.Add(item);
            }
            return avilable;
        }

        /// <summary>
        /// get a station of dal and return a ststion of bl
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private Station convertStation(DO.Station s)
        {
            Station tmp = new Station();
            tmp.chargeSlots = s.chargeSlots;
            tmp.ID = s.ID;
            tmp.location = new Location();
            tmp.location.latitude = s.lattitude;
            tmp.location.longitude = s.longitude;
            tmp.name = s.name;

            IEnumerable<DO.DroneCharge> d = dl.findDroneCharge(s.ID);
            List<DroneInCharge> dr = new List<DroneInCharge>();
            foreach (var item in d)
            {
                DroneInCharge charge = new DroneInCharge();
                charge.ID = item.droneID;
                charge.battery = findDrone(item.droneID).battery;
                //charge.enterToCharge = item.enterToCharge;
                dr.Add(charge);
            }
            tmp.dronesInChargeList = new List<DroneInCharge>();
            tmp.dronesInChargeList = dr;
            return tmp;

        }


        public void deleteStation(int id)
        {
            dl.deleteStation(id);
        }
    }
}
