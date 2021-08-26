using System.Collections.Generic;
using BilibiliTools.Analyzer.Model;

namespace BilibiliTools.Analyzer
{
    public class AnalyzeResult
    {
        public List<Uploader> UploaderList { get; set; } = new List<Uploader>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public List<Part> PartList { get; set; } = new List<Part>();
    }
}