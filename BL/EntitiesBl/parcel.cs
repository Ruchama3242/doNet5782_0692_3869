using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class parcel
        {
            public int ID { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel target { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public DateTime requested { get; set; }//יצירת החבילה
            public DroneInParcel drone { get; set; }
            public DateTime scheduled { get; set; }//שיוך
            public DateTime pickedUp { get; set; }//איסוף
            public DateTime delivered { get; set; }//אספקה
        }
    }
}
