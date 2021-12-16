using System;
using DO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;



namespace DO
{
    namespace DalObject
    {
        sealed partial class DalObject
        {
            public void addCustomer(Customer temp)
            {
                bool flag = true;
                foreach (Customer item in DataSource.CustomersList)
                {

                    if (item.ID == temp.ID) //return true if the field is the same
                        flag = false;
                }
                if (flag)
                    DataSource.CustomersList.Add(temp);
                else
                {
                    Customer c = findCustomer(temp.ID);
                    if (c.active == false)
                        c.active = true;
                    //throw new IdExistsException();
                }
            }

            public Customer findCustomer(int id)
            {
                foreach (Customer item in DataSource.CustomersList)
                {
                    if (item.ID == id && item.active == true)
                        return item;
                }
                throw new IdUnExistsException("ERROR! the customer doesn't found or not active");
            }

            /// <summary>
            /// return list of the customer from type IEnumerable<Customer>
            /// </summary>
            /// <returns></returns>
            public IEnumerable<Customer> getAllCustomers()
            {
                List<Customer> lst = new List<Customer>();
                foreach (Customer item in DataSource.CustomersList)
                    lst.Add(item);
                return lst;
            }

            public void deleteSCustomer(int id)
            {
                try
                {
                    Customer c = findCustomer(id);
                    c.active = false;
                }
                catch (Exception e)
                {
                    throw new generalException(e.Message, e);
                }
                //foreach (Customer item in DataSource.CustomersList)
                //{
                //    if (item.ID == id)
                //    {
                //        DataSource.CustomersList.Remove(item);
                //        return;
                //    }
                //}
                throw new IdUnExistsException("ERROR! the customer doesn't found");
            }

            public void updateCustomer(int id, Customer c)
            {
                try
                {
                    Customer tmp = findCustomer(id);
                    tmp = c;
                }
                catch (Exception e)
                {
                    throw new IdUnExistsException(e.Message, e);
                }
            }
        }
    }
}
