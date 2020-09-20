// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteModelSnapshot
{
	public class DeleteModelSnapshotApiTests
		: MachineLearningIntegrationTestBase<DeleteModelSnapshotResponse, IDeleteModelSnapshotRequest, DeleteModelSnapshotDescriptor,
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

				client.MachineLearning.GetModelSnapshots(callUniqueValue.Value).Count.Should().Be(1);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteModelSnapshot(CallIsolatedValue, "1", f),
			(client, f) => client.MachineLearning.DeleteModelSnapshotAsync(CallIsolatedValue, "1", f),
			(client, r) => client.MachineLearning.DeleteModelSnapshot(r),
			(client, r) => client.MachineLearning.DeleteModelSnapshotAsync(r)
		);

		protected override DeleteModelSnapshotDescriptor NewDescriptor() => new DeleteModelSnapshotDescriptor(CallIsolatedValue, "1");

		protected override void ExpectResponse(DeleteModelSnapshotResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
