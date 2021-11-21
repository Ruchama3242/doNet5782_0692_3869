﻿using System;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int ID { get; set; }
            public int model { get; set; }
            public WeightCategories weight { get; set; }
           // public DroneStatus status { get; set; }
            //public double battery { get; set; }

            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, model is: {model}, weight  is: {weight},\n";
                //result += $"status is: {status}, \n";
                //result += $"battery is: {battery}, \n";
                return result;
            }
        }
    }
}

 