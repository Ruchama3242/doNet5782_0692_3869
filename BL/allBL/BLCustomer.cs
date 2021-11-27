using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BL;
using IBL;

namespace BL
{
    partial class BL : IBL.interfaceIBL
    {
        

        /// <summary>
        /// get a customer of BL and add it to the list of DAL
        /// </summary>
        /// <param name="customerBL"></param>
         public void addCustomer(IBL.BO.Customer customerBL)
        {
            try
            {
                if (customerBL.location.longitude < -180 || customerBL.location.longitude > 180)
                    throw new BLgeneralException("Error! the longitude is incorrect");
                if (customerBL.location.latitude < -90 || customerBL.location.latitude > 90)
                    throw new BLgeneralException("Error! the latitude is incorrect");
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp.ID = customerBL.ID;
                temp.name = customerBL.name;
                temp.phone = customerBL.phone;
                temp.lattitude = customerBL.location.latitude;
                temp.longitude = customerBL.location.longitude;
                myDalObject.addCustomer(temp);
            }
            catch(Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// update the name or the phone number of the customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public void updateCustomer(int id, string name, string phoneNum)
        {
            try
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp = myDalObject.findCustomer(id);

                //if the user want to change some detail....
                if (name != "")
                    temp.name = name;
                if (phoneNum != null)
                    temp.phone = phoneNum;

                myDalObject.updateCustomer(id, temp);
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// print all the list of the customer to list
        /// </summary>
        public IEnumerable<IBL.BO.CustomerToList> viewListCustomer()
        {
            //bring al the data from dal
            IEnumerable<IDAL.DO.Customer> lst = new List<IDAL.DO.Customer>();
            lst = myDalObject.getAllCustomers();

            List<IBL.BO.CustomerToList> listBL = new List<IBL.BO.CustomerToList>();
            foreach (var item in lst)
            {
                IBL.BO.CustomerToList c = new IBL.BO.CustomerToList();
                c.ID = item.ID;
                c.name = item.name;
                c.phone = item.phone;

                //
                var p = myDalObject.getAllParcels();
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

        /// <summary>
        /// get a id of customer and return a customer of BL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Customer findCustomer(int id)
        {
            try
            {
                IBL.BO.Customer cusBL = new IBL.BO.Customer();
                IDAL.DO.Customer cusDal = myDalObject.findCustomer(id);

                cusBL.ID = cusDal.ID;
                cusBL.location = new IBL.BO.Location();
                cusBL.location.latitude = cusDal.lattitude;
                cusBL.location.longitude = cusDal.longitude;

                
                IEnumerable<IDAL.DO.Parcel> lstP = myDalObject.getAllParcels();
                foreach (var item in lstP)
                {
                    //מוצא את כל החבילות שהלקוח מקבל
                    if (item.targetId == cusBL.ID)
                    {
                        IBL.BO.ParcelAtCustomer tmp =new IBL.BO.ParcelAtCustomer();
                        tmp.ID = item.ID;
                        tmp.priority = GetParcelPriorities(item.priority);
                        cusBL.fromCustomer.Add(tmp);
                    }
                    //מוצא את כל החבילות שהלקוח שולח
                    if (item.senderID == cusBL.ID)
                    {
                        IBL.BO.ParcelAtCustomer tmp = new IBL.BO.ParcelAtCustomer();
                        tmp.ID = item.ID;
                        tmp.priority = GetParcelPriorities(item.priority);
                        cusBL.toCustomer.Add(tmp);
                    }
                }
                return cusBL;
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// convert to the right type
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private IBL.BO.Priorities GetParcelPriorities(IDAL.DO.Priorities p)
        {

            if (p == IDAL.DO.Priorities.emergency)
                return IBL.BO.Priorities.emergency;
            if (p == IDAL.DO.Priorities.fast)
                return IBL.BO.Priorities.fast;
            if (p == IDAL.DO.Priorities.normal)
                return IBL.BO.Priorities.normal;

            //הוספתי עוד שורה רק כדי שהפונקציה תהיה חוקית
            return IBL.BO.Priorities.normal;
        }
    }
}
