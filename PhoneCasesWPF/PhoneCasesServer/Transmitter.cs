using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PhoneCases.Server
{
    public class Transmitter
    {
        private TcpClient m_tcpClient = new TcpClient();
        private NetworkStream m_networkStream = new NetworkStream();

        public Transmitter()
        {
            
        }
        public void Send(string data, Client to)
        {
            //Make asyncronous
            m_tcpClient.Connect(to.IP, int.Parse(to.Port));
            m_networkStream = m_tcpClient.GetStream();
            byte[] message = new byte[4096];
            ASCIIEncoding encoder = new ASCIIEncoding();
            message = encoder.GetBytes(data);
            m_networkStream.Write(message, 0, 4096);

            m_networkStream.Close();
            m_tcpClient.Close();

        }
    }
}
