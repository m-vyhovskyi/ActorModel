using System.Collections.ObjectModel;

using Akka.Actor;

namespace ActorModel
{
    public class DatabaseActor : ReceiveActor
    {
        private readonly IDatabaseGateway databaseGateway;

        public DatabaseActor(IDatabaseGateway databaseGateway)
        {
            this.databaseGateway = databaseGateway;

            Receive<GetInitialStatisticsMessage>(message =>
            {
                var storedStats = databaseGateway.GetStoredStatistics();

                Sender.Tell(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(storedStats)));
            });
        }
    }
}