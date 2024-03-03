using BilibiliTools.Analyzer.Model;

namespace BilibiliTools.Transfer
{
    public interface ITransfer
    {
        #region TODO：改为 TransferFilter 类
        public string Keyword { get; set; }

        public string UploaderId { get; set; }

        public DateTime BeginDateTime { get; set; }
        #endregion

        public string TargetDirectoryPath { get; set; }

        public string BuildMoveTargetFilePath(Episode episode, Part part, bool isOnlyOnePart);
    }
}
