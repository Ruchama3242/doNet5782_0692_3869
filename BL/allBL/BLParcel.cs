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
        public void addParcel(int senderId,int targetId,int weight,int priority)
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
                dl.addParcel(p);
            
            
        }
    }
}
