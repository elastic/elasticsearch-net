using System.Threading.Tasks;
using Nest;

namespace Profiling.Async
{
	public interface IAsyncProfiledOperation
	{
		Task RunAsync(IElasticClient client, ColoredConsoleWriter output);
	}
}