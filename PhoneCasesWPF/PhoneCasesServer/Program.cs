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




namespace PhoneCases.Server
{
    public class Server
    {
        private int m_listenPort; //Port som TcpListener ska lyssna på
        private TcpListener m_listener; //Lyssnar efter TCP klienter
        private Thread m_listenThread; //Tråd som skapar ny tråd för varje ny klient
        private static object m_lock = new object();
        private volatile bool m_requestStop = false;

        public Server(int port = 21337)
        {
            m_listenPort = port;
            m_listener = new TcpListener(IPAddress.Any, m_listenPort);
            m_listenThread = new Thread(new ThreadStart(Listen));
            m_listenThread.Start();
            m_listenThread.Name = "ListenThread";
        }
        public void CreateTestCase()
        {
            //createCase("0531526116", "112", DateTime.Now.ToString());
        }
        public void Kill()
        {
            m_requestStop = true;

            m_listenThread.Abort();
            try
            {
                m_listener.Stop();
            }
            finally
            {

            }
        }
        private void Listen()
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
        private void HandleCommunication(object p_client)
        {
            TcpClient client = (TcpClient)p_client;
            
            NetworkStream clientStream = client.GetStream();
            
            byte[] message = new byte[4096];
            int bytesRead = 0;
            ASCIIEncoding encoder = new ASCIIEncoding();
            Console.WriteLine("Local" + client.Client.LocalEndPoint.ToString());
            Console.WriteLine("Remote" + client.Client.RemoteEndPoint.ToString()); //THIS IS THE SHIT

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
                parseMessage(s);
            }

            client.Close();
        }

        //MESSAGE PARSER -- Decides what to do with a message
        private void parseMessage(string message)
        {
            if (message != "" || message != null)
            {
                Console.WriteLine(message);
                string[] strings = message.Split('|');


                switch (strings[0])
                {
                    //New Incoming Call
                    case "00":
                        if (strings.Length == 4)
                        {
                            //change to Choose to answer or decline
                            createCase(strings[1], strings[2], strings[3]);
                            Console.WriteLine("00");
                        }
                        break;
                    //Answered Incoming Call
                    case "01":
                        if (true)
                        {
                            //Create case
                            Console.WriteLine("01");
                        }
                        break;
                    //End of Incoming Call
                    case "02":
                        if(true)
                        {
                            //Update endtime

                            Console.WriteLine("02");
                        }
                        break;
                }
            }
        }
        private void createCase(string inNumber, string myNumber, string startTime)
        {
            inNumber = inNumber.Replace("-", "");
            startTime = startTime.Replace("\n", "");
            NewCase(inNumber, myNumber, startTime);
        }
        private void NewCase(string inNumber, string ownerNumber, string startTime)
        {
            
        }
        static void Main()
        {
            
        }
    }
}
