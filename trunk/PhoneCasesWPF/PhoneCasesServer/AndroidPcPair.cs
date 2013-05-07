using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class AndroidPcPair
    {
        private string m_currentCaseId;
        private Client m_androidClient;
        private Client m_pcClient;

        public string CurrentCaseId { get { return m_currentCaseId; } set { m_currentCaseId = value; } }
        public Client Android { get { return m_androidClient; } set { m_androidClient = value; } }
        public Client Pc { get { return m_pcClient; } set { m_pcClient = value; } }

        public AndroidPcPair()
        {

        }
        public AndroidPcPair(Client android, Client pc)
        {
            m_androidClient = android;
            m_pcClient = pc;
        }



    }
}
