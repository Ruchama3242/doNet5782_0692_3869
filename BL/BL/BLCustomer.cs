using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BL;


namespace BL
{
   internal partial class BL 
    {
         public void addCustomer(Customer customerBL)
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

                DO.Customer temp = new DO.Customer();
                temp.ID = customerBL.ID;
                temp.name = customerBL.name;
                temp.phone = customerBL.phone;
                temp.lattitude = customerBL.location.latitude;
                temp.longitude = customerBL.location.longitude;
                dl.addCustomer(temp);
            }
            catch(Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

         public void updateCustomer(int id, string name, string phoneNum)
        {
            if (phoneNum != "" && (phoneNum.Length < 9 || phoneNum.Length > 10))
                throw new BLgeneralException("ERROR! the phone number must be with 9 or ten digits");
            
            try
            {
                DO.Customer temp = new DO.Customer();
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
                throw new BLgeneralException(e.Message,e);
            }
        }

        public IEnumerable<CustomerToList> viewListCustomer()
        {
            //bring al the data from dal
            IEnumerable<DO.Customer> lst = new List<DO.Customer>();
            lst = dl.getAllCustomers();

            List<CustomerToList> listBL = new List<CustomerToList>();
            foreach (var item in lst)
            {
                CustomerToList c = new CustomerToList();
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

        public Customer findCustomer(int id)
        {
            try
            {
                Customer cusBL = new Customer();
                DO.Customer cusDal = dl.findCustomer(id);

                cusBL.ID = cusDal.ID;
                cusBL.location = new Location();
                cusBL.location.latitude = cusDal.lattitude;
                cusBL.location.longitude = cusDal.longitude;
                cusBL.phone = cusDal.phone;
                cusBL.name = cusDal.name;
                
                
                IEnumerable<DO.Parcel> lstP = dl.getAllParcels();
                foreach (var item in lstP)
                {
                    //מוצא את כל החבילות שהלקוח מקבל
                    if (item.targetId == cusBL.ID)
                    {
                        ParcelAtCustomer tmp =new ParcelAtCustomer();
                        tmp.ID = item.ID;
                        tmp.status = getParcelStatus(item);
                        tmp.priority = GetParcelPriorities(item.priority);
                        tmp.senderOrTaget = new CustomerInParcel();
                        tmp.senderOrTaget.ID = item.senderID;
                        tmp.senderOrTaget.customerName = dl.findCustomer(item.senderID).name;
                        cusBL.toCustomer = new List<ParcelAtCustomer>();
                        cusBL.toCustomer.Add(tmp);
                    }
                    //מוצא את כל החבילות שהלקוח שולח
                    if (item.senderID == cusBL.ID)
                    {
                        ParcelAtCustomer tmp = new ParcelAtCustomer();
                        tmp.ID = item.ID;
                        tmp.status = getParcelStatus(item);
                        tmp.senderOrTaget = new CustomerInParcel();
                        tmp.senderOrTaget.ID = item.targetId;
                        tmp.senderOrTaget.customerName = dl.findCustomer(item.targetId).name;
                        tmp.priority = GetParcelPriorities(item.priority);
                        cusBL.fromCustomer = new List<ParcelAtCustomer>();
                        cusBL.fromCustomer.Add(tmp);
                    }
                }
                return cusBL;
            }
            catch (Exception e)
            {
                throw new BLgeneralException(e.Message, e);
            }
        }

         private Priorities GetParcelPriorities(DO.Priorities p)
        {

            if (p == DO.Priorities.emergency)
                return Priorities.Emergency;
            if (p == DO.Priorities.fast)
                return Priorities.Fast;
            if (p == DO.Priorities.normal)
                return Priorities.Normal;

            //הוספתי עוד שורה רק כדי שהפונקציה תהיה חוקית
            return Priorities.Normal;
        }


        private ParcelStatus getParcelStatus(DO.Parcel p)
        {
            if (p.scheduled == null && p.requested != null)
                return ParcelStatus.Created;
            if (p.pickedUp == null && p.scheduled != null)
                return ParcelStatus.Match;
            if (p.delivered == null && p.pickedUp != null)
                return ParcelStatus.PickedUp;
            return ParcelStatus.Delivred;
        }
    }
}
