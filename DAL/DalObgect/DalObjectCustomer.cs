using System;
using DO;
using DAL;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


    namespace DO
    {
        namespace DalObject
        {
            partial class DalObject 
            {
                public void addCustomer(Customer temp)
                {
                    bool flag = true;
                    foreach (Customer item in DataSource.CustomersList)
                    {

                        if (item.ID==temp.ID) //return true if the field is the same
                            flag = false;
                    }
                    if (flag)
                        DataSource.CustomersList.Add(temp);
                    else
                        throw new IdExistsException();
                }

                public Customer findCustomer(int id)
                {
                    foreach (Customer item in DataSource.CustomersList)
                    {
                        if (item.ID==id)
                            return item;
                    }
                    throw new IdUnExistsException("ERROR! the customer doesn't found");
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
