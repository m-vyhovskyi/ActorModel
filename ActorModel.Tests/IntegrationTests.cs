using System.Collections.Generic;
using System.Collections.ObjectModel;

using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.Xunit2;

using Xunit;

namespace ActorModel.Tests
{
    public class IntegrationTests : TestKit
    {
        private const string CodenanTheBarbarian = "Codenan the Barbarian";

        [Fact]
        public void UserShouldUpdatePlayCounts()
        {
            TestActorRef<StatisticsActor> stats = ActorOfAsTestActorRef<StatisticsActor>();
            var initialMovieStats = new Dictionary<string, int> { { CodenanTheBarbarian, 42 } };
            stats.Tell(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(initialMovieStats)));

            TestActorRef<UserActor> user = ActorOfAsTestActorRef<UserActor>(Props.Create(() => new UserActor(stats)));

            user.Tell(new PlayMovieMessage(CodenanTheBarbarian));

            Assert.Equal(43, stats.UnderlyingActor.PlayCounts[CodenanTheBarbarian]);
        }
    }
}