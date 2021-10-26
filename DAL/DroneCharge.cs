using System;
using IDAL.DO;


namespace IDAL
{
    namespace DO
    {

        public struct DroneCharge
        {
            public int droneID { get; set; }
            public int stationeld { get; set; }

            public override string ToString()
            {
                String result = " ";
                result += $" drone ID is {droneID}, \n";
                result += $"stationeld is {stationeld}, \n";
                return result;
            }
        }
    }
}