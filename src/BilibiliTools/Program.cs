using System;

namespace BilibiliTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new Analyzer();
            analyzer.Analyze(@"E:\Temp");
            Console.WriteLine("complete!");
        }
    }
}