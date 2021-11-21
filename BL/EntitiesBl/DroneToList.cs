using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneToList
        {
            public int ID { get; set; }
            public int droneModel { get; set; }
            public WeightCategories weight { get; set; }
            public double battery { get; set; }
            public DroneStatus status { get; set; }
            public Location currentLocation { get; set; }
            public int parcelNumber { get; set; }
            //public override string ToString()
            //{
                
            //}
        }
    }
}
