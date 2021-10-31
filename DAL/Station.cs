using System;
using IDAL.DO;
namespace IDAL
{
    namespace DO
    {


        public struct Station
        {
            public int ID { get; set; }
            public string name { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }
            public int chargeSlots { get; set; }

            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, \n";
                result += $"Name is: {name}, \n";
                result+= $"Longitude is: {longitude}, \n";
                result += $"Latitude is: {lattitude}, \n";
                result += $"charge slolts is: {chargeSlots}, \n";
                return result;
            }
        }
    }
}