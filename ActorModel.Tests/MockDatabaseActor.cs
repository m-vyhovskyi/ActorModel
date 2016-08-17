using System.Collections.Generic;
using System.Collections.ObjectModel;

using Akka.Actor;

namespace ActorModel.Tests
{
    public class MockDatabaseActor : ReceiveActor
    {
        private const string CodenanTheBarbarian = "Codenan the Barbarian";

        public MockDatabaseActor()
        {
            Receive<GetInitialStatisticsMessage>(message =>
            {
                var stats = new Dictionary<string, int> { { CodenanTheBarbarian, 42 } };
                Sender.Tell(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(stats)));
            });
        }
    }
}