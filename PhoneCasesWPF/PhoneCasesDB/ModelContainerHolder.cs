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
        public static void NewCase(string pCustomer,string pOwner, string pTime)
        {
            Customers customer = Model.Customers.Where(a => a.PhoneNumber == pCustomer).First();
            Users owner = Model.Users.Where(a => a.PhoneNumber == pOwner).First();

            if (customer != null)
            {
                if (owner != null)
                {
                    try{
                        Model.Cases.Add(new Cases() { Owner = owner, Customer = customer, StartTime = DateTime.Parse(pTime) });
                    }
                    catch(InvalidCastException e){
                        
                    }
                    catch(Exception e)
                    {
                        
                    }
                }
            }
            
        }
    }
}
