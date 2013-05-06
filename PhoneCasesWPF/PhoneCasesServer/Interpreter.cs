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
            m_receiver.Parser.AnsweredCall += AnsweredCall;
        }

        void AnsweredCall(object Params)
        {
 	        throw new NotImplementedException();
        }
        public void Send(string data, Client to)
        {
            m_transmitter.Send(data, to);
        }
    }
}
