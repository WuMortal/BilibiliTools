using System.IO;

namespace Rename
{
    public interface IEpisodeProcessor
    {
        void Process(EpisodeInfo episode);
    }
}