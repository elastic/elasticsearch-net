using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.Info
{
	public class MlInfoApiTests
		: MachineLearningIntegrationTestBase<IMlInfoResponse, IMlInfoRequest, MlInfoDescriptor, MlInfoRequest>
	{
		public MlInfoApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<MlInfoDescriptor, IMlInfoRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override MlInfoRequest Initializer => new MlInfoRequest();

		protected override string UrlPath => $"_xpack/ml/info";
		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MlInfo(f),
			(client, f) => client.MlInfoAsync(f),
			(client, r) => client.MlInfo(r),
			(client, r) => client.MlInfoAsync(r)
		);

		protected override MlInfoDescriptor NewDescriptor() => new MlInfoDescriptor();

		protected override void ExpectResponse(IMlInfoResponse response)
		{
			response.ShouldBeValid();

			response.Defaults.AnomalyDetectors.ModelMemoryLimit.Should().Be("1gb");
			response.Defaults.AnomalyDetectors.CategorizationExamplesLimit.Should().Be(4);
			response.Defaults.AnomalyDetectors.ModelSnapshotRetentionDays.Should().Be(1);

			response.Defaults.Datafeeds.ScrollSize.Should().Be(1000);
		}
	}
}
