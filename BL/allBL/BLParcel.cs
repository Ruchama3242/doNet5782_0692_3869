using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL;
namespace BL
{
    partial class BL : InterfaceIBL
    {
        /// <summary>
        /// add a parcel to the list in the DAL
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public int addParcel(int senderId,int targetId,int weight,int priority)
        {
            try//check if the sender and the target are exists
            {
                 var s=dl.findCustomer(senderId);
               var t= dl.findCustomer(targetId);
            }
            catch (Exception e)
            {

                throw new BLgeneralException(e.Message);
            }

            try
            {
                IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                p.senderID = senderId;
                p.targetId = targetId;
                p.weight = (IDAL.DO.WeightCategories)weight;
                p.priority = (IDAL.DO.Priorities)priority;
                p.droneID = 0;
                p.requested = DateTime.Now;
                p.pickedUp = DateTime.MinValue;
                p.scheduled = DateTime.MinValue;
                p.delivered = DateTime.MinValue;
                int id = dl.addParcel(p);
                return id;
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// return the list of all parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.ParcelToList> getAllParcels()
        {
            List<IBL.BO.ParcelToList> lst = new List<IBL.BO.ParcelToList>();//create the list
            foreach (var item in dl.getAllParcels())//pass on the list of the parcels and copy them to the new list
            {
                lst.Add(getParcelTolist(item));
            }
            return lst;
        }

        /// <summary>
        /// return a parcel with all details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.parcel getParcel(int id)
        {
            try
            {
                IDAL.DO.Parcel p = dl.findParcel(id);//find the parcl in the list in the dal
                IBL.BO.parcel pb = new IBL.BO.parcel();//create a parcel of BL type
                pb.ID = p.ID;
                pb.weight = (IBL.BO.WeightCategories)p.weight;
                pb.priority = (IBL.BO.Priorities)p.priority;
                pb.delivered = p.delivered;
                pb.pickedUp = p.pickedUp;
                pb.requested = p.requested;
                pb.scheduled = p.scheduled;
                if (p.droneID != 0)//if there is a drone to the parcel
                {
                    IBL.BO.DroneToList d = DroneArr.Find(x => x.ID == p.droneID);//find the drone in the dron list
                    pb.drone = new IBL.BO.DroneInParcel();
                    pb.drone.ID = d.ID;
                    pb.drone.battery = d.battery;
                    pb.drone.currentLocation = new IBL.BO.Location();
                    pb.drone.currentLocation = d.currentLocation;
                }
                IDAL.DO.Customer sender = dl.findCustomer(p.senderID);//find the sender customer in the list in the dal
                IDAL.DO.Customer target = dl.findCustomer(p.targetId);//find the target customer in the list in the dal
                pb.sender = new IBL.BO.CustomerInParcel();
                pb.sender.ID = sender.ID;
                pb.sender.customerName = sender.name;
                pb.target = new IBL.BO.CustomerInParcel();
                pb.target.customerName = target.name;
                pb.target.ID = target.ID;
                return pb;
            }
            catch (Exception)
            {
                throw new BLIdUnExistsException("ERROR! the parcel  not found");
            }
        }

        /// <summary>
        /// return a list of parcels that have not a drone yet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.ParcelToList> parcelsWithoutDrone()
        {
            IEnumerable<IDAL.DO.Parcel> pd = dl.getParcelsWithoutDrone();
            List<IBL.BO.ParcelToList> lst = new List<IBL.BO.ParcelToList>();
            foreach (var item in pd)
            {
                lst.Add(getParcelTolist(item));
            }
            return lst;
        }

        /// <summary>
        /// the method get a parcel of DAL type and return a parcel of parcelToList type
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private IBL.BO.ParcelToList getParcelTolist(IDAL.DO.Parcel item)
        {
            IBL.BO.ParcelToList p = new IBL.BO.ParcelToList();
            p.ID = item.ID;
            p.senderName = dl.findCustomer(item.ID).name;
            p.targetName = dl.findCustomer(item.ID).name;
            p.weight = (IBL.BO.WeightCategories)item.weight;
            p.priority = (IBL.BO.Priorities)item.priority;
            if (item.scheduled == DateTime.MinValue)//check what is the status of the parcel
                p.status = IBL.BO.ParcelStatus.created;
            else if (item.pickedUp == DateTime.MinValue)
                p.status = IBL.BO.ParcelStatus.match;
            else if (item.delivered == DateTime.MinValue)
                p.status = IBL.BO.ParcelStatus.pickedUp;
            else
                p.status = IBL.BO.ParcelStatus.delivred;
            return p;
        }
        /// <summary>
        /// the function update the parcel to be collected by the drone
        /// </summary>
        /// <param name="droneid"></param>
        public void packageCollection(int droneid)
        {
            var d = DroneArr.Find(x => x.ID == droneid);
            if (d == null)
                throw new BLgeneralException("Error! the drone not found");
            foreach (var item in dl.getAllParcels())
            {
                if(item.droneID==droneid)
                {
                    if(item.pickedUp==DateTime.MinValue)
                    {
                        DroneArr.Remove(d);
                        d.battery = d.battery - distance(d.currentLocation, new IBL.BO.Location { latitude = dl.findCustomer(item.senderID).lattitude, longitude = dl.findCustomer(item.senderID).longitude })*chargeCapacity[0];
                        d.currentLocation.longitude = dl.findCustomer(item.senderID).longitude;
                        d.currentLocation.latitude = dl.findCustomer(item.senderID).lattitude;
                        var par = item;
                        dl.deleteSParcel(item.ID);
                        par.pickedUp = DateTime.Now;
                        dl.addParcel(par);
                        DroneArr.Add(d);
                        return;
                    }
                }
            }
            throw new BLgeneralException("ERROR! the parcel can't be collected");
        }

        /// <summary>
        /// The function update the parcel to be delivered
        /// </summary>
        /// <param name="droneid"></param>
        public void packageDelivery(int droneid)
        {
            var d = DroneArr.Find(x => x.ID == droneid);
            if (d == null)
                throw new BLgeneralException("Error! the drone not found");
            foreach (var item in dl.getAllParcels())
            {
                if (item.droneID == droneid)
                {
                    if (item.pickedUp != DateTime.MinValue&&item.delivered==DateTime.MinValue)
                    {
                        DroneArr.Remove(d);
                        d.battery = d.battery - distance(d.currentLocation, new IBL.BO.Location { latitude = dl.findCustomer(item.targetId).lattitude, longitude = dl.findCustomer(item.targetId).longitude }) * chargeCapacity[indexOfChargeCapacity(item.weight)];
                        d.currentLocation.longitude = dl.findCustomer(item.targetId).longitude;
                        d.currentLocation.latitude = dl.findCustomer(item.targetId).lattitude;
                        d.status = IBL.BO.DroneStatus.available;
                        var par = item;
                        dl.deleteSParcel(item.ID);
                        par.delivered = DateTime.Now;
                        dl.addParcel(par);
                        DroneArr.Add(d);
                        return;
                    }
                }
            }
            throw new BLgeneralException("ERROR! the parcel can't be collected");
        }
    }
}
