using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneCases.Server;

namespace PhoneCases.WPFGUI
{
    class PcParser : Parser
    {
        public override void ParseMessage(string message)
        {
            if (message != "" || message != null)
            {
                Console.WriteLine(message);
                string[] strings = message.Split('|');


                switch (strings[0])
                {
                    //New Incoming Call
                    case "00":
                        if (strings.Length == 2)
                        {
                            //1 = CaseID
                            if (IncomingCall != null)
                                IncomingCall(strings[1]);
                        }
                        break;
                    //Answered Incoming Call
                    case "01":
                        if (true)
                        {
                            if (AnsweredCall != null)
                                AnsweredCall(strings);
                        }
                        break;
                    //End of Incoming Call
                    case "02":
                        if (true)
                        {
                            if (EndOfCall != null)
                                EndOfCall(strings);
                        }
                        break;
                }
            }
        }
    }
}
