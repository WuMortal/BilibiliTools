using System;

namespace BilibiliTools.Web.Response
{
    public class EpisodeResponse
    {
        public string EpisodeId { get; set; }

        public string EpisodeTitle { get; set; }

        public string CoverImagePath { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}