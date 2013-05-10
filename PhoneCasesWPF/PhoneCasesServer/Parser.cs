using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class Parser
    {
        public delegate void PhonePairRequestHandler(string phoneNumber, string port, string ip);
        public delegate void PcPairRequestHandler(string ip, string port, string userId);
        public delegate void IncomingCallHandler(string ownerNumber, string callerNumber, string time);
        public delegate void AnsweredCallHandler(object Params);
        public delegate void EndOfCallHandler(string ownerNumber, string caseId, string time);
        public delegate void CaseCreatedHandler(string userId);
        public delegate void UpdateCasesHandler();
        

        public event PhonePairRequestHandler PhonePairRequest;
        public event PcPairRequestHandler PcPairRequest;
        public event IncomingCallHandler IncomingCall;
        public event AnsweredCallHandler AnsweredCall;
        public event EndOfCallHandler EndOfCall;
        public event CaseCreatedHandler CaseCreated;
        public event UpdateCasesHandler UpdateCases;

        public Parser()
        {

        }

        protected void OnPhonePairRequest(string phoneNumber, string port, string ip)
        {
            if(PhonePairRequest!=null)
                PhonePairRequest(phoneNumber, port, ip);
        }
        protected void OnPcPairRequest(string userId, string ip, string port)
        {
            if (PcPairRequest != null)
                PcPairRequest(userId, ip, port);
        }
        protected void OnIncomingCall(string ownerNumber, string callerNumber, string time)
        {
            if (IncomingCall!=null)
                IncomingCall(ownerNumber, callerNumber, time);
        }
        protected void OnAnsweredCall(object Params)
        {
            if (AnsweredCall != null)
                AnsweredCall(Params);
        }
        protected void OnEndOfCall(string ownerNumber, string caseId, string time)
        {
            if (EndOfCall != null)
                EndOfCall(ownerNumber,caseId, time);
        }
        protected void OnCaseCreated(string caseId)
        {
            if (CaseCreated != null)
                CaseCreated(caseId);
        }
        protected void OnUpdateCases()
        {
            if (UpdateCases != null)
                UpdateCases();
        }

        //MESSAGE PARSER -- Decides what to do with a message
        //Remake to use decorator?
        public virtual void ParseMessage(string message)
        {
            if (message != "" || message != null)
            {
                message = message.Replace("\n", "");
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
                        if (strings.Length == 4)
                        {
                            OnEndOfCall(strings[1],strings[2], strings[3]);
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
