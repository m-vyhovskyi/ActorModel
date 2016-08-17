using System.Collections.Generic;

using Akka.Actor;

namespace ActorModel
{
    public class StatisticsActor : ReceiveActor
    {
        public Dictionary<string, int> PlayCounts { get; set; }

        public StatisticsActor()
        {
            Receive<InitialStatisticsMessage>(message => HandleInitialMessage(message));
            Receive<string>(title => HandleTitleMassage(title));
        }

        private void HandleTitleMassage(string title)
        {
            if (PlayCounts.ContainsKey(title))
            {
                PlayCounts[title]++;
            }
            else
            {
                PlayCounts.Add(title, 1);
            }
        }

        public void HandleInitialMessage(InitialStatisticsMessage message)
        {
            PlayCounts = new Dictionary<string, int>(message.PlayCounts);
        }
    }
}