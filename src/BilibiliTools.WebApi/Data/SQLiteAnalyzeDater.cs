using System;
using System.Data.SQLite;
using BilibiliTools.Analyzer;
using Dapper;

namespace BilibiliTools.Web.Data
{
    public class SQLiteAnalyzeDater : IAnalyzeDater
    {
        private const string LastAnalysisTime = "LastAnalysisTime";

        public DateTime GetAnalyzeDateTime()
        {
            var sql = "SELECT `Value` FROM main.KeyValue WHERE `Key`=@LastAnalysisTime";
            using var connection = new SQLiteConnection(DataOptions.DateBaseConnectionStr);
            return connection.QueryFirst<DateTime>(sql, new {LastAnalysisTime});
        }

        public void UpdateAnalyzeDateTime(DateTime dateTime)
        {
            var sql = "UPDATE main.KeyValue SET  Value = @Value WHERE Key = @LastAnalysisTime;";
            using var connection = new SQLiteConnection(DataOptions.DateBaseConnectionStr);
            connection.Execute(sql, new {LastAnalysisTime, Value = dateTime});
        }
    }
}