using System.Linq;
using System.Threading;
using CafeDataSet;

namespace LoadLiveCafeDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = args.FirstOrDefault() ?? "http://localhost:5341";

			var backgroundThread = new Thread(new ThreadStart(() => DataSetLoader.RunInteractive(uri, "kUC9lFPHyJxU0R5FS0Aa", CancellationToken.None))); //stage
			backgroundThread.Start();

			DataSetLoader.RunInteractive(uri, "Taa0y9FPYcfqWXOuHCHh", CancellationToken.None); //production
        }
    }
}
