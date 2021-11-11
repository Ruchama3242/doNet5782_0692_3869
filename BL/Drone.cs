using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Drone
        {
            public int ID { get; set; }
            public int model { get; set; }
            public WeightCategories weight { get; set; }
            public DroneStatus status { get; set; }
            public double battery { get; set; }
            public ParcelInTransfer parcel { get; set; }
            public Location location { get; set; }

            public override string ToString()
            {
               
            }
        }
    }
}
