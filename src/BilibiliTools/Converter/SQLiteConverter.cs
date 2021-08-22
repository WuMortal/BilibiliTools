using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using BilibiliTools.Model;
using Dapper;

namespace BilibiliTools
{
    public class SQLiteConverter : IConverter
    {
        public async Task Convert(List<Uploader> uploader, List<Episode> episodes, List<Part> parts)
        {
            var queryUploaderIds = await GetExistUploaderIds(uploader.Select(u => u.UploaderId).ToList());
            var queryEpisodesIds = await GetExistEpisodeIds(episodes.Select(e => e.EpisodeId).ToList());
            var queryPartIds = await GetExistPartIds(parts.Select(p => p.PartId).ToList());

            Console.WriteLine("开始写入数据库.....");

            await using var connection = new SQLiteConnection(Common.DateBaseConnectionStr);

            await connection.ExecuteAsync(Common.UploaderInsertSql,
                uploader.Where(u => !queryUploaderIds.Contains(u.UploaderId)));

            await connection.ExecuteAsync(Common.EpisodeInsertSql,
                episodes.Where(e => !queryEpisodesIds.Contains(e.EpisodeId)));

            await connection.ExecuteAsync(Common.PartInsertSql,
                parts.Where(p => !queryPartIds.Contains(p.PartId)));

            Console.WriteLine("数据库写入完毕。");
        }

        private async Task<IEnumerable<string>> GetExistUploaderIds(List<string> ids)
        {
            var sql = "SELECT UploaderId FROM Uploader WHERE UploaderId IN @Ids";
            await using var connection = new SQLiteConnection(Common.DateBaseConnectionStr);
            return await connection.QueryAsync<string>(sql, new {Ids = ids});
        }

        private async Task<IEnumerable<string>> GetExistEpisodeIds(List<string> ids)
        {
            var sql = "SELECT EpisodeId FROM Episode WHERE EpisodeId IN @Ids";
            await using var connection = new SQLiteConnection(Common.DateBaseConnectionStr);
            return await connection.QueryAsync<string>(sql, new {Ids = ids});
        }

        private async Task<IEnumerable<string>> GetExistPartIds(List<string> ids)
        {
            var sql = "SELECT PartId FROM Part WHERE PartId IN @Ids";
            await using var connection = new SQLiteConnection(Common.DateBaseConnectionStr);
            return await connection.QueryAsync<string>(sql, new {Ids = ids});
        }
    }
}