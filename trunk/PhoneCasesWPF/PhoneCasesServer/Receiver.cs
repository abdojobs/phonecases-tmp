using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Threading;
using System.Net;

using System.ComponentModel;
using System.Collections.ObjectModel;


using PhoneCases.DB;


namespace PhoneCases.Server
{
    public class Receiver
    {
        private int m_listenPort; //Port som TcpListener ska lyssna på
        private TcpListener m_listener; //Lyssnar efter TCP klienter
        private Thread m_listenThread; //Tråd som skapar ny tråd för varje ny klient
        private static object m_lock = new object();
        private volatile bool m_requestStop = false;
        private Parser m_parser;


        public int ListeningPort { get { return m_listenPort; } set { m_listenPort = value; } }
        public Parser Parser { get { return m_parser; } set { m_parser = value; } }


        public Receiver(Parser parser, int port = 21337)
        {
            m_listenPort = port;
            m_listener = new TcpListener(IPAddress.Any, m_listenPort);
            m_listenThread = new Thread(new ThreadStart(Listen));
            m_listenThread.Name = "ListenThread";

            m_parser = parser;
        }
        ~Receiver()
        {
            Kill();
        }
        public void Start()
        {
            if(!m_listenThread.IsAlive)
                m_listenThread.Start();
        }
        public void Kill()
        {
            m_requestStop = true;
            m_listener.Stop();
            m_listenThread.Abort();
        }
        protected void Listen()
        {
            m_listener.Start();
            while (!m_requestStop)
            {
                TcpClient client = m_listener.AcceptTcpClient();
                Thread clientThread = new Thread(HandleCommunication);
                clientThread.Start(client);
            }
            Console.WriteLine("STOP");
            m_listener.Stop();
            m_listenThread.Join();
        }
        protected void HandleCommunication(object p_client)
        {
            TcpClient client = (TcpClient)p_client;
            
            NetworkStream clientStream = client.GetStream();
            
            byte[] message = new byte[4096];
            int bytesRead = 0;
            ASCIIEncoding encoder = new ASCIIEncoding();
            //Console.WriteLine("Local" + client.Client.LocalEndPoint.ToString());
            //Console.WriteLine("Remote" + client.Client.RemoteEndPoint.ToString()); //THIS IS THE SHIT

            while (true)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //Socket error
                    break;
                }

                if (bytesRead == 0)
                {
                    //Disconnected
                    break;
                }

                string s = encoder.GetString(message).Remove(bytesRead);

                //FULFIX until androidclint can send ip.
                string[] strings = s.Split('|');
                if (strings[0] == "99")
                {
                    s=s.Replace("\n", "");
                    s += "|" + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                }
                //End of FULFIX

                m_parser.ParseMessage(s);
            }

            client.Close();
        }
    }
}
