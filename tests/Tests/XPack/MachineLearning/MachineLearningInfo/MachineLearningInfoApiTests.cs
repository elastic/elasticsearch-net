// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.MachineLearningInfo
{
	public class MachineLearningInfoApiTests
		: MachineLearningIntegrationTestBase<MachineLearningInfoResponse, IMachineLearningInfoRequest, MachineLearningInfoDescriptor, MachineLearningInfoRequest>
	{
		public MachineLearningInfoApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override MachineLearningInfoRequest Initializer => new MachineLearningInfoRequest();

		protected override string UrlPath => $"_ml/info";
		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.Info(f),
			(client, f) => client.MachineLearning.InfoAsync(f),
			(client, r) => client.MachineLearning.Info(r),
			(client, r) => client.MachineLearning.InfoAsync(r)
		);

		protected override MachineLearningInfoDescriptor NewDescriptor() => new MachineLearningInfoDescriptor();

		protected override void ExpectResponse(MachineLearningInfoResponse response)
		{
			response.ShouldBeValid();

			var anomalyDetectors = response.Defaults.AnomalyDetectors;
			anomalyDetectors.ModelMemoryLimit.Should().Be("1gb");
			anomalyDetectors.CategorizationExamplesLimit.Should().Be(4);

			anomalyDetectors.ModelSnapshotRetentionDays.Should().Be(TestClient.Configuration.InRange(">=7.8.0")
				? 10
				: 1);

			if (TestClient.Configuration.InRange(">=7.8.0"))
				anomalyDetectors.DailyModelSnapshotRetentionAfterDays.Should().Be(1);

			response.Defaults.Datafeeds.ScrollSize.Should().Be(1000);

			if (Cluster.ClusterConfiguration.Version >= "7.6.0")
			{
				var analyzer = anomalyDetectors.CategorizationAnalyzer;
				analyzer.Should().NotBeNull();
				analyzer.Tokenizer.Should().Be("ml_classic");
				analyzer.Filter.Should().NotBeNullOrEmpty();
			}

			if (TestClient.Configuration.InRange(">=7.8.0"))
				response.Limits.EffectiveMaxModelMemoryLimit.Should().NotBeNullOrEmpty();
		}
	}
}
