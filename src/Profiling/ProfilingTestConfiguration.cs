using Tests.Framework.Configuration;

namespace Profiling
{
    public class ProfilingTestConfiguration : TestConfiguration
    {
        public override bool RunIntegrationTests { get; } = true;

        public override bool DoNotSpawnIfAlreadyRunning { get; } = true;

        public ProfilingTestConfiguration(string configurationFile) 
            : base(configurationFile)
        {
        }
    }
}