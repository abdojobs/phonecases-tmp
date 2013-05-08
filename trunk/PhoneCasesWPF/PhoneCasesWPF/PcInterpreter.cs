using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneCases.Server;
using System.Net;
using System.Net.Sockets;

namespace PhoneCases.WPFGUI
{
    public class PcInterpreter : Interpreter
    {
        private int m_ownerId;
        

        public int OwnerId { get { return m_ownerId; } set { m_ownerId = value; } }

        public PcInterpreter(Receiver receiver, Transmitter transmitter) : base(receiver, transmitter)
        {
            //this.m_receiver.Parser.CaseCreated += OnCaseCreated;
        }

        public void OnCaseCreated(Parser.CaseCreatedHandler handler)
        {
            this.m_receiver.Parser.CaseCreated += handler;
        }
        public void Init(int ownerId)
        {
            m_ownerId = ownerId;
            m_receiver.Start();
            PairWithServer();
        }
        private void PairWithServer()
        {
            Client server = new Client(Properties.Settings.Default.ServerIP,(21337).ToString());
            m_transmitter.Send("98|" + m_ownerId + "|" + LocalIPAddress() + "|" + m_receiver.ListeningPort, server);
        }
        private string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
