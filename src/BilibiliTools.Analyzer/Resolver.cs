using System;
using System.IO;
using System.Linq;
using System.Text;
using BilibiliTools.Analyzer.Bilibili;
using BilibiliTools.Analyzer.Model;

namespace BilibiliTools.Analyzer
{
    public static class Resolver
    {
        public static Uploader ResolveUploader(BilibiliVideoInfo bilibiliVideoInfo)
        {
            return new Uploader
            {
                UploaderId = bilibiliVideoInfo.Mid,
                UploaderName = bilibiliVideoInfo.Uploader,
                CreateDate = DateTime.Now
            };
        }

        public static Episode ResolveEpisode(BilibiliVideoInfo bilibiliVideoInfo, string episodePath)
        {
            return new Episode
            {
                EpisodeId = bilibiliVideoInfo.Bid,
                EpisodeTitle = bilibiliVideoInfo.Title,
                Aid = bilibiliVideoInfo.Aid,
                DirectoryPath = episodePath,
                CoverImagePath = new StringBuilder(episodePath).Append(@"\cover.jpg").ToString(),
                CoverUrl = bilibiliVideoInfo.CoverURL,
                UploaderId = bilibiliVideoInfo.Mid,
                CreateDate = bilibiliVideoInfo.CreateDate
            };
        }

        public static Part ResolvePart(BilibiliPartInfo partInfo, string partPath)
        {
            var filePath = Directory.GetFiles(partPath).FirstOrDefault(f => f.EndsWith(".mp4"));
            return new Part
            {
                PartId = new StringBuilder(partInfo.Bid).Append("-").Append(partInfo.PartNo).ToString(),
                PartNo = partInfo.PartNo,
                PartName = partInfo.PartName,
                EpisodeId = partInfo.Bid,
                DirectoryPath = partPath,
                FileName = filePath?.Substring(partPath.Length - 1),
                CreateDate = partInfo.CreateDate
            };
        }
    }
}