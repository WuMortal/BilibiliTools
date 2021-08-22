using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using BilibiliTools.Model;
using Dapper;

namespace BilibiliTools
{
    public class SQLiteConverter : IConverter
    {
        public async Task Convert(List<Uploader> uploader, List<Episode> episodes, List<Part> parts)
        {
            Console.WriteLine("开始写入数据库.....");
            await using var connection = new SQLiteConnection(Common.DateBaseConnectionStr);

            await connection.ExecuteAsync(Common.UploaderInsertSql, uploader);
            await connection.ExecuteAsync(Common.EpisodeInsertSql, episodes);
            await connection.ExecuteAsync(Common.PartInsertSql, parts);

            Console.WriteLine("数据库写入完毕。");
        }
    }
}