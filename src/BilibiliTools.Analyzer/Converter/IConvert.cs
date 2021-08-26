using System.Collections.Generic;
using System.Threading.Tasks;
using BilibiliTools.Analyzer.Model;

namespace BilibiliTools.Analyzer.Converter
{
    public interface IConverter
    {
        Task Convert(List<Uploader> uploader, List<Episode> episodes, List<Part> parts);
    }
}