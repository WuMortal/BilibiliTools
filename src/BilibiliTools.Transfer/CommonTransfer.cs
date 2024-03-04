using BilibiliTools.Analyzer.Model;

namespace BilibiliTools.Transfer
{
    public class CommonTransfer : ITransfer
    {
        public string Keyword { get; set; } = "【经典译读】谢林《先验观念论体系》";

        public string UploaderId { get; set; } = "23191782";

        public DateTime BeginDateTime { get; set; } = Convert.ToDateTime("2002-08-03 00:44:00");

        public string TargetDirectoryPath { get; set; } = "G:\\ismist";

        public string BuildMoveTargetFilePath(Episode episode, Part part, bool isOnlyOnePart)
        {
            return isOnlyOnePart ? Path.Combine(TargetDirectoryPath, $"{episode.EpisodeTitle}.mp4") : Path.Combine(TargetDirectoryPath, episode.EpisodeTitle, $"{part.PartNo}-{part.PartName}.mp4");
        }
    }
}
