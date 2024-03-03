using BilibiliTools.Analyzer.Model;
using BilibiliTools.Transfer;
using Dapper;
using System.Data.SQLite;

var sourceDirectoryPath = "F:\\Other\\BilibiliCache";
ITransfer transfer = new IsmistTransfer();

Console.WriteLine($"开始迁移关于{transfer.Keyword}相关的数据，数据根目录：{sourceDirectoryPath}");
await using var connection = new SQLiteConnection("Data Source=E:\\Projects\\BilibiliTools\\src\\BilibiliTools.WebApi\\info.db3");
var sql = "SELECT * FROM Episode WHERE UploaderId=@UploaderId AND CreateDate > @BeginDateTime AND EpisodeTitle LIKE '%'||@QueryText||'%'  ORDER BY CreateDate";
var result = await connection.QueryAsync<Episode>(sql, new { QueryText = transfer.Keyword, transfer.UploaderId, transfer.BeginDateTime });
var partList = await connection.QueryAsync<Part>("SELECT * FROM Part ");

foreach (var item in result)
{
    item.EpisodeTitle = item.EpisodeTitle.Replace("/", "或").Replace("\\", "或");
    var thatPartList = partList.Where(p => p.EpisodeId == item.EpisodeId);
    var isOnlyOnePart = thatPartList.Count() <= 1;

    Console.WriteLine($"开始迁移：{item.EpisodeTitle}，发现：{thatPartList.Count()} 个视频");
    foreach (var part in thatPartList)
    {
        var sourceFilePath = Path.Combine(sourceDirectoryPath, part.DirectoryPath, part.FileName);
        var targetFilePath = transfer.BuildMoveTargetFilePath(item, part, isOnlyOnePart);

        var fileInfo = new FileInfo(targetFilePath);
        if (fileInfo.Exists)
        {
            Console.WriteLine("文件已存在跳过");
            continue;
        }
        if (!Directory.Exists(fileInfo.Directory.FullName))
        {
            Directory.CreateDirectory(fileInfo.Directory.FullName);
        }
        File.Copy(sourceFilePath, targetFilePath);

        var fileBytes = await File.ReadAllBytesAsync(targetFilePath);
        var isEncryFile = fileBytes[0] == 255 && fileBytes[1] == 255 && fileBytes[2] == 255;
        if (isEncryFile)
        {
            var newFileBytes = fileBytes.Skip(3).ToArray();
            using var fileStream = File.OpenWrite(targetFilePath);
            await fileStream.WriteAsync(newFileBytes);
        }

        Console.WriteLine("处理完成");
    }
}

Console.WriteLine("所有视频处理完成！");
Console.ReadLine();
