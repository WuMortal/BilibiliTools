using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace BilibiliTools
{
    public static class Util
    {


        public static string GetEpisodeName(string filePath)
        {
            var infoFile = File.ReadAllText(filePath);
            var info = JsonConvert.DeserializeObject<dynamic>(infoFile);
            return info?.PartName.ToString();
        }

        public static string GetIniString(string section, string key, string strIniFilePath)
        {
            var temp = new StringBuilder(255);

            long i = GetPrivateProfileString(section, key, "", temp, 255, strIniFilePath);
            return temp.ToString();
        }

        // 返回取得字符串缓冲区的长度
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern long GetPrivateProfileString(string section, string key, string strDefault,
            StringBuilder retVal, int size,
            string filePath);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileInt(string section, string key, int nDefault, string filePath);
    }
}