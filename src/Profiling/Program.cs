using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Profiling
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO move the tests.yml to a more general location and Add Existing Item -> Add Link?
            TestClient.Configuration = new ProfilingTestConfiguration(@"..\..\..\Tests\tests.yml");

            using (var cluster = new ProfilingCluster())
            {
                var client = cluster.Client();
                var output = new ColoredConsoleWriter();
                MainAsync(client, output).Wait();
            }
        }

        static async Task MainAsync(IElasticClient client, ColoredConsoleWriter output)
        {
            await new BulkAsyncOperation().RunAsync(client, output).ConfigureAwait(false);
        }
    }
}
