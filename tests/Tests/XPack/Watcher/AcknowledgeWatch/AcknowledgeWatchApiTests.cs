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
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.AcknowledgeWatch
{
	public class AcknowledgeWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, AcknowledgeWatchResponse, IAcknowledgeWatchRequest, AcknowledgeWatchDescriptor,
			AcknowledgeWatchRequest>
	{
		public AcknowledgeWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> Fluent => f => f
			.ActionId("test_index");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override AcknowledgeWatchRequest Initializer =>
			new AcknowledgeWatchRequest(CallIsolatedValue, "test_index");

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}/_ack/test_index";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// set up a watch that can be acknowledged
			foreach (var callUniqueValue in values)
			{
				var watchId = callUniqueValue.Value;

				var putWatchResponse = client.Watcher.Put(watchId, p => p
					.Input(i => i
						.Simple(s => s
							.Add("payload", new { send = "yes" })
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Hourly(h => h
								.Minute(0, 5)
							)
						)
					)
					.Condition(co => co
						.Always()
					)
					.Actions(a => a
						.Index("test_index", i => i
							.ThrottlePeriod("15m")
							.Index("test-" + CallIsolatedValue)
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up PutWatch for integration test");

				var getWatchResponse = client.Watcher.Get(watchId);
				if (!getWatchResponse.IsValid)
					throw new Exception("Problem with GetWatch for integration test");

				getWatchResponse.Status.Actions["test_index"].Acknowledgement.State.Should().Be(AcknowledgementState.AwaitsSuccessfulExecution);

				var executeWatchResponse = client.Watcher.Execute(e => e
					.Id(watchId)
					.RecordExecution()
				);

				if (!executeWatchResponse.IsValid)
					throw new Exception("Problem with ExecuteWatch for integration test");

				getWatchResponse = client.Watcher.Get(watchId);
				if (!getWatchResponse.IsValid)
					throw new Exception("Problem with GetWatch for integration test");

				getWatchResponse.Status.Actions["test_index"].Acknowledgement.State.Should().Be(AcknowledgementState.Acknowledgable);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Acknowledge(CallIsolatedValue, f),
			(client, f) => client.Watcher.AcknowledgeAsync(CallIsolatedValue, f),
			(client, r) => client.Watcher.Acknowledge(r),
			(client, r) => client.Watcher.AcknowledgeAsync(r)
		);

		protected override AcknowledgeWatchDescriptor NewDescriptor() => new AcknowledgeWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(AcknowledgeWatchResponse response)
		{
			var watchStatusState = response.Status.State;

			watchStatusState.Should().NotBeNull();
			watchStatusState.Active.Should().BeTrue();
			watchStatusState.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);

			response.Status.Actions.Should().NotBeNull().And.ContainKey("test_index");
			var indexAction = response.Status.Actions["test_index"];

			var acknowledgeState = indexAction.Acknowledgement;
			acknowledgeState.Should().NotBeNull();
			acknowledgeState.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			acknowledgeState.State.Should().Be(AcknowledgementState.Acknowledged);

			var lastExecutionState = indexAction.LastExecution;
			lastExecutionState.Should().NotBeNull();
			lastExecutionState.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			lastExecutionState.Successful.Should().Be(true);

			var lastSuccessfulExecutionState = indexAction.LastSuccessfulExecution;
			lastSuccessfulExecutionState.Should().NotBeNull();
			lastSuccessfulExecutionState.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			lastSuccessfulExecutionState.Successful.Should().Be(true);
		}
	}
}
