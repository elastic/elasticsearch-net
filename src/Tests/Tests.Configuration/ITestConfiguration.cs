namespace Tests.Configuration
{
	public interface ITestConfiguration
	{
		string ClusterFilter { get; }
		string ElasticsearchVersion { get; }
		bool ForceReseed { get; }
		TestMode Mode { get; }

		RandomConfiguration Random { get; }

		bool RunIntegrationTests { get; }
		bool RunUnitTests { get; }

		int Seed { get; }
		bool ShowElasticsearchOutputAfterStarted { get; }
		bool TestAgainstAlreadyRunningElasticsearch { get; }
		string TestFilter { get; }
	}

	public class RandomConfiguration
	{
		public bool SourceSerializer { get; set; }
		public bool TypedKeys { get; set; }
	}
}
