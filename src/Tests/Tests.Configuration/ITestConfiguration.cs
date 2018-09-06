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
}
