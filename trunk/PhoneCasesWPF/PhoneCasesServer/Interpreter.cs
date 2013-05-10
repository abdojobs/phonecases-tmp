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
        ~Interpreter()
        {
            m_receiver.Kill();
            m_receiver.Parser = null;
            m_receiver = null;
            m_transmitter = null;
        }
        public void StartReceiving()
        {
            m_receiver.Start();
            m_receiver.Parser.IncomingCall += IncomingCall;
        }
        public void StopReceiving()
        {
            m_receiver.Kill();
        }

        protected virtual void IncomingCall(string ownerNumber, string callerNumber, string time)
        {
            throw new Exception();
        }
        

        public void Send(string data, Client to)
        {
            m_transmitter.Send(data, to);
        }
    }
}
