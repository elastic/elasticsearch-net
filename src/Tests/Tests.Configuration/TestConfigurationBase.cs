namespace Tests.Configuration
{
	public abstract class TestConfigurationBase : ITestConfiguration
	{
		protected const string DefaultVersion = "6.0.0";
		public abstract bool TestAgainstAlreadyRunningElasticsearch { get; protected set; }
		public abstract string ElasticsearchVersion { get; protected set; }
		public abstract bool ForceReseed { get; protected set; }
		public abstract TestMode Mode { get; protected set; }
		public abstract string ClusterFilter { get; protected set; }
		public abstract string TestFilter { get; protected set; }
		public abstract bool ShowElasticsearchOutputAfterStarted { get; protected set; }


		public virtual bool RunIntegrationTests => this.Mode == TestMode.Mixed || this.Mode == TestMode.Integration;
		public virtual bool RunUnitTests => this.Mode == TestMode.Mixed || this.Mode == TestMode.Unit;

		public abstract int Seed { get; protected set; }
		public RandomConfiguration Random { get; protected set; }
	}
}
