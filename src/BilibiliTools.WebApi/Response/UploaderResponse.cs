using System;

namespace BilibiliTools.Web.Response
{
    public class UploaderResponse
    {
        public string UploaderId { get; set; }

        public string UploaderName { get; set; }

        public string UploaderAvatarUrl { get; set; }

        public int UploadCount { get; set; }

        public DateTime CreateDate { get; set; }
    }
}