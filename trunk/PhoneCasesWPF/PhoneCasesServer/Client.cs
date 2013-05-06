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
        
        public Client(string ip, string port)
        {
            m_ip = ip;
            m_port = port;
        }

    }
}
