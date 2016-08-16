using System.Collections.Generic;
using System.Collections.ObjectModel;

using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.Xunit2;

using Xunit;

namespace ActorModel.Tests
{
    public class StatisticsActorTests : TestKit
    {
        [Fact(DisplayName = "The number of plays initially equals 0")]
        public void ShouldHaveInitialPlayCountsValue()
        {
            StatisticsActor actor = new StatisticsActor();
            Assert.Null(actor.PlayCounts);
        }

        [Fact(DisplayName = "Should Set Initial Play Counts")]
        public void ShouldSetInitialPlayCounts()
        {
            StatisticsActor actor = new StatisticsActor();

            var initialMovieStats = new Dictionary<string, int> { { "Codenan the Barbarian", 42 } };

            actor.HandleInitialMessage(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(initialMovieStats)));

            Assert.Equal(42, actor.PlayCounts["Codenan the Barbarian"]);
        }

        [Fact(DisplayName = "Should Receive Initial Statistics Message")]
        public void ShouldReceiveInitialStatisticsMessage()
        {
            TestActorRef<StatisticsActor> actor = ActorOfAsTestActorRef<StatisticsActor>();

            var initialMovieStats = new Dictionary<string, int> { { "Codenan the Barbarian", 42 } };

            actor.Tell(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(initialMovieStats)));

            Assert.Equal(42, actor.UnderlyingActor.PlayCounts["Codenan the Barbarian"]);
        }

    }


}