using System.Collections.ObjectModel;

namespace ActorModel
{
    public class InitialStatisticsMessage
    {
        public ReadOnlyDictionary<string,int> PlayCounts { get; private set; }

        public InitialStatisticsMessage(ReadOnlyDictionary<string, int> playCounts)
        {
            PlayCounts = playCounts;
        }
    }
}