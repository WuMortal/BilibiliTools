using System;

namespace BilibiliTools.Analyzer.Model
{
    public class Episode
    {
        public string EpisodeId { get; set; }

        public string EpisodeTitle { get; set; }

        public string Aid { get; set; }

        public string DirectoryPath { get; set; }

        public string CoverImagePath { get; set; }

        public string CoverUrl { get; set; }

        public string UploaderId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}