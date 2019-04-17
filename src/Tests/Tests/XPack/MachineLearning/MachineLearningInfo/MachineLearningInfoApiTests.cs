using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

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
			(client, f) => client.MachineLearningInfo(f),
			(client, f) => client.MachineLearningInfoAsync(f),
			(client, r) => client.MachineLearningInfo(r),
			(client, r) => client.MachineLearningInfoAsync(r)
		);

		protected override MachineLearningInfoDescriptor NewDescriptor() => new MachineLearningInfoDescriptor();

		protected override void ExpectResponse(MachineLearningInfoResponse response)
		{
			response.ShouldBeValid();

			response.Defaults.AnomalyDetectors.ModelMemoryLimit.Should().Be("1gb");
			response.Defaults.AnomalyDetectors.CategorizationExamplesLimit.Should().Be(4);
			response.Defaults.AnomalyDetectors.ModelSnapshotRetentionDays.Should().Be(1);

			response.Defaults.Datafeeds.ScrollSize.Should().Be(1000);
		}
	}
}
