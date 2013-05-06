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
        public delegate void CaseCreatedDelegate(string userId);

        public event CaseCreatedDelegate CaseCreated;

        private void OnCaseCreated(string userId)
        {
            if (CaseCreated != null)
                CaseCreated(userId);
        }
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
                            OnCaseCreated(strings[1]);
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
    }
}
