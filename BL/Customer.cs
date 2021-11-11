﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Customer
        {
            public int ID { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public Location location { get; set; }
            public List<ParcelAtCustomer> fromCustomer { get; set; }
            public List<ParcelAtCustomer> toCustomer { get; set; }
            public override string ToString()
            {
                String result = "";
                result += $"ID is: {ID}, Name is: {name}, Telephone is: {phone.Substring(0, 3) + '-' + phone.Substring(3)}," +
                    $"Lcation is: {location}\n";
                return result;
            }
        }
    }
}