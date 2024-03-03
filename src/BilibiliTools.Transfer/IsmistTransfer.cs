using BilibiliTools.Analyzer.Model;

namespace BilibiliTools.Transfer
{
    public class IsmistTransfer : ITransfer
    {
        public string Keyword { get; set; } = "【主义主义】";

        public string UploaderId { get; set; } = "23191782";

        public DateTime BeginDateTime { get; set; } = Convert.ToDateTime("2021-08-03 00:44:00");

        public string TargetDirectoryPath { get; set; } = "G:\\ismist";

        public string BuildMoveTargetFilePath(Episode episode, Part part, bool isOnlyOnePart)
        {
            var isIsmistPoinstVideo = episode.EpisodeTitle.Contains("（");
            if (!isIsmistPoinstVideo)
            {
                return isOnlyOnePart ? Path.Combine(TargetDirectoryPath, $"{episode.EpisodeTitle}.mp4") : Path.Combine(TargetDirectoryPath, episode.EpisodeTitle, $"{part.DirectoryPath.Split("//")[1]}.mp4");
            }
            else
            {
                var pointStart = episode.EpisodeTitle.IndexOf("（");
                var pointEnd = episode.EpisodeTitle.IndexOf("）");
                var length = pointEnd - pointStart;
                var point = episode.EpisodeTitle.Substring(pointStart + 1, length - 1);
                var pointArrary = point.Split("-");

                string? cosmology;
                string? ontology;
                var title = episode.EpisodeTitle.Replace($"（{point}）", string.Empty).Insert(0, $"({point})");
                switch (pointArrary.Count())
                {
                    case 1:
                        cosmology = pointArrary[0].Trim();
                        return isOnlyOnePart ? Path.Combine(TargetDirectoryPath, cosmology, $"{title}.mp4") : Path.Combine(TargetDirectoryPath, cosmology, title, $"{part.DirectoryPath.Split("\\")[1]}.mp4");
                    case 2:
                    case 3:
                    case 4:
                        cosmology = pointArrary[0].Trim();
                        ontology = pointArrary[1].Trim();
                        return isOnlyOnePart ? Path.Combine(TargetDirectoryPath, cosmology, $"{cosmology}-{ontology}", $"{title}.mp4") : Path.Combine(TargetDirectoryPath, cosmology, $"{cosmology}-{ontology}", title, $"{part.DirectoryPath.Split("\\")[1]}.mp4");
                    default:
                        throw new NotImplementedException();
                }
            }

        }
    }
}
