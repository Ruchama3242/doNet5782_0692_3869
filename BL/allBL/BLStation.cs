using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BL;

namespace BL
{
    partial class BL
    {
        /// <summary>
        /// add a station to the list of dal.dataSource
        /// </summary>
        /// <param name="station"></param>
        public void addStation(IBL.BO.Station station)
        {
            IDAL.DO.Station stationDal = new IDAL.DO.Station();
            stationDal.ID = station.ID;
            stationDal.lattitude = station.location.latitude;
            stationDal.longitude = station.location.longitude;
            stationDal.name = station.name;
            stationDal.chargeSlots = station.chargeSlots;
            myDalObject.addStations(stationDal);
        }

        /// <summary>
        /// update some field in the station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="emptyChargeSlot"></param>
        public void updateStation(int id, string name, int chargeSlot)
        {
            IDAL.DO.Station stationDal = new IDAL.DO.Station();

            stationDal = myDalObject.findStation(id);
            if (name != "")
                stationDal.name = name;
            if (chargeSlot != 0) 
                stationDal.chargeSlots = chargeSlot;

            myDalObject.updateStation(id,stationDal);
        }

        /// <summary>
        /// the func return all the list of the station
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.StationToList> veiwListStation()
        {
           //get the list of the station from dal
            IEnumerable<IDAL.DO.Station> lstD= new List<IDAL.DO.Station>();
            lstD = myDalObject.printAllStations();

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

        public IBL.BO.Station findStation(int id)
        {
            IBL.BO.Station s = new IBL.BO.Station();
            IDAL.DO.Station sD = myDalObject.findStation(id);
            s.ID = sD.ID;
            s.location.latitude = sD.lattitude;
            s.location.longitude = sD.longitude;
            s.name = sD.name;
            s.chargeSlots = sD.chargeSlots;

            //for the field of drone in charge list
            List<IDAL.DO.DroneCharge> d = myDalObject.findDroneCharge(id);
            List<IBL.BO.DroneInCharge> dr = new List<IBL.BO.DroneInCharge>();
            foreach (var item in d)
            {
                IBL.BO.DroneInCharge tmp = new IBL.BO.DroneInCharge();
                tmp.ID = item.droneID;
                tmp.battery = findDrone(item.droneID).battery;
                dr.Add(tmp);
            }
            s.dronesInChargeList = dr;

            return s;
        }
    }
}
