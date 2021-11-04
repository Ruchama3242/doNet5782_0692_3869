using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IDAL.DO.DalObject;
using IDAL.DO;

namespace DAL
{
    
    public interface IDal
    {
        public void addStations(Station temp);
        public void addDrone(Drone temp);
        public void addCustomer(Customer temp);
        public int addParcel(Parcel temp);
        public void ParcelDrone(int parcelID, int droneID);
        public void ParcelPickedUp(int parcelID, DateTime day);
        public void ParcelReceived(int parcelID, DateTime day);
        public DroneCharge SendToCharge(int DroneID, int StationID);
        public void BatteryCharged(DroneCharge dc);
        public Station printStation(int id);
        public Drone printDrone(int id);
        public Customer printCustomer(int id);
        public Parcel printParcel(int id);
        public IEnumerable<Station> printAllStations();
        public IEnumerable<Drone> printAllDrones();
        public IEnumerable<Customer> printAllCustomers();
        public IEnumerable<Parcel> printAllParcels();
        public IEnumerable<Parcel> printParcelsWithoutDrone();
        public IEnumerable<Station> printStationsWithChargeSlots();
        public void deleteStation(int id);
        public void deleteSParcel(int id);
        public void deleteDrone(int id);
        public void deleteSCustomer(int id);
        public void updateStation(int id, Station st);
        public void updateParcel(int id, Parcel p);
        public void updateDrone(int id, int mod);
        public void updateCustomer(int id, Customer c);

    }
}
