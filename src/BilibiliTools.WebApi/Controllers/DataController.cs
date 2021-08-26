using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using BilibiliTools.Web.Data;
using BilibiliTools.Web.Response;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BilibiliTools.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet("[action]")]
        public List<EpisodeResponse> GetEpisodes()
        {
            var sql = "SELECT EpisodeId,EpisodeTitle,CoverImagePath,CreateDate FROM Episode ORDER BY CreateDate DESC";
            using var connection = new SQLiteConnection(DataOptions.DateBaseConnectionStr);
            return connection.Query<EpisodeResponse>(sql).ToList();
        }
    }
}