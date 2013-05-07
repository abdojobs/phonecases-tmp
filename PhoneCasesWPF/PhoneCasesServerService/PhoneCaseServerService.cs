using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoneCases.Server;

namespace PhoneCases.Server.Service
{
    class PhoneCaseServerService
    {

        static void Main(string[] args)
        {
            Parser parser = new Parser();
            Receiver receiver = new Receiver(parser,21337);
            Transmitter transmitter = new Transmitter();
            ServerInterpreter interpreter = new ServerInterpreter(receiver,transmitter);

            interpreter.StartReceiving();

        }
    }
}
