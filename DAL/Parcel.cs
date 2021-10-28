using System;
using IDAL.DO;


namespace IDAL
{
    namespace DO
    {


        public struct Parcel
        {
            public int ID { get; set; }
            public int senderID { get; set; }
            public int targetId { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public DateTime requested { get; set; }
            public int droneID { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            //public override string ToString()
            //{
            //    String result = "";
            //    result += $"ID is {ID}, \n";
            //    result += $"sender ID is {senderid}, \n";
            //    result += $"target ID is {phone.Substring(0, 3) + '-' + phone.Substring(3)}, \n";
            //    result += $"Latitude is {Latitude}, \n";
            //    result += $"longitude is {Longitude}, \n";
            //    return result;
            //}

        }
    }
}
