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
        public IEnumerable<IDAL.DO.Station> veiwListStation()
        {
            //הפונקציה מדפיסה את נתוני התחנה בלי רשימת הרחפנים שבטעינה,אני צריכ לחשוב איך לעשות את זה
            IEnumerable<IDAL.DO.Station> lst = new List<IDAL.DO.Station>();
            lst = myDalObject.printAllStations();
            return lst;
        }
    }
}
