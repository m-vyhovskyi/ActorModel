using System.Threading;

using Akka.Actor;

namespace ActorModel
{
    public class UserActor: ReceiveActor
    {
        private readonly IActorRef stats;
        public string CurrentlyPlaying { get; private set; }

        public UserActor(IActorRef stats)
        {
            this.stats = stats;
            Receive<PlayMovieMessage>(message =>
            {
                CurrentlyPlaying = message.TitleName;
                Sender.Tell(new NowPlayingMessage(CurrentlyPlaying));
                stats.Tell(message.TitleName);
            });
        }
    }
}