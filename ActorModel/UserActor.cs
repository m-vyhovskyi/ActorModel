using System;

using Akka.Actor;
using Akka.Event;

namespace ActorModel
{
    public class UserActor : ReceiveActor
    {
        private readonly ILoggingAdapter log = Context.GetLogger();

        private readonly IActorRef stats;

        public string CurrentlyPlaying { get; private set; }

        public UserActor(IActorRef stats)
        {
            this.stats = stats;
            Receive<PlayMovieMessage>(message =>
            {
                if (message.TitleName == "Null Terminator")
                {
                    throw new NotSupportedException();
                }

                log.Info("Started playing {0}", message.TitleName);
                CurrentlyPlaying = message.TitleName;
                log.Info("Replying to sender");
                Sender.Tell(new NowPlayingMessage(CurrentlyPlaying));
                stats.Tell(message.TitleName);

                Context.ActorSelection("/user/audit").Tell(message);
            });
        }
    }
}