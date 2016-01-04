using System.Threading.Tasks;
using Nest;

namespace Profiling.Async
{
	public class CatAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;

		public CatAsyncOperation(int iterations = 1000)
		{
			_iterations = iterations;
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (var i = 0; i < _iterations; i++)
			{
				var catAllocationAsync = client.CatAllocationAsync(s => s.V());
				var catCountAsync = client.CatCountAsync(s => s.V());
				var catFielddataAsync = client.CatFielddataAsync(s => s.V());
				var catHealthAsync = client.CatHealthAsync(s => s.V());
				var catIndicesAsync = client.CatIndicesAsync(s => s.V());
				var catNodesAsync = client.CatNodesAsync(s => s.V());
				var catPendingTasksAsync = client.CatPendingTasksAsync(s => s.V());
				var catThreadPoolAsync = client.CatThreadPoolAsync(s => s.V());
				var catAliasesAsync = client.CatAliasesAsync(s => s.V());

				await Task.WhenAll(
					catAllocationAsync,
					catCountAsync,
					catFielddataAsync,
					catHealthAsync,
					catIndicesAsync,
					catNodesAsync,
					catPendingTasksAsync,
					catThreadPoolAsync,
					catAliasesAsync
					).ConfigureAwait(false);

				if (!catAllocationAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat allocation");

				if (!catCountAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat count");

				if (!catFielddataAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat fielddata");

				if (!catHealthAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat health");

				if (!catIndicesAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat indices");

				if (!catNodesAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat nodes");

				if (!catPendingTasksAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat pending tasks");

				if (!catThreadPoolAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat thread pool");

				if (!catAliasesAsync.Result.IsValid)
					output.WriteOrange("invalid response received for cat aliases");
			}
		}
	}
}