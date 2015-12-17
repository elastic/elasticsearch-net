using System.Threading.Tasks;
using Nest;

namespace Profiling
{
    public interface IAsyncProfiledOperation
    {
        Task RunAsync(IElasticClient client, ColoredConsoleWriter output);
    }
}