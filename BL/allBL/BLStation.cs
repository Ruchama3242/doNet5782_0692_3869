using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BL;

namespace BL
{
    partial class BL : InterfaceBL
    {
        /// <summary>
        /// add a station to the list of dal.dataSource
        /// </summary>
        /// <param name="station"></param>
        public void addStation(IBL.BO.Station station)
        {
            try
            {
                if (station.location.longitude < -180 || station.location.longitude > 180)
                    throw new BLgeneralException("Error! the longitude is incorrect");
                if (station.location.latitude < -90 || station.location.latitude > 90)
                    throw new BLgeneralException("Error! the latitude is incorrect");
                IDAL.DO.Station stationDal = new IDAL.DO.Station();
                stationDal.ID = station.ID;
                stationDal.lattitude = station.location.latitude;
                stationDal.longitude = station.location.longitude;
                stationDal.name = station.name;
                stationDal.chargeSlots = station.chargeSlots;
                myDalObject.addStations(stationDal);
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// update some field in the station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="emptyChargeSlot"></param>
        public void updateStation(int id, string name, int chargeSlot)
        {
            try
            {
                IDAL.DO.Station stationDal = new IDAL.DO.Station();

                stationDal = myDalObject.findStation(id);
                if (name != "")
                    stationDal.name = name;
                if (chargeSlot != 0)
                    stationDal.chargeSlots = chargeSlot;

                myDalObject.updateStation(id, stationDal);
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// the func return all the list of the station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.StationToList> veiwListStation()
        {
           //get the list of the station from dal
            IEnumerable<IDAL.DO.Station> lstD= new List<IDAL.DO.Station>();
            lstD = myDalObject.getAllStations();

            //copy all the dal station to bl statio
            List<IBL.BO.StationToList> lstBL= new List<IBL.BO.StationToList>();
           
            foreach (var item in lstD)
            {
                IBL.BO.StationToList temp = new IBL.BO.StationToList();
                temp.ID = item.ID;
                temp.name = item.name;
                temp.availableChargeSlots = item.chargeSlots;
                //findDroneCharge return a list that contain all the drone in charge 
                temp.notAvailableChargeSlots = myDalObject.findDroneCharge(item.ID).Count;
                lstBL.Add(temp);
            }
            return lstBL;
        }

        /// <summary>
        /// get a id of station and return a station of BL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Station findStation(int id)
        {
            try
            {
                IBL.BO.Station s = new IBL.BO.Station();
                IDAL.DO.Station sD = myDalObject.findStation(id);
                s = convertStation(sD);
                return s;
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// return all the ststion with 1 or more avilable charge slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.Station> avilableCharginStation()
        {
            IEnumerable<IDAL.DO.Station> stations = myDalObject.getAllStations();
            List<IBL.BO.Station> avilable = new List<IBL.BO.Station>();
            foreach (var item in stations)
            {
                if (item.chargeSlots > 0)
                    avilable.Add(convertStation(item));
            }
            return avilable;
        }

        /// <summary>
        /// get a station of dal and return a ststion of bl
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private IBL.BO.Station convertStation(IDAL.DO.Station s)
        {
            IBL.BO.Station tmp = new IBL.BO.Station();
            tmp.chargeSlots = s.chargeSlots;
            tmp.ID = s.ID;
            tmp.location = new IBL.BO.Location();
            tmp.location.latitude = s.lattitude;
            tmp.location.longitude = s.longitude;
            tmp.name = s.name;

            List<IDAL.DO.DroneCharge> d = myDalObject.findDroneCharge(s.ID);
            List<IBL.BO.DroneInCharge> dr = new List<IBL.BO.DroneInCharge>();
            foreach (var item in d)
            {
                IBL.BO.DroneInCharge charge = new IBL.BO.DroneInCharge();
                charge.ID = item.droneID;
                charge.battery = findDrone(item.droneID).battery;
                dr.Add(charge);
            }
            tmp.dronesInChargeList = new List<IBL.BO.DroneInCharge>();
            tmp.dronesInChargeList = dr;
            return tmp;

        }
    }
}
