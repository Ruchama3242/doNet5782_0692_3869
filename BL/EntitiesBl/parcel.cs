using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class parcel
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

            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, sender ID is: {sender}, " +
                          $"drone ID is: {drone}, target ID is: {target}, " +
                          $"weight is: {weight}, priority is: {priority}, " +
                          $"requested date is: {requested}, schedueld date ID is: {scheduled}," +
                          $" picke up date  is: {pickedUp}, delivered date is: {delivered}\n";
                result += $"sender ID is: {sender}, \n";
                result += $"drone ID is: {drone}, \n";
                result += $"target ID is: {target}, \n";
                result += $"weight is: {weight}, \n";
                result += $"priority is: {priority}, \n";
                result += $"requested date is: {requested}, \n";
                result += $"schedueld date ID is: {scheduled}, \n";
                result += $"picke up date  is: {pickedUp}, \n";
                result += $"delivered date is: {delivered}, \n";
                return result;
            }
        }
    }
}
