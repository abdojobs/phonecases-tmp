using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class Interpreter
    {
        protected Receiver m_receiver;
        protected Transmitter m_transmitter;
        
        public Interpreter(Receiver receiver, Transmitter transmitter)
        {
            m_receiver = receiver;
            m_transmitter = transmitter;
        }
        public void StartReceiving()
        {
            m_receiver.Start();
        }
        public void Send(string data)
        {

        }
    }
}
