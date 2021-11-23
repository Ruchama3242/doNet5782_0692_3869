using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class Drone
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
                String result = "";
                result += $"ID is: {ID}, model: {model}, weight categories: {weight}, drone status is:{status}, " +
                    $" {battery} on battery, parcel in transfer:{parcel}" +
                    $" Lcation is: {location}\n";
                return result;

            }
        }
    }
}
