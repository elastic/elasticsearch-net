using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Elastic.Managed.Configuration;

namespace Tests.Framework.Configuration
{
	public class EnvironmentConfiguration : TestConfigurationBase
	{
		private const string DefaultVersion = "6.0.0";

		public sealed override bool TestAgainstAlreadyRunningElasticsearch { get; protected set; } = false;
		public sealed override bool ForceReseed { get; protected set; } = true;
		public sealed override ElasticsearchVersion ElasticsearchVersion { get; protected set; }
		public sealed override TestMode Mode { get; protected set; } = TestMode.Unit;
		public sealed override string ClusterFilter { get; protected set; }
		public sealed override string TestFilter { get; protected set; }
		public sealed override int Seed { get; protected set; }

		public EnvironmentConfiguration()
		{
			//if env var NEST_INTEGRATION_VERSION is set assume integration mode
			//used by the build script FAKE
			var version = Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");
			if (!string.IsNullOrEmpty(version)) Mode = TestMode.Integration;

			this.ElasticsearchVersion = ElasticsearchVersion.From(string.IsNullOrWhiteSpace(version) ? DefaultVersion : version);
			this.ClusterFilter = Environment.GetEnvironmentVariable("NEST_INTEGRATION_CLUSTER");
			this.TestFilter = Environment.GetEnvironmentVariable("NEST_TEST_FILTER");

			var newRandom = new Random().Next(1, 100000);

			this.Seed = TryGetEnv("NEST_TEST_SEED", out var seed) ? int.Parse(seed) : newRandom;
		    Randomizer.Seed = new Random(this.Seed);
			var randomizer = new Randomizer();

			this.Random = new RandomConfiguration
			{
				SourceSerializer = RandomBoolConfig("SOURCESERIALIZER", randomizer),
				TypedKeys = RandomBoolConfig("TYPEDKEYS", randomizer),
#if FEATURE_HTTPWEBREQUEST
				OldConnection = RandomBoolConfig("OLDCONNECTION", randomizer),
#else
				OldConnection = false
#endif
			};
		}

		private static bool RandomBoolConfig(string key, Randomizer randomizer)
		{
			if (TryGetEnv("NEST_RANDOM_" + key, out var source) && bool.TryParse(source, out var b))
				return b;
			return randomizer.Bool();
		}


		private static bool TryGetEnv(string key, out string value)
		{
			value = Environment.GetEnvironmentVariable(key);
			return !string.IsNullOrWhiteSpace(value);
		}
	}
}
