using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCases.Server
{
    public class ServerInterpreter :Interpreter
    {
        private List<AndroidPcPair> m_pairList = new List<AndroidPcPair>();
        public ServerInterpreter(Receiver receiver, Transmitter transmitter) : base(receiver,transmitter)
        {

        }

    }
}
