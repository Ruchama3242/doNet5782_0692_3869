using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelInTransfer
        {
            public int ID { get; set; }
            public bool status { get; set; }//true if its on the way to the target and false if its waiting for collection
            public Priorities priority { get; set; }
            public WeightCategories weight { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel target { get; set; }
            public Location collectionLocation { get; set; }
            public Location targetLocation { get; set; }
            public double distance { get; set; }
            public override string ToString()
            {
                
            }
        }
    }
}
