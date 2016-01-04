using Tests.Framework.Configuration;

namespace Benchmarking
{
	public class BenchmarkingTestConfiguration : YamlConfiguration
	{
		public override bool RunIntegrationTests => true;

		public override bool ForceReseed { get; protected set; } = true;

		public override bool DoNotSpawnIfAlreadyRunning { get; protected set; } = false;

		public BenchmarkingTestConfiguration()
			: base(@"..\..\..\..\Tests\tests.yaml")
		{
		}
	}
}