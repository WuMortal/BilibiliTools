﻿using System.Threading.Tasks;
using BilibiliTools.Analyzer.Converter;
using Microsoft.AspNetCore.Mvc;

namespace BilibiliTools.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly Analyzer.Analyzer _analyzer;
        private readonly IConverter _converter;

        public SystemController(Analyzer.Analyzer analyzer, IConverter converter)
        {
            _analyzer = analyzer;
            _converter = converter;
        }

        [HttpPost("[action]")]
        public async Task<int> RunAnalyze(string path = "F:\\Other\\BilibiliCache")
        {
            var result = _analyzer.Analyze(path);

            await _converter.Convert(result.UploaderList, result.EpisodeList, result.PartList);

            return result.PartList.Count;
        }
    }
}