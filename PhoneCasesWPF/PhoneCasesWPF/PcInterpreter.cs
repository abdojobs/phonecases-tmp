using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneCases.Server;

namespace PhoneCases.WPFGUI
{
    class PcInterpreter : Interpreter
    {
        PcInterpreter(Receiver receiver, Transmitter transmitter) : base(receiver, transmitter) { }
        protected override void IncomingCall(string callerNumber, string ownerNumber, string time)
        {

        }
    }
}
