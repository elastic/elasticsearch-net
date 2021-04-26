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
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.DeactivateWatch
{
	public class DeactivateWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, DeactivateWatchResponse, IDeactivateWatchRequest, DeactivateWatchDescriptor, DeactivateWatchRequest>
	{
		public DeactivateWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override DeactivateWatchRequest Initializer =>
			new DeactivateWatchRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}/_deactivate";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
					.Active()
					.Input(i => i
						.Search(s => s
							.Request(si => si
								.Indices("_all")
								.Body<object>(sd => sd
									.MatchAll()
								)
							)
						)
					)
					.Condition(c => c
						.Never()
					)
					.Trigger(t => t
						.Schedule(s => s
							.Monthly(m => m
								.Add(ti => ti
									.On(1)
									.At("16:10")
								)
							)
						)
					)
					.Actions(a => a
						.Webhook("test_webhook", w => w
							.ThrottlePeriod("15m")
							.Host("localhost")
							.Port(8080)
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up PutWatch for integration test: " + putWatchResponse.DebugInformation);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Deactivate(CallIsolatedValue, f),
			(client, f) => client.Watcher.DeactivateAsync(CallIsolatedValue, f),
			(client, r) => client.Watcher.Deactivate(r),
			(client, r) => client.Watcher.DeactivateAsync(r)
		);

		protected override DeactivateWatchDescriptor NewDescriptor() => new DeactivateWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeactivateWatchResponse response)
		{
			var watchStatusState = response.Status.State;

			watchStatusState.Should().NotBeNull();
			watchStatusState.Active.Should().BeFalse();
			watchStatusState.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);

			response.Status.Actions.Should().NotBeNull().And.ContainKey("test_webhook");
			var indexAction = response.Status.Actions["test_webhook"];

			indexAction.Acknowledgement.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			indexAction.Acknowledgement.State.Should().Be(AcknowledgementState.AwaitsSuccessfulExecution);
		}
	}
}
