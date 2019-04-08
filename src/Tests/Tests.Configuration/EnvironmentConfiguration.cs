using System;

namespace Tests.Configuration
{
	public class EnvironmentConfiguration : TestConfigurationBase
	{
		public EnvironmentConfiguration(YamlConfiguration yamlConfiguration)
		{
			ClusterFilter = Environment.GetEnvironmentVariable("NEST_INTEGRATION_CLUSTER");
			TestFilter = Environment.GetEnvironmentVariable("NEST_TEST_FILTER");

			var version = Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");
			if (!string.IsNullOrEmpty(version)) Mode = TestMode.Integration;

			ElasticsearchVersion = string.IsNullOrWhiteSpace(version) ? yamlConfiguration.ElasticsearchVersion : version;
			if (string.IsNullOrWhiteSpace(ElasticsearchVersion))
				throw new Exception("Elasticsearch Version could not be determined from env var NEST_INTEGRATION_VERSION nor the test yaml configuration");

			var externalSeed = TryGetEnv("NEST_TEST_SEED", out var seed)
				? int.Parse(seed)
				: yamlConfiguration.SeedProvidedExternally
					? yamlConfiguration.Seed
					: (int?)null;
			SetExternalSeed(externalSeed, out var randomizer);

			TestOnlyOne = RandomBoolConfig("TEST_ONLY_ONE", randomizer, false);
			Random = new RandomConfiguration
			{
				SourceSerializer = RandomBoolConfig("SOURCESERIALIZER", randomizer),
				TypedKeys = RandomBoolConfig("TYPEDKEYS", randomizer),
			};
		}

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
