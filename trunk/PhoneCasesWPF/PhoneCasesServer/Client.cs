using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class Client
    {
        private string m_ip;
        private string m_port;

        public string IP { get { return m_ip; } set { m_ip = value; } }
        public string Port { get { return m_port; } set { m_port = value; } }
        
        public Client(string ip, string port)
        {
            m_ip = ip;
            m_port = port;
        }

    }
}
