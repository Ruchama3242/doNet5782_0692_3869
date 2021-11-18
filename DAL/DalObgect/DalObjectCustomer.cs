using System;
using IDAL.DO;
using DAL;
using System.Collections.Generic;
using System.Collections;
namespace IDAL

{
    namespace DO
    {
        namespace DalObject
        {
            public partial class DalObject : IDal
            {
                /// <summary>
                /// add customer to the array
                /// </summary>
                /// <param name="temp"></param>
                public void addCustomer(Customer temp)
                {
                    bool flag = true;
                    foreach (Customer item in DataSource.CustomersList)
                    {

                        if (item.Equals(temp.ID)) //return true if the field is the same
                            flag = false;
                    }
                    if (flag)
                        DataSource.CustomersList.Add(temp);
                    else
                        throw new IdExistsException();
                }

                /// <summary>
                /// print a customer
                /// </summary>
                /// <param name="id"></param>
                public Customer printCustomer(int id)
                {
                    foreach (Customer item in DataSource.CustomersList)
                    {
                        if (item.Equals(id))
                            return item;
                    }
                    throw new IdUnExistsException("ERROR! the customer doesn't found");
                }

                /// <summary>
                /// print all customers
                /// </summary>
                public IEnumerable<Customer> printAllCustomers()
                {
                    List<Customer> lst = new List<Customer>();
                    foreach (Customer item in DataSource.CustomersList)
                        lst.Add(item);
                    return lst;
                }

                /// <summary>
                /// delete a customer from the list
                /// </summary>
                /// <param name="id"></param>
                public void deleteSCustomer(int id)
                {
                    foreach (Customer item in DataSource.CustomersList)
                    {
                        if (item.ID == id)
                        {
                            DataSource.CustomersList.Remove(item);
                            return;
                        }
                    }
                    throw new IdUnExistsException("ERROR! the customer doesn't found");
                }

                /// <summary>
                /// update the details of a customer
                /// </summary>
                /// <param name="id"></param>
                /// <param name="c"></param>
                public void updateCustomer(int id, Customer c)
                {
                    for (int i = 0; i < DataSource.CustomersList.Count; i++)
                    {
                        if (DataSource.CustomersList[i].ID == id)
                        {
                            DataSource.CustomersList[i] = c;
                            return;
                        }
                    }
                    throw new IdUnExistsException("ERROR! the customer doesn't found");
                }
            }
        }
    }
}
