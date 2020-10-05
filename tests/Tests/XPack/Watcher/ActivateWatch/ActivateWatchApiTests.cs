// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.ActivateWatch
{
	public class ActivateWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, ActivateWatchResponse, IActivateWatchRequest, ActivateWatchDescriptor, ActivateWatchRequest>
	{
		public ActivateWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<ActivateWatchDescriptor, IActivateWatchRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override ActivateWatchRequest Initializer =>
			new ActivateWatchRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}/_activate";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
					.Active(false)
					.Input(i => i
						.Http(h => h
							.Request(r => r
								.Scheme(ConnectionScheme.Https)
								.Host("localhost")
								.Port(8080)
								.Method(HttpInputMethod.Get)
							)
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Yearly(y => y
								.Add(ty => ty
									.In(Month.January, Month.December)
									.On(1)
									.At("noon")
								)
							)
						)
					)
					.Actions(a => a
						.Logging("test_logging", l => l
							.ThrottlePeriod("15m")
							.Level(LogLevel.Debug)
							.Category("Test")
							.Text("Logging action test")
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up PutWatch for integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Activate(CallIsolatedValue, f),
			(client, f) => client.Watcher.ActivateAsync(CallIsolatedValue, f),
			(client, r) => client.Watcher.Activate(r),
			(client, r) => client.Watcher.ActivateAsync(r)
		);

		protected override ActivateWatchDescriptor NewDescriptor() => new ActivateWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(ActivateWatchResponse response)
		{
			var watchStatusState = response.Status.State;

			watchStatusState.Should().NotBeNull();
			watchStatusState.Active.Should().BeTrue();
			watchStatusState.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);

			response.Status.Actions.Should().NotBeNull().And.ContainKey("test_logging");
			var indexAction = response.Status.Actions["test_logging"];
			indexAction.Acknowledgement.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			indexAction.Acknowledgement.State.Should().Be(AcknowledgementState.AwaitsSuccessfulExecution);
		}
	}
}
