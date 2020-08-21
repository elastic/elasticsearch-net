// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Stack.ArtifactsApi;

namespace Tests.Configuration
{
	public abstract class TestConfigurationBase
	{
		/// <summary> If specified will only run integration test against these comma separated clusters </summary>
		public string ClusterFilter { get; protected set; }

		/// <summary> comma separated list of test method names to execute </summary>
		public string TestFilter { get; protected set; }

		/// <summary> The Elasticsearch version to test against, defined for both unit and integration tests</summary>
		public ElasticVersion ElasticsearchVersion { get; protected set; }

		public bool ElasticsearchVersionIsSnapshot => ElasticsearchVersion.ArtifactBuildState == ArtifactBuildState.Snapshot;

		/// <summary> Force a reseed (bootstrap) of the cluster even if checks indicate bootstrap already ran </summary>
		public bool ForceReseed { get; protected set; }

		/// <summary>
		/// Signals to our test framework that the cluster was started externally. The framework will assert this before
		/// attempting to spin up a cluster. If no node is found the framework will still spin up a node.
		/// </summary>
		public bool TestAgainstAlreadyRunningElasticsearch { get; protected set; }

		/// <summary> The mode to run the tests under <see cref="TestMode"/></summary>
		public TestMode Mode { get; protected set; }

		/// <summary> Some test parameters are randomized, these are found under this property</summary>
		public RandomConfiguration Random { get; protected set; }

		/// <summary> The current configuration signals integration tests should be run </summary>
		public bool RunIntegrationTests => Mode == TestMode.Mixed || Mode == TestMode.Integration;

		/// <summary> The current configuration signals unit tests should be run </summary>
		public bool RunUnitTests => Mode == TestMode.Mixed || Mode == TestMode.Unit;

		/// <summary> The current configured seed used for random configuration </summary>
		public int Seed { get; private set; }

		/// <summary> whether the seed was provided externally to be fixated </summary>
		public bool SeedProvidedExternally { get; private set; }

		/// <summary>
		/// This is fixed for now, specifying false leads to flaky tests, warrants deeper investigation
		/// in our abstractions project
		/// </summary>
		public bool ShowElasticsearchOutputAfterStarted { get; } = true;

		/// <summary> When specified will only run one overload in API tests, helpful when debugging locally </summary>
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
		/// <summary> Run tests with a custom source serializer rather than the build in one </summary>
		public bool SourceSerializer { get; set; }

		/// <summary> Randomly enable typed keys on searches (defaults to true) on NEST search requests</summary>
		public bool TypedKeys { get; set; }

		/// <summary> Randomly enable compression on the http requests</summary>
		public bool HttpCompression { get; set; }
	}
}
