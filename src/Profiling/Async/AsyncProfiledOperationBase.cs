using System.Threading.Tasks;
using Nest;

namespace Profiling.Async
{
	public abstract class AsyncProfiledOperationBase : IAsyncProfiledOperation
	{
		public virtual Task SetupAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			return Task.FromResult(0);
		}

		public virtual Task TeardownAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			return Task.FromResult(0);
		}

		public abstract Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output);

		public async Task RunAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			await SetupAsync(client, output).ConfigureAwait(false);

			output.Write($"Start {this.GetType().Name} operation");

			using (Profiler.Start())
			{
				await ProfileAsync(client, output).ConfigureAwait(false);
			}

			output.Write($"Finished {this.GetType().Name} operation");

			await TeardownAsync(client, output).ConfigureAwait(false);
		}
	}
}