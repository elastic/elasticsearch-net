/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elasticsearch.Net;
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
