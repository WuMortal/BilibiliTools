using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BilibiliTools.Analyzer.Bilibili;
using BilibiliTools.Analyzer.Converter;
using BilibiliTools.Analyzer.Model;
using Newtonsoft.Json;

namespace BilibiliTools.Analyzer
{
    public class Analyzer
    {
        private readonly List<Uploader> _uploaderList = new List<Uploader>();
        private readonly List<Episode> _episodeList = new List<Episode>();
        private readonly List<Part> _partList = new List<Part>();

        private readonly IAnalyzeDater _analyzeDater;
        private readonly DateTime _lastAnalysisTime;

        public Analyzer(IAnalyzeDater analyzeDater)
        {
            _analyzeDater = analyzeDater;
            _lastAnalysisTime = analyzeDater.GetAnalyzeDateTime();
        }

        public AnalyzeResult Analyze(string path)
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

            _analyzeDater.UpdateAnalyzeDateTime(DateTime.Now);

            return new AnalyzeResult
            {
                UploaderList = _uploaderList,
                EpisodeList = _episodeList,
                PartList = _partList
            };
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