using System;
using System.Threading.Tasks;
using Nest;

namespace Profiling.Async
{
	public class PutIndexTemplateAsync : IAsyncProfiledOperation
	{
		public Task RunAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			throw new NotImplementedException();
		}
	}
}