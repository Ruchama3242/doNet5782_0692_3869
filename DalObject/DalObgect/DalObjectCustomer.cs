﻿using System;
using DalApi;
using System.Collections.Generic;

namespace Dal
{
    sealed partial class DalObject 
    {
        
            public void addCustomer(DO.Customer temp)
            {
                bool flag = true;
                foreach (DO.Customer item in DAL.DataSource.CustomersList)
                {

                    if (item.ID == temp.ID) //return true if the field is the same
                        flag = false;
                }
                if (flag)
                   DAL.DataSource.CustomersList.Add(temp);
                else
                {
                    DO.Customer c = findCustomer(temp.ID);
                    if (c.active == false)
                        c.active = true;
                    //throw new IdExistsException();
                }
            }

            public DO.Customer findCustomer(int id)
            {
                foreach (DO.Customer item in DAL.DataSource.CustomersList)
                {
                    if (item.ID == id && item.active == true)
                        return item;
                }
                throw new  DO.IdUnExistsException("ERROR! the customer doesn't found or not active");
            }

            /// <summary>
            /// return list of the customer from type IEnumerable<Customer>
            /// </summary>
            /// <returns></returns>
            public IEnumerable<DO.Customer> getAllCustomers()
            {
                List<DO.Customer> lst = new List<DO.Customer>();
                foreach (DO.Customer item in DAL.DataSource.CustomersList)
                    lst.Add(item);
                return lst;
            }

            public void deleteSCustomer(int id)
            {
                try
                {
                    DO.Customer c = findCustomer(id);
                    c.active = false;
                }
                catch (Exception e)
                {
                    throw new DO.generalException(e.Message, e);
                }
                //foreach (Customer item in DataSource.CustomersList)
                //{
                //    if (item.ID == id)
                //    {
                //        DataSource.CustomersList.Remove(item);
                //        return;
                //    }
                //}
                throw new DO.IdUnExistsException("ERROR! the customer doesn't found");
            }

            public void updateCustomer(int id, DO.Customer c)
            {
                try
                {
                    DO.Customer tmp = findCustomer(id);
                    tmp = c;
                }
                catch (Exception e)
                {
                    throw new DO.IdUnExistsException(e.Message, e);
                }
            }
        }
    }