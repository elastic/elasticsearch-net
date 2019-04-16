using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteModelSnapshot
{
	public class DeleteModelSnapshotApiTests
		: MachineLearningIntegrationTestBase<IDeleteModelSnapshotResponse, IDeleteModelSnapshotRequest, DeleteModelSnapshotDescriptor,
			DeleteModelSnapshotRequest>
	{
		public DeleteModelSnapshotApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteModelSnapshotRequest Initializer => new DeleteModelSnapshotRequest(CallIsolatedValue, "1");
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/1";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexSnapshot(client, callUniqueValue.Value, "1");

				client.GetModelSnapshots(callUniqueValue.Value).Count.Should().Be(1);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteModelSnapshot(CallIsolatedValue, "1", f),
			(client, f) => client.DeleteModelSnapshotAsync(CallIsolatedValue, "1", f),
			(client, r) => client.DeleteModelSnapshot(r),
			(client, r) => client.DeleteModelSnapshotAsync(r)
		);

		protected override DeleteModelSnapshotDescriptor NewDescriptor() => new DeleteModelSnapshotDescriptor(CallIsolatedValue, "1");

		protected override void ExpectResponse(IDeleteModelSnapshotResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
