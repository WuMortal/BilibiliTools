using System.IO;

namespace Rename
{
    public class EpisodeInfo
    {
        public DirectoryInfo DirectoryInfo { get; set; }

        public string EpisodeName { get; set; }

        public string EpisodeSourceVideoFilePath { get; set; }

        public bool IsAddPrefix { get; set; }
    }
}