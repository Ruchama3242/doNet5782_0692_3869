using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BL;

namespace BL
{
    partial class BL
    {
        IDAL.DO.DalObject.DalObject myDalObject;
        /// <summary>
        /// get a customer of BL and add it to the list of DAL
        /// </summary>
        /// <param name="customerBL"></param>
         public void addCustomer(IBL.BO.Customer customerBL)
        {
            IDAL.DO.Customer temp = new IDAL.DO.Customer(); 
            temp.ID = customerBL.ID;
            temp.name = customerBL.name;
            temp.phone = customerBL.phone;
            temp.lattitude = customerBL.location.latitude;
            temp.longitude = customerBL.location.longitude;
            myDalObject.addCustomer(temp);
        }
        /// <summary>
        /// update the name or the phone number of the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public void updateCustomer(int id, string name, string phoneNum)
        {
            IDAL.DO.Customer temp = new IDAL.DO.Customer();
            temp =myDalObject.findCustomer(id);

            //if the user want to change some detail....
            if (name != "")
                temp.name = name;
            if (phoneNum != null)
                temp.phone = phoneNum;

            myDalObject.updateCustomer(id, temp);
        }

        /// <summary>
        /// print all the list of the customer
        /// </summary>
        public void viewListCustomer()
        {
            myDalObject.printAllCustomers();
        }
    }
}
