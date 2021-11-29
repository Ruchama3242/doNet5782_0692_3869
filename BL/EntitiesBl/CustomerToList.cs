using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
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
                String result = "";
                result += $"ID: {ID}, Name: {name}, phone number: {phone.Substring(0, 3) + '-' + phone.Substring(3)}, {sendAndDeliveredParcels} Packages were successfully delivered" +
                    $", {sendAndNotDeliveredParcels} Packages sending , {gotParcels} Packages received, {onTheWayParcels} on the way'\n";
                return result;
            }
        }
    }
}
