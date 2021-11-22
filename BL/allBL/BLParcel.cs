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
        public int addParcel(int senderId,int targetId,int weight,int priority)
        {
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

        public IEnumerable<IBL.BO.ParcelToList> getAllParcels()
        {
            List<IBL.BO.ParcelToList> lst = new List<IBL.BO.ParcelToList>();
            foreach (var item in dl.printAllParcels())
            {
                IBL.BO.ParcelToList p = new IBL.BO.ParcelToList();
                p.ID = item.ID;
                p.senderName = dl.findCustomer(item.ID).name;
                p.targetName = dl.findCustomer(item.ID).name;
                p.weight = (IBL.BO.WeightCategories)item.weight;
                p.priority = (IBL.BO.Priorities)item.priority;
                if (item.scheduled == DateTime.MinValue)
                    p.status = IBL.BO.ParcelStatus.created;
                else if (item.pickedUp == DateTime.MinValue)
                    p.status = IBL.BO.ParcelStatus.match;
                else if (item.delivered == DateTime.MinValue)
                    p.status = IBL.BO.ParcelStatus.pickedUp;
                else
                    p.status = IBL.BO.ParcelStatus.delivred;
                lst.Add(p);
            }
            return lst;
        }
    }
}
