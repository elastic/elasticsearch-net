using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Profiling.Async
{
	public class GetMappingAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;

		public GetMappingAsyncOperation(int iterations = 1000)
		{
			_iterations = iterations;
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (var i = 0; i < _iterations; i++)
			{
				var mappingResponse = await client.GetMappingAsync<Project>(s => s.Index<Project>()).ConfigureAwait(false);

				if (!mappingResponse.IsValid)
					output.WriteOrange("Invalid response for get mapping operation");
			}
		}
	}
}