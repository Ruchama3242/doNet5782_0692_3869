using System;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {

        public struct Customer
        {
            public int ID { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }

            public override string ToString()
            {
                String result = " ";
                result += $"ID is {ID}, \n";
                result += $"Name is {name}, \n";
                result += $"Telephone is 0 {phone.Substring(0, 2) + '-' + phone.Substring(2)}, \n";
                result += $"Lattitude is {lattitude}, \n";
                result += $"longitude is {longitude}, \n";
                return result;
            }

        }
    }
}
   