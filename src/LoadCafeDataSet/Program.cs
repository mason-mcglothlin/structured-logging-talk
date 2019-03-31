using System;
using System.Linq;
using CafeDataSet;

namespace LoadCafeDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = args.FirstOrDefault() ?? "http://localhost:5341";
            var days = 90;
            var start = DateTime.UtcNow.AddDays(-days);
            var limit = start.AddDays(days);
            DataSetLoader.Load(uri, start, limit);
            Console.WriteLine($"{start:o},{limit:o}");
        }
    }
}
