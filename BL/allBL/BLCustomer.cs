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
   public partial class BL : IBL.interfaceIBL
    {
        

        /// <summary>
        /// get a customer of BL and add it to the list of DAL
        /// </summary>
        /// <param name="customerBL"></param>
         public void addCustomer(IBL.BO.Customer customerBL)
        {
            try
            {
                if (customerBL.location.longitude < 29.3 || customerBL.location.longitude > 33.5)
                    throw new BLgeneralException("Error! the longitude is incorrect");
                if (customerBL.location.latitude < 33.7 || customerBL.location.latitude > 36.3)
                    throw new BLgeneralException("Error! the latitude is incorrect");
                if (customerBL.ID < 100000000 || customerBL.ID > 999999999)
                    throw new BLgeneralException("ERROR! the id must be with 9 digits");
                if (customerBL.phone.Length < 9 || customerBL.phone.Length > 10)
                    throw new BLgeneralException("ERROR! the phone must be with 9 or 10 digits");

                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp.ID = customerBL.ID;
                temp.name = customerBL.name;
                temp.phone = customerBL.phone;
                temp.lattitude = customerBL.location.latitude;
                temp.longitude = customerBL.location.longitude;
                dl.addCustomer(temp);
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
            if (phoneNum != "" && (phoneNum.Length < 9 || phoneNum.Length > 10))
                throw new BLgeneralException("ERROR! the phone number must be with 9 or ten digits");
            
            try
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp = dl.findCustomer(id);

                //if the user want to change some detail....
                if (name != "")
                    temp.name = name;
                if (phoneNum != "")
                    temp.phone = phoneNum;

                dl.updateCustomer(id, temp);
            }
            catch (Exception e)
            {
                throw new BLgeneralException($"{e}");
            }
        }

        /// <summary>
        /// print all the list of the customerToList
        /// </summary>
        public IEnumerable<IBL.BO.CustomerToList> viewListCustomer()
        {
            //bring al the data from dal
            IEnumerable<IDAL.DO.Customer> lst = new List<IDAL.DO.Customer>();
            lst = dl.getAllCustomers();

            List<IBL.BO.CustomerToList> listBL = new List<IBL.BO.CustomerToList>();
            foreach (var item in lst)
            {
                IBL.BO.CustomerToList c = new IBL.BO.CustomerToList();
                c.ID = item.ID;
                c.name = item.name;
                c.phone = item.phone;

                //
                var p = dl.getAllParcels();
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
                    if (parcel.senderID == item.ID && parcel.delivered != null)
                        counterDelivered++;
                    //החבילה עוד לא שוייכה
                    if (parcel.senderID == item.ID && parcel.scheduled == null)
                        counterDontDelivered++;
                    // החבילה שוייכה לרחפן ועוד לא הגיעה
                    if (parcel.senderID == item.ID && parcel.scheduled != null && parcel.delivered == null)
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
                IDAL.DO.Customer cusDal = dl.findCustomer(id);

                cusBL.ID = cusDal.ID;
                cusBL.location = new IBL.BO.Location();
                cusBL.location.latitude = cusDal.lattitude;
                cusBL.location.longitude = cusDal.longitude;
                cusBL.phone = cusDal.phone;
                cusBL.name = cusDal.name;
                
                
                IEnumerable<IDAL.DO.Parcel> lstP = dl.getAllParcels();
                foreach (var item in lstP)
                {
                    //מוצא את כל החבילות שהלקוח מקבל
                    if (item.targetId == cusBL.ID)
                    {
                        IBL.BO.ParcelAtCustomer tmp =new IBL.BO.ParcelAtCustomer();
                        tmp.ID = item.ID;
                        tmp.status = getParcelStatus(item);
                        tmp.priority = GetParcelPriorities(item.priority);
                        tmp.senderOrTaget = new IBL.BO.CustomerInParcel();
                        tmp.senderOrTaget.ID = item.senderID;
                        tmp.senderOrTaget.customerName = dl.findCustomer(item.senderID).name;
                        cusBL.toCustomer = new List<IBL.BO.ParcelAtCustomer>();
                        cusBL.toCustomer.Add(tmp);
                    }
                    //מוצא את כל החבילות שהלקוח שולח
                    if (item.senderID == cusBL.ID)
                    {
                        IBL.BO.ParcelAtCustomer tmp = new IBL.BO.ParcelAtCustomer();
                        tmp.ID = item.ID;
                        tmp.status = getParcelStatus(item);
                        tmp.senderOrTaget = new IBL.BO.CustomerInParcel();
                        tmp.senderOrTaget.ID = item.targetId;
                        tmp.senderOrTaget.customerName = dl.findCustomer(item.targetId).name;
                        tmp.priority = GetParcelPriorities(item.priority);
                        cusBL.fromCustomer = new List<IBL.BO.ParcelAtCustomer>();
                        cusBL.fromCustomer.Add(tmp);
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


        private IBL.BO.ParcelStatus getParcelStatus(IDAL.DO.Parcel p)
        {
            if (p.scheduled == null && p.requested != null)
                return IBL.BO.ParcelStatus.created;
            if (p.pickedUp == null && p.scheduled != null)
                return IBL.BO.ParcelStatus.match;
            if (p.delivered == null && p.pickedUp != null)
                return IBL.BO.ParcelStatus.pickedUp;
            return IBL.BO.ParcelStatus.delivred;
        }
    }
}
