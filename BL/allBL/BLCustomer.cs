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
        /// print all the list of the customer to list
        /// </summary>
        public List<IBL.BO.CustomerToList> viewListCustomer()
        {
            //bring al the data from dal
            IEnumerable<IDAL.DO.Customer> lst = new List<IDAL.DO.Customer>();
            lst = myDalObject.printAllCustomers();

            List<IBL.BO.CustomerToList> listBL = new List<IBL.BO.CustomerToList>();
            foreach (var item in lst)
            {
                IBL.BO.CustomerToList c = new IBL.BO.CustomerToList();
                c.ID = item.ID;
                c.name = item.name;
                c.phone = item.phone;

                //
                var p = myDalObject.printAllParcels();
                int counterDelivered = 0;
                int counterDontDelivered = 0;
                int counterGot = 0;
                int counterOnWay = 0;
                foreach (var parcel in p)
                {
                    //count the parcel he got
                    if (parcel.senderID == item.ID)
                        counterGot++;
                    //if the parcel arrived
                    if (parcel.senderID == item.ID && parcel.delivered != DateTime.MinValue)
                        counterDelivered++;
                    //החבילה עוד לא שוייכה
                    if (parcel.senderID == item.ID && parcel.scheduled == DateTime.MinValue)
                        counterDontDelivered++;
                    // החבילה שוייכה לרחפן ועוד לא הגיעה
                    if (parcel.senderID == item.ID && parcel.scheduled != DateTime.MinValue
                    && parcel.delivered == DateTime.MinValue)
                        counterOnWay++;
                }
                c.gotParcels = counterGot;
                c.onTheWayParcels = counterOnWay;
                c.sendAndDeliveredParcels = counterDelivered;
                c.sendAndNotDeliveredParcels = counterDontDelivered;
                listBL.Add(c);
            }
            return listBL;
        }


    }
}
