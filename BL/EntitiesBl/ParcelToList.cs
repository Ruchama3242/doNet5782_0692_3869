using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
         public class ParcelToList
        {
            public int ID { get; set; }
            public string senderName { get; set; }
            public string targetName { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public ParcelStatus status { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, status is: {status},  " +
                          $"weight is: {weight}, priority is: {priority}, \n";
                         
                return result;
            }
        }
    }
}
