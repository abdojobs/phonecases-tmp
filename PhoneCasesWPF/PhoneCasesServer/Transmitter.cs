﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
//using System.IO;

namespace PhoneCases.Server
{
    public class Transmitter
    {
        private TcpClient m_tcpClient;
        private NetworkStream m_networkStream;

        public Transmitter()
        {
            
        }
        public void Send(string data, Client to)
        {
            //Make asyncronous
            try
            {
                m_tcpClient = new TcpClient();
                m_tcpClient.Connect(to.IP, int.Parse(to.Port));
                m_networkStream = m_tcpClient.GetStream();
                byte[] message = new byte[4096];
                ASCIIEncoding encoder = new ASCIIEncoding();
                message = encoder.GetBytes(data);
                m_networkStream.Write(message, 0, message.Length);
                m_networkStream.Flush();
                m_networkStream.Close();
                m_tcpClient.Close();
                m_tcpClient = null;
            }
            catch (Exception e)
            {
            	//Error..
            }
            

        }
    }
}
