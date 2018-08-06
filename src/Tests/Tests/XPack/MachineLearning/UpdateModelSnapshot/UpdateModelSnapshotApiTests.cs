using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.UpdateModelSnapshot
{
	public class UpdateModelSnapshotApiTests : MachineLearningIntegrationTestBase<IUpdateModelSnapshotResponse, IUpdateModelSnapshotRequest, UpdateModelSnapshotDescriptor, UpdateModelSnapshotRequest>
	{
		public UpdateModelSnapshotApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateModelSnapshot(CallIsolatedValue, CallIsolatedValue + "-snapshot", f),
			fluentAsync: (client, f) => client.UpdateModelSnapshotAsync(CallIsolatedValue, CallIsolatedValue + "-snapshot", f),
			request: (client, r) => client.UpdateModelSnapshot(r),
			requestAsync: (client, r) => client.UpdateModelSnapshotAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				IndexSnapshot(client, callUniqueValue.Value, callUniqueValue.Value + "-snapshot");
			}
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/{CallIsolatedValue}-snapshot/_update";
		protected override bool SupportsDeserialization => false;
		protected override UpdateModelSnapshotDescriptor NewDescriptor() => new UpdateModelSnapshotDescriptor(CallIsolatedValue, CallIsolatedValue + "-snapshot");

		protected override object ExpectJson => new
			{
				description = "Modified snapshot description"
			};

		protected override Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> Fluent => f => f
			.Description("Modified snapshot description");

		protected override UpdateModelSnapshotRequest Initializer =>
			new UpdateModelSnapshotRequest(CallIsolatedValue, CallIsolatedValue + "-snapshot")
			{
				Description = "Modified snapshot description",
			};

		protected override void ExpectResponse(IUpdateModelSnapshotResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
