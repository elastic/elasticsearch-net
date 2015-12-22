using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Profiling.Async
{
	public class IndexAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;

		public IndexAsyncOperation(int iterations = 1000)
		{
			_iterations = iterations;
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (var i = 0; i < _iterations; i++)
			{
				var indexResponse =
					await client.IndexAsync(Developer.Generator.Generate(), d => d.Index<Developer>()).ConfigureAwait(false);

				if (!indexResponse.IsValid)
					output.WriteOrange($"error with id {indexResponse.Id}. message: {indexResponse.CallDetails.OriginalException}");
			}
		}
	}
}