using System;
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
                    //Make new "Okänd kund" with customerNumber and make it possible to change user if unkown. also add feature to unlock and edit customer on case.
                    //throw new Exception("Customer not found"); //Should notify pcuser and create new customer
                    int customerId = NewCustomer(customerNumber);
                    customer = Model.Customers.Find(customerId);
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
        public static int NewCustomer(string CustomerNumber, string CustomerName = "Okänd", int CompanyId = 0)
        {
            try
            {
                Companies company = Model.Companies.Find(CompanyId);
                Customers entity = Model.Customers.Add(new Customers() { PhoneNumber = CustomerNumber, Company=company, Name = CustomerName });
                Model.SaveChanges();
                return entity.Id;
            }
            catch (System.Exception e)
            {
            	//Error.
            }
            return -1;
        }
        public static int NewCompany(string CompanyName, int CompanyType = 0, int CompanyLocation = 0)
        {
            try
            {
                CompanyTypes type = Model.CompanyTypes.Find(CompanyType);
                Locations location = Model.Locations.Find(CompanyLocation);
                Companies entity = Model.Companies.Add(new Companies() { Name = CompanyName, CompanyType = type, Location = location });
                Model.SaveChanges();
                return entity.Id;
            }
            catch (System.Exception e)
            {
                //Error.
            }
            return -1;
        }
        public static int NewLocation(string LocationName)
        {
            try
            {
                Locations entity = Model.Locations.Add(new Locations() {Name = LocationName });
                Model.SaveChanges();
                return entity.Id;
            }
            catch (System.Exception e)
            {
                //Error.
            }
            return -1;
        }
    }
}
