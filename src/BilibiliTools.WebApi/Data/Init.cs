using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using Dapper;

namespace BilibiliTools.Web.Data
{
    public class Init
    {
        public static async Task InitDatabase()
        {
            if (!File.Exists(DataOptions.DateBaseFullName))
            {
                Console.WriteLine("正在初始化数据库文件......");
                SQLiteConnection.CreateFile(DataOptions.DateBaseFullName);

                await using var connection = new SQLiteConnection(DataOptions.DateBaseConnectionStr);
                connection.Open();
                await connection.ExecuteAsync(GetInitSql());
            }
        }

        private static string GetInitSql()
        {
            return @"
            CREATE TABLE ""Uploader"" (
              ""UploaderId"" VARCHAR(255) NOT NULL,
              ""UploaderName"" VARCHAR(255) NOT NULL,
              ""CreateDate"" DATE NOT NULL,
              PRIMARY KEY (""UploaderId"")
            );
            CREATE TABLE ""Episode"" (
              ""EpisodeId"" VARCHAR(255) NOT NULL,
              ""EpisodeTitle"" VARCHAR(255) NOT NULL,
              ""Aid"" VARCHAR(255) NOT NULL,
              ""DirectoryPath"" VARCHAR(255) NOT NULL,
              ""CoverImagePath"" VARCHAR(255) NOT NULL,
              ""CoverUrl"" VARCHAR(255) NOT NULL,
              ""UploaderId"" VARCHAR(255) NOT NULL,
              ""CreateDate"" DATE NOT NULL,
              PRIMARY KEY (""EpisodeId"")
            );
            CREATE TABLE ""Part"" (
              ""PartId"" VARCHAR(255) NOT NULL,
              ""PartNo"" VARCHAR(32) NOT NULL,
              ""PartName"" VARCHAR(255) NOT NULL,
              ""DirectoryPath"" VARCHAR(255) NOT NULL,
              ""FileName"" VARCHAR(255) NOT NULL,
              ""EpisodeId"" VARCHAR(255) NOT NULL,
              ""CreateDate"" DATE NOT NULL,
              PRIMARY KEY (""PartId"")
            );
            CREATE TABLE ""KeyValue"" (
              ""Key"" VARCHAR(255) NOT NULL,
              ""Value"" VARCHAR(255) NOT NULL
            );
            INSERT INTO ""main"".""KeyValue""(""Key"", ""Value"") VALUES ('LastAnalysisTime', '1949-10-01');";
        }
    }
}