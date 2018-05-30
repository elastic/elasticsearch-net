using Elastic.Managed.Configuration;

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


		public virtual bool RunIntegrationTests => Mode == TestMode.Mixed || Mode == TestMode.Integration;
		public virtual bool RunUnitTests => Mode == TestMode.Mixed || Mode == TestMode.Unit;

		public abstract int Seed { get; protected set; }
		public RandomConfiguration Random { get; protected set; }
	}
}
