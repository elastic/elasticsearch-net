using System;

namespace Tests.Configuration
{
	public abstract class TestConfigurationBase
	{
		public string ClusterFilter { get; protected set; }
		public string ElasticsearchVersion { get; protected set; }
		public bool ForceReseed { get; protected set; } = true;
		public TestMode Mode { get; protected set; }
		public RandomConfiguration Random { get; protected set; }

		public bool RunIntegrationTests => Mode == TestMode.Mixed || Mode == TestMode.Integration;
		public bool RunUnitTests => Mode == TestMode.Mixed || Mode == TestMode.Unit;

		public int Seed { get; private set; }
		public bool SeedProvidedExternally { get; private set; }

		public bool ShowElasticsearchOutputAfterStarted { get; protected set; } = true;
		public bool TestAgainstAlreadyRunningElasticsearch { get; protected set; }
		public string TestFilter { get; protected set; }
		public bool TestOnlyOne { get; protected set; }

		private static int CurrentSeed { get; } = new Random().Next(1, 1_00_000);

		protected void SetExternalSeed(int? seed, out Random randomizer)
		{
			SeedProvidedExternally = seed.HasValue;
			Seed = seed.GetValueOrDefault(CurrentSeed);
			randomizer = new Random(Seed);
		}
	}

	public class RandomConfiguration
	{
		public bool SourceSerializer { get; set; }
		public bool TypedKeys { get; set; }
	}
}
