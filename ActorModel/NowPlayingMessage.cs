namespace ActorModel
{
    public class NowPlayingMessage
    {
        public string CurrentlyPlaying { get; private set; }

        public NowPlayingMessage(string currentlyPlaying)
        {
            CurrentlyPlaying = currentlyPlaying;
        }
    }
}