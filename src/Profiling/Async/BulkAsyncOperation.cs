using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Profiling.Async
{
	public class BulkAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _itemsPerIteration;
		private readonly int _iterations;

		public BulkAsyncOperation(int iterations = 100, int itemsPerIteration = 1000)
		{
			_iterations = iterations;
			_itemsPerIteration = itemsPerIteration;
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (var i = 0; i < _iterations; i++)
			{
				var bulkResponse = await client.BulkAsync(b => b
					.IndexMany(Developer.Generator.Generate(_itemsPerIteration), (bd, d) => bd
						.Index(Infer.Index<Developer>())
						.Document(d)
					)).ConfigureAwait(false);

				if (!bulkResponse.IsValid)
					if (bulkResponse.Errors)
						foreach (var error in bulkResponse.ItemsWithErrors)
							output.WriteOrange($"error with id {error.Id}. message: {error.Error.Reason}");
			}
		}
	}
}