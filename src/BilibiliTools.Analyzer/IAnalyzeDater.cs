using System;

namespace BilibiliTools.Analyzer
{
    public interface IAnalyzeDater
    {
        DateTime GetAnalyzeDateTime();

        void UpdateAnalyzeDateTime(DateTime dateTime);
    }
}