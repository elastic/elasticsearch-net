using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Profiling.Async;
using Tests.Framework;

namespace Profiling
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO move the tests.yml to a more general location and Add Existing Item -> Add Link?
            TestClient.Configuration = new ProfilingTestConfiguration();

            using (var cluster = new ProfilingCluster())
            {
                var client = cluster.Client();
                var output = new ColoredConsoleWriter();

	            using (Snapshot.Create())
	            {
					MainAsync(client, output).Wait();
				}
            }
        }

	    private static async Task MainAsync(IElasticClient client, ColoredConsoleWriter output)
        {
	        var asyncOperations = new List<IAsyncProfiledOperation>
	        {
				new AliasAsyncOperation(),
				new AliasExistsAsyncOperation(),
				new AnalyzeAsyncOperation(),
				new BulkAsyncOperation(),
		        new IndexAsyncOperation(),
				new SearchAsyncOperation(),
				new CatAsyncOperation(),
				new GetMappingAsyncOperation()
	        };

	        foreach (var asyncOperation in asyncOperations)
	        {
		        await asyncOperation.RunAsync(client, output).ConfigureAwait(false);
	        }
        }
    }
}
