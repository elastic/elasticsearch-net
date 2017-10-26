using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.Versions;

namespace Tests.Framework.Configuration
{
	public abstract class TestConfigurationBase : ITestConfiguration
	{
		public abstract bool TestAgainstAlreadyRunningElasticsearch { get; protected set; }
		public abstract ElasticsearchVersion ElasticsearchVersion { get; protected set; }
		public abstract bool ForceReseed { get; protected set; }
		public abstract TestMode Mode { get; protected set; }
		public abstract string ClusterFilter { get; protected set; }
		public abstract string TestFilter { get; protected set; }
		public abstract bool UsingCustomSourceSerializer { get; protected set; }
		public abstract int Seed { get; protected set; }

		public virtual bool RunIntegrationTests => Mode == TestMode.Mixed || Mode == TestMode.Integration;
		public virtual bool RunUnitTests => Mode == TestMode.Mixed || Mode == TestMode.Unit;
	}
}
