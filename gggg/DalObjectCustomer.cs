//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DalObject;

//namespace DAL
//{
//    sealed partial class DalObject : DalApi.IDal
//    {
//        public void addCustomer(DO.Customer temp)
//        {
//            bool flag = true;
//            foreach (DO.Customer item in DataSource.CustomersList)
//            {

//                if (item.ID == temp.ID) //return true if the field is the same
//                    flag = false;
//            }
//            if (flag)
//                DataSource.CustomersList.Add(temp);
//            else
//                throw new DO.IdExistsException();
//        }

//        public DO.Customer findCustomer(int id)
//        {
//            foreach (DO.Customer item in DataSource.CustomersList)
//            {
//                if (item.ID == id)
//                    return item;
//            }
//            throw new DO.IdUnExistsException("ERROR! the customer doesn't found");
//        }

//        /// <summary>
//        /// return list of the customer from type IEnumerable<Customer>
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<DO.Customer> getAllCustomers()
//        {
//            List<DO.Customer> lst = new List<DO.Customer>();
//            foreach (DO.Customer item in DataSource.CustomersList)
//                lst.Add(item);
//            return lst;
//        }

//        public void deleteSCustomer(int id)
//        {
//            foreach (DO.Customer item in DataSource.CustomersList)
//            {
//                if (item.ID == id)
//                {
//                    DataSource.CustomersList.Remove(item);
//                    return;
//                }
//            }
//            throw new DO.IdUnExistsException("ERROR! the customer doesn't found");
//        }

//        public void updateCustomer(int id, DO.Customer c)
//        {

//            for (int i = 0; i < DataSource.CustomersList.Count; i++)
//            {
//                if (DataSource.CustomersList[i].ID == id)
//                {
//                    DataSource.CustomersList[i] = c;
//                    return;
//                }
//            }
//            throw new DO.IdUnExistsException("ERROR! the customer doesn't found");
//        }
//    }
//}
