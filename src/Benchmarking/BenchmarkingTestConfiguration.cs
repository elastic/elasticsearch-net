using System.IO;
using Tests.Framework.Configuration;

namespace Benchmarking
{
	public class BenchmarkingTestConfiguration : YamlConfiguration
	{
	    private static readonly string YamlConfigurationPath;

        static BenchmarkingTestConfiguration()
	    {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

            // If running the classic .NET solution, runs from bin/{config} directory,
            // but when running DNX solution, runs from the test project root
            YamlConfigurationPath = directoryInfo.Name == "Benchmarking" &&
                                        directoryInfo.Parent != null &&
                                        directoryInfo.Parent.Name == "src"
                ? @"..\Tests\tests.yaml"
                : @"..\..\..\Tests\tests.yaml";
        }

		public override bool RunIntegrationTests => true;

		public override bool ForceReseed { get; protected set; } = true;

		public override bool DoNotSpawnIfAlreadyRunning { get; protected set; } = false;

		public BenchmarkingTestConfiguration()
			: base(YamlConfigurationPath)
		{
		}
	}
}