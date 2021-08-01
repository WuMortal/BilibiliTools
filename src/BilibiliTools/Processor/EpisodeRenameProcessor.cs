using System.IO;
using System.Text;

namespace Rename
{
    public class EpisodeRenameEpisodeProcessor : IEpisodeProcessor
    {
        public void Process(EpisodeInfo episode)
        {
            var renameMp4FilePath = new StringBuilder(episode.DirectoryInfo.FullName)
                .Append(@"\").Append(episode.EpisodeName).Append(".mp4").ToString();

            var newDirectoryPath = BuildNewDirectoryPath(episode);

            File.Move(episode.EpisodeSourceVideoFilePath, renameMp4FilePath);
            Directory.Move(episode.DirectoryInfo.FullName, newDirectoryPath);
        }

        private static string BuildNewDirectoryPath(EpisodeInfo episode)
        {
            var newDirectoryPath = new StringBuilder(episode.DirectoryInfo.Parent.FullName)
                .Append(@"\");
            if (episode.IsAddPrefix)
            {
                newDirectoryPath.Append(episode.DirectoryInfo.Name).Append(" - ");
            }

            newDirectoryPath.Append(episode.EpisodeName);
            return newDirectoryPath.ToString();
        }
    }
}