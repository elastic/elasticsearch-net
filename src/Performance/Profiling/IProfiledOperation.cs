using Nest;

namespace Profiling
{
    public interface IProfiledOperation
    {
        void Run(IElasticClient client, ColoredConsoleWriter output);
    }
}