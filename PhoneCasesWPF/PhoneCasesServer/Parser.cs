using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class Parser
    {
        public delegate void PhonePairRequestHandler(string ip, string port, string phoneNumber);
        public delegate void PcPairRequestHandler(string ip, string port, string userId);
        public delegate void IncomingCallHandler(string callerNumber, string ownerNumber, string time);
        public delegate void AnsweredCallHandler(object Params);
        public delegate void EndOfCallHandler(object Params);


        public event PhonePairRequestHandler PhonePairRequest;
        public event PcPairRequestHandler PcPairRequest;
        public event IncomingCallHandler IncomingCall;
        public event AnsweredCallHandler AnsweredCall;
        public event EndOfCallHandler EndOfCall;


        public Parser()
        {

        }

        protected void OnPhonePairRequest(string ip, string port, string phoneNumber)
        {
            if(PhonePairRequest!=null)
                PhonePairRequest(ip, port, phoneNumber);
        }
        protected void OnPcPairRequest(string ip, string port, string userId)
        {
            if (PcPairRequest != null)
                PcPairRequest(ip, port, userId);
        }
        protected void OnIncomingCall(string callerNumber, string ownerNumber, string time)
        {
            if (IncomingCall!=null)
                IncomingCall(callerNumber, ownerNumber, time);
        }
        protected void OnAnsweredCall(object Params)
        {
            if (AnsweredCall != null)
                AnsweredCall(Params);
        }
        protected void OnEndOfCall(object Params)
        {
            if (EndOfCall != null)
                EndOfCall(Params);
        }

        //MESSAGE PARSER -- Decides what to do with a message
        public virtual void ParseMessage(string message)
        {
            if (message != "" || message != null)
            {
                Console.WriteLine(message);
                string[] strings = message.Split('|');


                switch (strings[0])
                {
                    case "99":
                        if (strings.Length == 4)
                        {
                            //1 = ip, 2= port, 3 = phonenumber
                            OnPhonePairRequest(strings[1], strings[2], strings[3]);
                        }
                        break;
                    case "98":
                        if (strings.Length == 4)
                        {
                            OnPcPairRequest(strings[1], strings[2], strings[3]);
                        }
                        break;
                    //New Incoming Call
                    case "00":
                        if (strings.Length == 4)
                        {
                            //change to Choose to answer or decline
                            OnIncomingCall(strings[1], strings[2], strings[3]);
                        }
                        break;
                    //Answered Incoming Call
                    case "01":
                        if (true)
                        {
                            OnAnsweredCall(strings);
                        }
                        break;
                    //End of Incoming Call
                    case "02":
                        if (true)
                        {
                            OnEndOfCall(strings);
                        }
                        break;
                }
            }
        }
       /* protected void IncomingCall(string callerNumber, string ownerNumber, string time)
        {
            throw (new NotImplementedException());
        }
        protected void AnsweredCall(string[] tmp)
        {
            //Update endtime
            throw (new NotImplementedException());
        }
        protected void EndOfCall(string[] tmp)
        {
            throw (new NotImplementedException());
        }*/
        
    }
}
