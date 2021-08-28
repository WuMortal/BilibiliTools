using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilibiliTools.Web.Data;
using BilibiliTools.Web.Request;
using BilibiliTools.Web.Response;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BilibiliTools.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<PageList<EpisodeResponse>> QueryEpisodes([FromBody] EpisodesRequest request)
        {
            var limitCount = (request.PageIndex - 1) * request.PageSize;
            var filterSql = string.IsNullOrEmpty(request.QueryText)
                ? string.Empty
                : " AND EpisodeTitle LIKE '%'||@QueryText||'%'";
            filterSql += string.IsNullOrEmpty(request.UploaderId) ? string.Empty : " AND UploaderId=@UploaderId ";

            var sql = new StringBuilder($@"SELECT COUNT(*) FROM Episode WHERE 1=1 ").Append(filterSql).Append(";")
                .AppendLine(" SELECT EpisodeId,EpisodeTitle,CoverImagePath,CreateDate")
                .AppendLine(" FROM Episode WHERE 1=1 ")
                .AppendLine(filterSql)
                .AppendLine($" ORDER BY CreateDate DESC LIMIT {limitCount},{request.PageSize}");


            await using var connection = new SQLiteConnection(DataOptions.DateBaseConnectionStr);
            var reader = await connection.QueryMultipleAsync(sql.ToString(),
                new {request.QueryText, request.UploaderId});

            return new PageList<EpisodeResponse>
            {
                TotalCount = reader.ReadFirst<int>(),
                Item = reader.Read<EpisodeResponse>().ToList()
            };
        }

        [HttpGet("[action]")]
        public async Task<List<UploaderResponse>> GetAllUploader()
        {
            var sql = @"SELECT U.*,COUNT(*) `UploadCount`
                        FROM Uploader AS U
                        LEFT JOIN Episode AS E ON U.UploaderId=E.UploaderId
                        GROUP BY U.UploaderId
                        ORDER BY UploadCount DESC;";

            await using var connection = new SQLiteConnection(DataOptions.DateBaseConnectionStr);
            var result = await connection.QueryAsync<UploaderResponse>(sql);
            return result.ToList();
        }
    }
}