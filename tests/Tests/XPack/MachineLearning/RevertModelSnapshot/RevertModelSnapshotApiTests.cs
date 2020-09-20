// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.RevertModelSnapshot
{
	public class RevertModelSnapshotApiTests
		: MachineLearningIntegrationTestBase<RevertModelSnapshotResponse, IRevertModelSnapshotRequest, RevertModelSnapshotDescriptor,
			RevertModelSnapshotRequest>
	{
		public RevertModelSnapshotApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			delete_intervening_results = true
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> Fluent => f => f
			.DeleteInterveningResults();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override RevertModelSnapshotRequest Initializer => new RevertModelSnapshotRequest(CallIsolatedValue, "first")
		{
			DeleteInterveningResults = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/first/_revert";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
				CloseJob(client, callUniqueValue.Value);

				IndexSnapshot(client, callUniqueValue.Value, "first");
				IndexSnapshot(client, callUniqueValue.Value, "second", "2016-06-01T00:00:00Z");

				client.MachineLearning.GetModelSnapshots(callUniqueValue.Value).Count.Should().Be(2);
				client.Indices.Refresh(".ml-state");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.RevertModelSnapshot(CallIsolatedValue, "first", f),
			(client, f) => client.MachineLearning.RevertModelSnapshotAsync(CallIsolatedValue, "first", f),
			(client, r) => client.MachineLearning.RevertModelSnapshot(r),
			(client, r) => client.MachineLearning.RevertModelSnapshotAsync(r)
		);

		protected override RevertModelSnapshotDescriptor NewDescriptor() => new RevertModelSnapshotDescriptor(CallIsolatedValue, "first");

		protected override void ExpectResponse(RevertModelSnapshotResponse response) => response.Model.Should().NotBeNull();
	}
}
