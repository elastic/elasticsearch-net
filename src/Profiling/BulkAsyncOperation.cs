using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Profiling
{
    public class BulkAsyncOperation : IAsyncProfiledOperation
    {
        public async Task RunAsync(IElasticClient client, ColoredConsoleWriter output)
        {
            var iterations = 100;
            var itemsPerIteration = 1000;
            var total = iterations * itemsPerIteration;
            var count = 0;

            output.WriteGreen($"Start Bulk indexing {total} {typeof(Developer).Name} documents");

            for (int i = 0; i < iterations; i++)
            {
                var bulkResponse = await client.BulkAsync(b => b
                    .IndexMany(Developer.Generator.Generate(itemsPerIteration), (bd, d) => bd
                        .Index(Index<Developer>())
                        .Document(d)
                    )).ConfigureAwait(false);

                ++count;

                if (!bulkResponse.IsValid)
                    if (bulkResponse.Errors)
                        foreach (var error in bulkResponse.ItemsWithErrors)
                            output.WriteOrange($"error with id {error.Id}. message: {error.Error.Reason}");
            }
            output.WriteGreen($"Finished Bulk indexing {total} {typeof(Developer).Name} documents");
        }
    }
}