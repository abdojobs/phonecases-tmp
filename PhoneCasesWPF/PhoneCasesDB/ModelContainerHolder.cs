﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.DB
{
    public static class ModelContainerHolder
    {
        private static ModelContainer m_modelContainer;

        public static ModelContainer Model
        {
            get
            {
                if (m_modelContainer == null)
                    return m_modelContainer = new ModelContainer();
                else
                    return m_modelContainer;
            }
        }
        public static void UpdateModel()
        {
            m_modelContainer.Dispose();
            m_modelContainer = new ModelContainer(); 
        }

        //Function should be placed on Cases
        public static int NewCase(string customerNumber,int ownerId, DateTime time)
        {
            try
            {
                Customers customer = Model.Customers.Where(a => a.PhoneNumber == customerNumber).FirstOrDefault();
                Users owner = Model.Users.Find(ownerId);
                if (customer == null)
                {
                    //Use "Okänd kund" and make the user choose from existing customers or create a new.
                    throw new Exception("Customer not found"); //Should notify pcuser and create new customer
                }
                    
                if (owner != null)
                {
                    try
                    {
                        Cases entity = Model.Cases.Add(new Cases() { Owner = owner, Customer = customer, StartTime = time });
                        Model.SaveChanges();
                        return entity.Id;
                    }
                    catch (InvalidCastException e)
                    {

                    }
                    catch (Exception e)
                    {

                    }
                }
                else
                    throw new Exception("Owner not found");
            }
            catch (Exception e)
            {
                //Error..
            }
            


            return -1;
            
        }
    }
}
