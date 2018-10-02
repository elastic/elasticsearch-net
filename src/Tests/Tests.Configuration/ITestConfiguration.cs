using System;
using System.Collections.Generic;

namespace Tests.Configuration
{
	public interface ITestConfiguration
	{
		TestMode Mode { get; }
		string ElasticsearchVersion { get; }
		string ClusterFilter { get; }
		string TestFilter { get; }
		bool ForceReseed { get; }
		bool TestAgainstAlreadyRunningElasticsearch { get; }

		int Seed { get; }

		bool RunIntegrationTests { get; }
		bool RunUnitTests { get; }

		RandomConfiguration Random { get; }
		bool ShowElasticsearchOutputAfterStarted { get; }
	}

	public class RandomConfiguration
	{
		public bool SourceSerializer { get; set; }
		public bool TypedKeys { get; set; }
	}

	public static class TestConfigurationExtensions
	{
		public static void DumpConfiguration(this ITestConfiguration config)
		{
			Console.WriteLine(new string('-', 20));
			Console.WriteLine("Starting tests using config:");
			Console.WriteLine($" - {nameof(config.TestAgainstAlreadyRunningElasticsearch)}: {config.TestAgainstAlreadyRunningElasticsearch}");
			Console.WriteLine($" - {nameof(config.ElasticsearchVersion)}: {config.ElasticsearchVersion}");
			Console.WriteLine($" - {nameof(config.ForceReseed)}: {config.ForceReseed}");
			Console.WriteLine($" - {nameof(config.Mode)}: {config.Mode}");
			Console.WriteLine($" - {nameof(config.Seed)}: {config.Seed}");
			if (config.Mode == TestMode.Integration)
			{
				Console.WriteLine($" - {nameof(config.ClusterFilter)}: {config.ClusterFilter}");
				Console.WriteLine($" - {nameof(config.TestFilter)}: {config.TestFilter}");

			}
			Console.WriteLine($" - {nameof(config.RunIntegrationTests)}: {config.RunIntegrationTests}");
			Console.WriteLine($" - {nameof(config.RunUnitTests)}: {config.RunUnitTests}");
			Console.WriteLine($" - Random:");
			Console.WriteLine($" \t- {nameof(config.Random.SourceSerializer)}: {config.Random.SourceSerializer}");
			Console.WriteLine($" \t- {nameof(config.Random.TypedKeys)}: {config.Random.TypedKeys}");
			Console.WriteLine(new string('-', 20));
		}
	}
}
