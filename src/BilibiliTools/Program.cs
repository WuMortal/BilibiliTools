using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Rename
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath;
            while (true)
            {
                Console.WriteLine("请输入视频缓存路径：");
                rootPath = Console.ReadLine();
                if (!Directory.Exists(rootPath))
                {
                    Console.WriteLine("视频缓存路径错误，请检查路径是否正确！");
                }
                else
                {
                    break;
                }
            }

            var videoPaths = Directory.GetDirectories(rootPath);
            Console.WriteLine("请选择视频整理方式：");
            var episodeMoveType = (EpisodeMoveType) Convert.ToInt32(Console.ReadLine());

            IEpisodeProcessor processor;
            switch (episodeMoveType)
            {
                case EpisodeMoveType.Rename:
                    processor = new EpisodeRenameEpisodeProcessor();
                    break;
                case EpisodeMoveType.RenameAndReclassify:
                    processor = new EpisodeRenameAndReclassifyEpisodeProcessor();
                    break;
                default:
                    processor = new EpisodeRenameEpisodeProcessor();
                    break;
            }

            foreach (var path in videoPaths)
            {
                EpisodeProcess(path, processor);
                RootDirectoryRename(path);
            }

            Console.WriteLine("complete!");
        }

        private static void RootDirectoryRename(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            var classInfo = Util.GetIniString(".ShellClassInfo", "InfoTip", $"{directoryInfo.FullName}/desktop.ini");
            File.Move($"{directoryInfo.FullName}/cover.jpg", $"{directoryInfo.FullName}/{classInfo}.jpg");
            Directory.Move($"{directoryInfo.FullName}", $"{directoryInfo.Parent}/{classInfo}");
        }

        private static void EpisodeProcess(string episodeVideoPath, IEpisodeProcessor episodeProcessor)
        {
            var videoDirectoryName = new DirectoryInfo(episodeVideoPath).Name;
            var videoSubPaths = Directory.GetDirectories(episodeVideoPath);

            foreach (var dirPath in videoSubPaths)
            {
                var currentDirPath = new DirectoryInfo(dirPath);
                var videoFilePath = new StringBuilder(currentDirPath.FullName)
                    .Append(@"\").Append(currentDirPath.GetFiles().First(f => f.FullName.EndsWith(".mp4")).Name).ToString();
                var episode = new EpisodeInfo
                {
                    IsAddPrefix = videoSubPaths.Length > 1,
                    DirectoryInfo = currentDirPath,
                    EpisodeName = Util.GetEpisodeName($"{dirPath}/{videoDirectoryName}.info"),
                    EpisodeSourceVideoFilePath = videoFilePath
                };
                episodeProcessor.Process(episode);

                Console.WriteLine($"{episode.EpisodeName} 处理完成");
            }
        }
    }

    enum EpisodeMoveType
    {
        Rename = 1,

        RenameAndReclassify = 2
    }
}