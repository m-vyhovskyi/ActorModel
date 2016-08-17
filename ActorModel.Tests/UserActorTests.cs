using Akka.TestKit;
using Akka.TestKit.Xunit2;

using Xunit;

namespace ActorModel.Tests
{
    public class UserActorTests : TestKit
    {
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

            actor.Tell(new PlayMovieMessage("Codenan the Barbarian"));

            Assert.Equal("Codenan the Barbarian", actor.UnderlyingActor.CurrentlyPlaying);
        }
    }
}