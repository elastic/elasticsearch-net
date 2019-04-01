using System;

namespace Tests.Configuration
{
	public class EnvironmentConfiguration : TestConfigurationBase
	{
		public EnvironmentConfiguration(YamlConfiguration tempYamlConfiguration) : this()
		{
			ElasticsearchVersion = tempYamlConfiguration.ElasticsearchVersion;
			Seed = tempYamlConfiguration.Seed;
		}

		public EnvironmentConfiguration()
		{
			//if env var NEST_INTEGRATION_VERSION is set assume integration mode used by the build script FAKE
			var version = Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");
			if (!string.IsNullOrEmpty(version)) Mode = TestMode.Integration;

			ElasticsearchVersion = string.IsNullOrWhiteSpace(version) ? DefaultVersion : version;
			ClusterFilter = Environment.GetEnvironmentVariable("NEST_INTEGRATION_CLUSTER");
			ShowElasticsearchOutputAfterStarted = Environment.GetEnvironmentVariable("NEST_INTEGRATION_SHOW_OUTPUT_AFTER_START") == "1";
			TestFilter = Environment.GetEnvironmentVariable("NEST_TEST_FILTER");

			var newRandom = new Random().Next(1, 100000);

			Seed = TryGetEnv("NEST_TEST_SEED", out var seed) ? int.Parse(seed) : newRandom;
			var randomizer = new Random(Seed);

			TestOnlyOne = RandomBoolConfig("TEST_ONLY_ONE", randomizer, false);

			Random = new RandomConfiguration
			{
				SourceSerializer = RandomBoolConfig("SOURCESERIALIZER", randomizer),
				TypedKeys = RandomBoolConfig("TYPEDKEYS", randomizer),
			};
		}

		public sealed override string ClusterFilter { get; protected set; }
		public sealed override string ElasticsearchVersion { get; protected set; }
		public sealed override bool ForceReseed { get; protected set; } = true;
		public sealed override bool TestOnlyOne { get; protected set; } = false;
		public sealed override TestMode Mode { get; protected set; } = TestMode.Unit;
		public sealed override int Seed { get; protected set; }
		public sealed override bool ShowElasticsearchOutputAfterStarted { get; protected set; }
		public sealed override bool TestAgainstAlreadyRunningElasticsearch { get; protected set; } = false;
		public sealed override string TestFilter { get; protected set; }

		private static bool RandomBoolConfig(string key, Random randomizer, bool? @default = null)
		{
			if (TryGetEnv("NEST_RANDOM_" + key, out var source) && bool.TryParse(source, out var b))
				return b;

			return @default ?? randomizer.NextDouble() >= 0.5;
		}

		private static bool TryGetEnv(string key, out string value)
		{
			value = Environment.GetEnvironmentVariable(key);
			return !string.IsNullOrWhiteSpace(value);
		}
	}
}
