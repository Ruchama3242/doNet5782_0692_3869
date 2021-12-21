using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
   internal partial class BL 
    {
        
        public int addParcel(int senderId,int targetId,int weight,int priority)
        {
            try//check if the sender and the target are exists
            {
                 var s=dl.findCustomer(senderId);
               var t= dl.findCustomer(targetId);
            }
            catch (Exception e)
            {

                throw new BLgeneralException(e.Message/*,e*/);
            }

            try
            {
                DO.Parcel p = new DO.Parcel();
                p.senderID = senderId;
                p.targetId = targetId;
                p.weight = (DO.WeightCategories)weight;
                p.priority = (DO.Priorities)priority;
                p.droneID = 0;
                p.requested = DateTime.Now;
                p.pickedUp = null;
                p.scheduled = null;
                p.delivered = null;
                int id = dl.addParcel(p);
                return id;
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

         public IEnumerable<ParcelToList> getAllParcels()
        {
            List<ParcelToList> lst = new List<ParcelToList>();//create the list
            foreach (var item in dl.getAllParcels())//pass on the list of the parcels and copy them to the new list
            {
                lst.Add(getParcelTolist(item));
            }
            return lst;
        }

        public Parcel findParcel(int id)
        {
            try
            {
                DO.Parcel p = dl.findParcel(id);//find the parcl in the list in the dal
                Parcel pb = new Parcel();//create a parcel of BL type
                pb.ID = p.ID;
                pb.weight = (WeightCategorie)p.weight;
                pb.priority = (Priorities)p.priority;
                pb.delivered = p.delivered;
                pb.pickedUp = p.pickedUp;
                pb.requested = p.requested;
                pb.scheduled = p.scheduled;
                if (p.droneID != 0)//if there is a drone to the parcel
                {
                    DroneToList d = DroneArr.Find(x => x.ID == p.droneID);//find the drone in the dron list
                    pb.drone = new DroneInParcel();
                    pb.drone.ID = d.ID;
                    pb.drone.battery = d.battery;
                    pb.drone.currentLocation = new Location();
                    pb.drone.currentLocation = d.currentLocation;
                }
                DO.Customer sender = dl.findCustomer(p.senderID);//find the sender customer in the list in the dal
                DO.Customer target = dl.findCustomer(p.targetId);//find the target customer in the list in the dal
                pb.sender = new CustomerInParcel();
                pb.sender.ID = sender.ID;
                pb.sender.customerName = sender.name;
                pb.target = new CustomerInParcel();
                pb.target.customerName = target.name;
                pb.target.ID = target.ID;
                return pb;
            }
            catch (Exception e)
            {
                throw new BLIdUnExistsException("ERROR! the parcel  not found", e);
            }
        }

        public IEnumerable<ParcelToList> parcelsWithoutDrone()
        {
            IEnumerable<DO.Parcel> pd = dl.getParcelsWithoutDrone();
            List<ParcelToList> lst = new List<ParcelToList>();
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
        private ParcelToList getParcelTolist(DO.Parcel item)
        {
            ParcelToList p = new ParcelToList();
            p.ID = item.ID;
            p.senderName = dl.findCustomer(item.senderID).name;
            p.targetName = dl.findCustomer(item.targetId).name;
            p.weight = (WeightCategorie)item.weight;
            p.priority = (Priorities)item.priority;
            if (item.scheduled == null)//check what is the status of the parcel
                p.status = ParcelStatus.Created;
            else if (item.pickedUp == null)
                p.status = ParcelStatus.Match;
            else if (item.delivered == null)
                p.status = ParcelStatus.PickedUp;
            else
                p.status = ParcelStatus.Delivred;
            return p;
        }
        
        public void packageCollection(int droneid)
        {
            var d = DroneArr.Find(x => x.ID == droneid);
            if (d == null)
                throw new BLgeneralException("Error! the drone not found");
            foreach (var item in dl.getAllParcels())
            {
                if(item.droneID==droneid)
                {
                    if(item.pickedUp==null)
                    {
                        DroneArr.Remove(d);
                        d.battery = d.battery - distance(d.currentLocation, new Location { latitude = dl.findCustomer(item.senderID).lattitude, longitude = dl.findCustomer(item.senderID).longitude })*chargeCapacity[0];
                        d.currentLocation.longitude = dl.findCustomer(item.senderID).longitude;
                        d.currentLocation.latitude = dl.findCustomer(item.senderID).lattitude;
                        
                        dl.ParcelPickedUp(item.ID, DateTime.Now);
                        DroneArr.Add(d);
                        return;
                    }
                }
            }
            throw new BLgeneralException("ERROR! the parcel can't be collected");
        }

        public void packageDelivery(int droneid)
        {
            var d = DroneArr.Find(x => x.ID == droneid);
            if (d == null)
                throw new BLgeneralException("Error! the drone not found");
            foreach (var item in dl.getAllParcels())
            {
                if (item.droneID == droneid)
                {
                    if (item.pickedUp != null && item.delivered == null) 
                    {
                        DroneArr.Remove(d);
                        d.battery = d.battery - distance(d.currentLocation, new Location { latitude = dl.findCustomer(item.targetId).lattitude, longitude = dl.findCustomer(item.targetId).longitude }) * chargeCapacity[indexOfChargeCapacity(item.weight)];
                        d.currentLocation.longitude = dl.findCustomer(item.targetId).longitude;
                        d.currentLocation.latitude = dl.findCustomer(item.targetId).lattitude;
                        d.status = DroneStatus.Available;
                        d.parcelNumber = 0;
                        dl.ParcelReceived(item.ID, DateTime.Now);
                        //var par = item;
                        //dl.deleteSParcel(item.ID);
                        //par.delivered = DateTime.Now;
                        //dl.addParcel(par);
                        DroneArr.Add(d);
                        return;
                    }
                }
            }
            throw new BLgeneralException("ERROR! the parcel can't be delivered");
        }

        public void deleteParcel(int id)
        {
            dl.deleteSParcel(id);
        }
    }
}
