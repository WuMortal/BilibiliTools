using System;

namespace BilibiliTools.Model
{
    public class Part
    {
        public string PartId { get; set; }

        public string PartNo { get; set; }

        public string PartName { get; set; }

        public string DirectoryPath { get; set; }

        public string FileName { get; set; }

        public string EpisodeId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}