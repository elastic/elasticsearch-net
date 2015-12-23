using Tests.Framework.Configuration;

namespace Benchmarking
{
	public class BenchmarkingTestConfiguration : TestConfiguration
	{
		public override bool RunIntegrationTests { get; } = true;

		public override bool ForceReseed { get; } = true;

		public override bool DoNotSpawnIfAlreadyRunning { get; } = false;

		public BenchmarkingTestConfiguration()
			: base(@"..\..\..\..\Tests\tests.yml")
		{
		}
	}
}