namespace ActorModel
{
    public class PlayMovieMessage
    {
        public string TitleName { get; private set; }

        public PlayMovieMessage(string titleName)
        {
            TitleName = titleName;
        }
    }
}