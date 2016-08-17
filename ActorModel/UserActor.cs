using System.Threading;

using Akka.Actor;

namespace ActorModel
{
    public class UserActor: ReceiveActor
    {
        public string CurrentlyPlaying { get; private set; }

        public UserActor()
        {
            Receive<PlayMovieMessage>(message =>
            {
                CurrentlyPlaying = message.TitleName;
                Sender.Tell(new NowPlayingMessage(CurrentlyPlaying));
            });
        }
    }
}