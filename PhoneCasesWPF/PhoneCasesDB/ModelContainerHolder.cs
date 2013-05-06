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

    }
}
