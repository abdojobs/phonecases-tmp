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
            m_receiver.Parser.EndOfCall += EndOfCall;
        }


        void EndOfCall(string ownerNumber, string caseId, string time)
        {
            try
            {
                Users usr = ModelContainerHolder.Model.Users.Where(a => a.PhoneNumber == ownerNumber).FirstOrDefault();
                if (usr == null)
                    throw new Exception("Couldnt match number with owner"); //Better exception

                AndroidPcPair ap = new AndroidPcPair();

                if (string.IsNullOrEmpty(caseId))//If the phone didnt pass the caseId, grab the latest recorded in the Pair.
                {
                    if (m_pairMap.TryGetValue(usr.Id,out ap))
                        caseId = ap.CurrentCaseId;
                }
                Cases Case = ModelContainerHolder.Model.Cases.Find(int.Parse(caseId));
                Case.EndTime = DateTime.Parse(time);
                Case.TotalTime = ((TimeSpan?)(Case.EndTime - (DateTime?)Case.StartTime)).Value.Seconds; // Duration in seconds
                ModelContainerHolder.Model.SaveChanges();

                //Notify PCClient
                if (m_pairMap.TryGetValue(usr.Id, out ap))
                    Send("02", ap.Pc);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        protected override void IncomingCall( string ownerNumber, string callerNumber, string time)
        {
            try
            {
                Users usr = ModelContainerHolder.Model.Users.Where(a => a.PhoneNumber == ownerNumber).FirstOrDefault();
                if (usr == null)
                    throw new Exception("Couldnt match number with owner"); //Better exception

                //Create new case
                int newCaseID = ModelContainerHolder.NewCase(callerNumber, usr.Id, DateTime.Parse(time));
                if (newCaseID == -1)
                    throw new Exception("Couldnt create new case.");
                //Notify PC
                AndroidPcPair pair = new AndroidPcPair();
                if (m_pairMap.TryGetValue(usr.Id, out pair))
                {
                    pair.CurrentCaseId = newCaseID.ToString();
                    if (pair.Android != null)
                    {
                        m_transmitter.Send("00|" + newCaseID, pair.Android); //Send caseId to Phone.
                    }
                    else
                        throw new Exception("Not paired with phone");

                    if (pair.Pc != null)
                    {
                        m_transmitter.Send("00|" + newCaseID, pair.Pc); //Send caseId to PC.
                    }
                    else
                        throw new Exception("Not paired with PC.");
                }
                else
                    throw new Exception("Not paired with phone");
            
            }
            catch (Exception e)
            {
                //Error..
            }
        }
        private void PhonePairRequest(string phoneNumber, string port, string ip)
        {
            try
            {
                Users usr = ModelContainerHolder.Model.Users.Where(a => a.PhoneNumber == phoneNumber).FirstOrDefault();
                if (usr == null)
                    throw new Exception("Couldnt match number with owner"); //Better exception

                AndroidPcPair pair = new AndroidPcPair();
                if (m_pairMap.TryGetValue(usr.Id, out pair))
                    pair.Android = new Client(ip, port);
                else
                {
                    pair = new AndroidPcPair();
                    pair.Android = new Client(ip, port);
                    m_pairMap.Add(usr.Id, pair);
                }
            }
            catch (Exception e)
            {
                //Handle Error.
            }
            

            
     
        }
        private void PcPairRequest(string userId, string ip, string port)
        {

            AndroidPcPair pair = new AndroidPcPair();
            //IF(Det finns redan en mappning mot detta userid)
            if (m_pairMap.TryGetValue(int.Parse(userId), out pair))
                pair.Pc = new Client(ip, port);
            else
            {
                pair = new AndroidPcPair();
                pair.Pc = new Client(ip, port);
                m_pairMap.Add(int.Parse(userId), pair);
            }

        }

    }
}
