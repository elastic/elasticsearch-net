// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.UpdateModelSnapshot
{
	public class UpdateModelSnapshotApiTests
		: MachineLearningIntegrationTestBase<UpdateModelSnapshotResponse, IUpdateModelSnapshotRequest, UpdateModelSnapshotDescriptor,
			UpdateModelSnapshotRequest>
	{
		public UpdateModelSnapshotApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			description = "Modified snapshot description"
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> Fluent => f => f
			.Description("Modified snapshot description");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateModelSnapshotRequest Initializer =>
			new UpdateModelSnapshotRequest(CallIsolatedValue, CallIsolatedValue + "-snapshot")
			{
				Description = "Modified snapshot description",
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/{CallIsolatedValue}-snapshot/_update";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.UpdateModelSnapshot(CallIsolatedValue, CallIsolatedValue + "-snapshot", f),
			(client, f) => client.MachineLearning.UpdateModelSnapshotAsync(CallIsolatedValue, CallIsolatedValue + "-snapshot", f),
			(client, r) => client.MachineLearning.UpdateModelSnapshot(r),
			(client, r) => client.MachineLearning.UpdateModelSnapshotAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) IndexSnapshot(client, callUniqueValue.Value, callUniqueValue.Value + "-snapshot");
		}

		protected override UpdateModelSnapshotDescriptor NewDescriptor() =>
			new UpdateModelSnapshotDescriptor(CallIsolatedValue, CallIsolatedValue + "-snapshot");

		protected override void ExpectResponse(UpdateModelSnapshotResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
