using System.Collections.Generic;

using Akka.Actor;
using Akka.TestKit.Xunit2;

using Moq;

using Xunit;

namespace ActorModel.Tests
{
    public class DatabaseActorTests : TestKit
    {
        private const string BooleanLies = "Boolean Lies";
        private const string CodenanTheBarbarian = "Codenan the Barbarian";

        [Fact]
        public void ShouldReadStatsFromDatabase()
        {
            var statsData = new Dictionary<string, int>
            {
                { BooleanLies, 42 },
                { CodenanTheBarbarian, 200 }
            };

            var mockDb = new Mock<IDatabaseGateway>();
            mockDb.Setup(x => x.GetStoredStatistics()).Returns(statsData);

            IActorRef actor = ActorOf(Props.Create(() => new DatabaseActor(mockDb.Object)));

            actor.Tell(new GetInitialStatisticsMessage());

            var received = ExpectMsg<InitialStatisticsMessage>();

            Assert.Equal(received.PlayCounts[BooleanLies], 42);
            Assert.Equal(received.PlayCounts[CodenanTheBarbarian], 200);
        }
    }
}