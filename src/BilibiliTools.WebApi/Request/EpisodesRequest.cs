namespace BilibiliTools.Web.Request
{
    public class EpisodesRequest
    {
        public string QueryText { get; set; }

        public string UploaderId { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}