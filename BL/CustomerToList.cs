using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class CustomerToList
        {
            public int ID;
            public string name;
            public string phone;
            public int sendAndDeliveredParcels;
            public int sendAndNotDeliveredParcels;
            public int gotParcels;
            public int onTheWayParcels;
            public override string ToString()
            {
               
            }
        }
    }
}
