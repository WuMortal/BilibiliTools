using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BilibiliTools.Bilibili;
using BilibiliTools.Model;
using Newtonsoft.Json;

namespace BilibiliTools
{
    public class Analyzer
    {
        private readonly IConverter _analysisResultConverter = new SQLiteConverter();

        private List<Uploader> _uploaderList = new List<Uploader>();
        private List<Episode> _episodeList = new List<Episode>();
        private List<Part> _partList = new List<Part>();

        private readonly DateTime _lastAnalysisTime = new DateTime(1999, 01, 01);

        public void Analyze(string path)
        {
            foreach (var episodeDirectoryInfo in GetVideoDirectories(path))
            {
                try
                {
                    var videoInfo = BuildBilibiliVideoInfo(episodeDirectoryInfo);

                    if (!UploaderIsExist(videoInfo.Mid))
                    {
                        _uploaderList.Add(Resolver.ResolveUploader(videoInfo));
                    }

                    _episodeList.Add(Resolver.ResolveEpisode(videoInfo, episodeDirectoryInfo.FullName));

                    foreach (var partDirectoryInfo in GetVideoDirectories(episodeDirectoryInfo.FullName))
                    {
                        var partInfo = BuildBilibiliPartInfo(episodeDirectoryInfo.Name, partDirectoryInfo);

                        _partList.Add(Resolver.ResolvePart(partInfo, partDirectoryInfo.FullName));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffff} - {e.Message}");
                }
            }
            _analysisResultConverter.Convert(_uploaderList, _episodeList, _partList);
        }

        private bool UploaderIsExist(string uploaderId) => _uploaderList.Any(u => u.UploaderId.Equals(uploaderId));

        private BilibiliVideoInfo BuildBilibiliVideoInfo(DirectoryInfo directoryInfo)
        {
            var infoFilePath = new StringBuilder(directoryInfo.FullName)
                .Append(@"\").Append(directoryInfo.Name).Append(".dvi");

            if (!File.Exists(infoFilePath.ToString()))
            {
                throw new FileNotFoundException($"跳过 【{directoryInfo.FullName}】 目录，原因：目录中未发现【.dvi】 文件！");
            }

            return JsonConvert.DeserializeObject<BilibiliVideoInfo>(File.ReadAllText(infoFilePath.ToString()));
        }

        private BilibiliPartInfo BuildBilibiliPartInfo(string episodeDirectoryName, DirectoryInfo partDirectoryInfo)
        {
            var infoFilePath = new StringBuilder(partDirectoryInfo.FullName)
                .Append(@"\").Append(episodeDirectoryName).Append(".info");

            return JsonConvert.DeserializeObject<BilibiliPartInfo>(File.ReadAllText(infoFilePath.ToString()));
        }

        private List<DirectoryInfo> GetVideoDirectories(string path)
        {
            var result = new List<DirectoryInfo>();
            var directoryPaths = Directory.GetDirectories(path);
            foreach (var directoryPath in directoryPaths)
            {
                var directoryInfo = new DirectoryInfo(directoryPath);
                if (directoryInfo.LastWriteTime > _lastAnalysisTime)
                {
                    result.Add(directoryInfo);
                }
            }

            return result;
        }
    }
}