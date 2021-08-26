using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using Dapper;

namespace BilibiliTools.Web.Data
{
    public static class DataOptions
    {
        public static string DateBaseFullName => "./info.db3";

        public static string DateBaseConnectionStr => $@"Data Source={DateBaseFullName}";

        public static string UploaderInsertSql = "INSERT INTO main.Uploader(UploaderId, UploaderName, CreateDate) VALUES (@UploaderId, @UploaderName, @CreateDate);";
        
        public static string EpisodeInsertSql = "INSERT INTO main.Episode(EpisodeId, EpisodeTitle, Aid, DirectoryPath, CoverImagePath, CoverUrl, UploaderId, CreateDate) VALUES (@EpisodeId, @EpisodeTitle, @Aid, @DirectoryPath, @CoverImagePath, @CoverUrl, @UploaderId, @CreateDate);";
        
        public static string PartInsertSql = "INSERT INTO main.Part(PartId, PartNo, PartName, DirectoryPath, FileName, EpisodeId, CreateDate) VALUES (@PartId, @PartNo, @PartName, @DirectoryPath, @FileName, @EpisodeId, @CreateDate);";
    }
}