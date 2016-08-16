using System.Collections.Generic;
using System.Threading.Tasks;

using Akka.Actor;

namespace ActorModel
{
    public class StatisticsActor: ReceiveActor
    {
        public Dictionary<string, int> PlayCounts { get; set; }

        public StatisticsActor()
        {
            Receive<InitialStatisticsMessage>(message => HandleInitialMessage(message));
        }

        public void HandleInitialMessage(InitialStatisticsMessage message)
        {
            PlayCounts = new Dictionary<string, int>(message.PlayCounts);
        }
    }
}