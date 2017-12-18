using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.Versions;

namespace Tests.Framework.Configuration
{
	public class EnvironmentConfiguration : TestConfigurationBase
	{
		private const string DefaultVersion = "5.6.5";

		public override bool TestAgainstAlreadyRunningElasticsearch { get; protected set; } = false;
		public override bool ForceReseed { get; protected set; } = true;
		public override ElasticsearchVersion ElasticsearchVersion { get; protected set; } = ElasticsearchVersion.GetOrAdd(DefaultVersion);
		public override TestMode Mode { get; protected set; } = TestMode.Unit;
		public override string ClusterFilter { get; protected set; }
		public override string TestFilter { get; protected set; }

		public EnvironmentConfiguration()
		{
			//if env var NEST_INTEGRATION_VERSION is set assume integration mode
			//used by the build script FAKE
			var version = Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");
			if (!string.IsNullOrEmpty(version)) Mode = TestMode.Integration;

			this.ElasticsearchVersion = ElasticsearchVersion.GetOrAdd(string.IsNullOrWhiteSpace(version) ? DefaultVersion : version);
			this.ClusterFilter = Environment.GetEnvironmentVariable("NEST_INTEGRATION_CLUSTER");
			this.TestFilter = Environment.GetEnvironmentVariable("NEST_TEST_FILTER");
		}
	}
}
