using System.Collections.Generic;
using System.Threading.Tasks;
using BilibiliTools.Model;

namespace BilibiliTools
{
    public interface IConverter
    {
        Task Convert(List<Uploader> uploader, List<Episode> episodes, List<Part> parts);
    }
}