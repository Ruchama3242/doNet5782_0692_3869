using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;

namespace BL
{
   partial class BL
    {
        Random rnd = new Random();
        /// <summary>
        /// "convert" a drone from BL type to DAL type
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private IDAL.DO.Drone droneDal(IBL.BO.DroneToList d)
        {
            IDAL.DO.Drone dr = new IDAL.DO.Drone { ID = d.ID, model = d.droneModel, weight = (IDAL.DO.WeightCategories)d.weight };
            return dr;
        }
        /// <summary>
        /// adding a drone to droneList
        /// </summary>
        /// <param name="drone"></param>
        public void addDrone(int id,int model,int weight,int stationId)
        {
           
            try
            {

                IBL.BO.DroneToList d = new IBL.BO.DroneToList();
                d.ID = id;
                d.droneModel = model;
                d.weight = (IBL.BO.WeightCategories)weight;
                d.battery = rnd.Next(20, 40);
                d.status = IBL.BO.DroneStatus.maintenace;
                try
                {
                    IDAL.DO.Station s = dl.findStation(stationId);
                    d.currentLocation.latitude = s.lattitude;
                    d.currentLocation.longitude = s.longitude;
                }
                catch (Exception e)
                {
                    throw new BLIdUnExistsException("Error! The station dosen't exist");
                }
                IDAL.DO.Drone dr = droneDal(d);
                dl.addDrone(dr);
                DroneArr.Add(d);
            }
            catch(Exception e)
            {
                throw new BLIdExistsException("Error! the drone is already exist");
            }
        }

        /// <summary>
        /// update the name of the drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void updateNameDrone(int ID, string model)
        {
            try
            {
                if(model!="")
                {
                    int m = int.Parse(model);
                    dl.updateDrone(ID, m);
                    IBL.BO.DroneToList dr = DroneArr.Find(p => p.ID == ID);
                    DroneArr.Remove(dr);
                    dr.droneModel = int.Parse(model);
                    DroneArr.Add(dr);
                }
            }
            catch (Exception)
            {
                throw new BLIdUnExistsException("Error! the drone not found");
            }

        }

        public void releaseFromCharge(int id, int time)
        {
            //רוחמה זה לא טעות שלא עידכנתי את הרחפן שבדל אלא שבתרגיל הזה נדרשנו למחוק את השדה הזה מהיישות בדל
            IBL.BO.DroneToList d = DroneArr.Find(p => p.ID == id);
            
            if (d.status == IBL.BO.DroneStatus.maintenace)
            {
                //the time* charging rate per hour, couldnt be more then 100%
                d.battery += time * chargeCapacity[4];
                if (d.battery > 100)
                    d.battery = 100;

                d.status = IBL.BO.DroneStatus.available;

                // up the number of the empty charge slots
               IDAL.DO.DroneCharge tmp= myDalObject.findStationOfDroneCharge(id);
                IEnumerable<IDAL.DO.Station> tmpList = new List<IDAL.DO.Station>();
                tmpList = myDalObject.printAllStations();
                foreach (var item in tmpList)
                {
                    if (item.ID == tmp.stationeld)
                    {
                        IDAL.DO.Station s= new IDAL.DO.Station();
                        s.chargeSlots = item.chargeSlots+1;
                        s.ID = item.ID;
                        s.lattitude = item.lattitude;
                        s.longitude = item.longitude;
                        s.name = item.name;
                        myDalObject.updateStation(tmp.stationeld, s);
                    }     
                }
                //remove the drone frome the list of the droneCharge
                myDalObject.BatteryCharged(tmp);
            }
            else throw new BLgeneralException("Error! the drone dont was in charge");

        }

        public IBL.BO.Drone getBlDrone()
        {
           
        }
    }
}
