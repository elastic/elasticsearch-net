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
