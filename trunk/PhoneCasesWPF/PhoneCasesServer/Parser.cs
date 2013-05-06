using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class Parser
    {

        public delegate void IncomingCallHandler(string callerNumber, string ownerNumber, string time);
        public delegate void AnsweredCallHandler(object Params);
        public delegate void EndOfCallHandler(object Params);

        public event IncomingCallHandler IncomingCall;
        public event AnsweredCallHandler AnsweredCall;
        public event EndOfCallHandler EndOfCall;


        public Parser()
        {

        }
        //MESSAGE PARSER -- Decides what to do with a message
        public void ParseMessage(string message)
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
                            if(IncomingCall!=null)
                                IncomingCall(strings[1], strings[2], strings[3]);
                        }
                        break;
                    //Answered Incoming Call
                    case "01":
                        if (true)
                        {
                            AnsweredCall(strings);
                        }
                        break;
                    //End of Incoming Call
                    case "02":
                        if (true)
                        {
                            EndOfCall(strings);
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
