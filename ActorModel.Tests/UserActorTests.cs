using System;

using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.TestActors;
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
            TestActorRef<UserActor> actor = ActorOfAsTestActorRef<UserActor>(
                Props.Create(()=>new UserActor(ActorOf(BlackHoleActor.Props))));

            Assert.Null(actor.UnderlyingActor.CurrentlyPlaying);
        }

        [Fact]
        public void ShouldUpdateCurrentlyPlayingState()
        {
            TestActorRef<UserActor> actor = ActorOfAsTestActorRef<UserActor>(
                Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            actor.Tell(new PlayMovieMessage(CodenanTheBarbarian));

            Assert.Equal(CodenanTheBarbarian, actor.UnderlyingActor.CurrentlyPlaying);
        }

        [Fact]
        public void ShouldPlayMovie()
        {
            TestActorRef<UserActor> actor = ActorOfAsTestActorRef<UserActor>(
                Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            actor.Tell(new PlayMovieMessage(CodenanTheBarbarian));

            var received = ExpectMsg<NowPlayingMessage>();

            Assert.Equal(CodenanTheBarbarian, received.CurrentlyPlaying);
        }

        [Fact]
        public void ShouldLogPlayMovie()
        {
            IActorRef actor = ActorOf(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            EventFilter.Info("Started playing Boolean Lies")
                        .And
                        .Info("Replying to sender")
                        .Expect(2, ()=>actor.Tell(new PlayMovieMessage("Boolean Lies")));
        }

        [Fact]
        public void ShouldSendToDeadLetters()
        {
            IActorRef actor = ActorOf(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            EventFilter.DeadLetter<PlayMovieMessage>(m => m.TitleName == "Boolean Lies")
                .ExpectOne(() => actor.Tell(new PlayMovieMessage("Boolean Lies")));
        }

        [Fact]
        public void ShouldErrorOnUnknownMovie()
        {
            IActorRef actor = ActorOf(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            EventFilter.Exception<NotSupportedException>()
                .ExpectOne(() => actor.Tell(new PlayMovieMessage("Null Terminator")));
        }

    }
}