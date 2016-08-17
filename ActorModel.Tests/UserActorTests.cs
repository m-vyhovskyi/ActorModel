using System;

using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.Xunit2;

using Xunit;

namespace ActorModel.Tests
{
    public class UserActorTests : TestKit
    {
        private const string CodenanTheBarbarian = "Codenan the Barbarian";

        [Fact]
        public void ShouldHaveInitialState()
        {
            TestActorRef<UserActor> actor = ActorOfAsTestActorRef<UserActor>();

            Assert.Null(actor.UnderlyingActor.CurrentlyPlaying);
        }

        [Fact]
        public void ShouldUpdateCurrentlyPlayingState()
        {
            TestActorRef<UserActor> actor = ActorOfAsTestActorRef<UserActor>();

            actor.Tell(new PlayMovieMessage(CodenanTheBarbarian));

            Assert.Equal(CodenanTheBarbarian, actor.UnderlyingActor.CurrentlyPlaying);
        }

        [Fact]
        public void ShouldPlayMovie()
        {
            IActorRef actor = ActorOf<UserActor>();

            actor.Tell(new PlayMovieMessage(CodenanTheBarbarian));

            var received = ExpectMsg<NowPlayingMessage>();

            Assert.Equal(CodenanTheBarbarian, received.CurrentlyPlaying);
        }

    }
}