using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoneCases.DB;

namespace PhoneCases.Server
{
    public class ServerInterpreter :Interpreter
    {
        private Dictionary<int,AndroidPcPair> m_pairMap = new Dictionary<int,AndroidPcPair>();
        public ServerInterpreter(Receiver receiver, Transmitter transmitter) : base(receiver,transmitter)
        {
            m_receiver.Parser.PhonePairRequest += PhonePairRequest;
            m_receiver.Parser.PcPairRequest += PcPairRequest;
        }

        void PhonePairRequest(string ip, string port, string phonenumber)
        {
            Users usr = ModelContainerHolder.Model.Users.Where(a => a.PhoneNumber == phonenumber).First();
            if (usr == null)
                throw new Exception(); //Better exception

            AndroidPcPair pair = new AndroidPcPair();
            if (m_pairMap.TryGetValue(usr.Id, out pair))
                pair.Android = new Client(ip, port);
            else
            {
                pair.Android = new Client(ip, port);
                m_pairMap.Add(usr.Id, pair);
            }
     
        }
        void PcPairRequest(string ip, string port, string userId)
        {

            AndroidPcPair pair = new AndroidPcPair();
            if (m_pairMap.TryGetValue(int.Parse(userId), out pair))
                pair.Pc = new Client(ip, port);
            else
            {
                pair.Pc = new Client(ip, port);
                m_pairMap.Add(int.Parse(userId), pair);
            }

        }

    }
}
