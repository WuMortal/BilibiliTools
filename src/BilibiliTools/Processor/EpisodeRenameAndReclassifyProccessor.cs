using System.IO;
using System.Text;

namespace Rename
{
    public class EpisodeRenameAndReclassifyEpisodeProcessor : IEpisodeProcessor
    {
        public void Process(EpisodeInfo episode)
        {
            var renameMp4FilePath = new StringBuilder(episode.DirectoryInfo.Parent.FullName)
                .Append(@"\").Append(episode.DirectoryInfo.Name).Append(" - ").Append(episode.EpisodeName).Append(".mp4").ToString();

            File.Move(episode.EpisodeSourceVideoFilePath, renameMp4FilePath);
        }
    }
}