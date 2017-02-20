using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.ActivateWatch
{
	public class ActivateWatchApiTests : ApiIntegrationTestBase<XPackCluster, IActivateWatchResponse, IActivateWatchRequest, ActivateWatchDescriptor, ActivateWatchRequest>
	{
		public ActivateWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.PutWatch(callUniqueValue.Value, p => p
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
			fluent: (client, f) => client.ActivateWatch(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.ActivateWatchAsync(CallIsolatedValue, f),
			request: (client, r) => client.ActivateWatch(r),
			requestAsync: (client, r) => client.ActivateWatchAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"/_xpack/watcher/watch/{CallIsolatedValue}/_activate";

		protected override bool SupportsDeserialization => true;

		protected override ActivateWatchDescriptor NewDescriptor() => new ActivateWatchDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<ActivateWatchDescriptor, IActivateWatchRequest> Fluent => f => f;

		protected override ActivateWatchRequest Initializer =>
			new ActivateWatchRequest(CallIsolatedValue);

		protected override void ExpectResponse(IActivateWatchResponse response)
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
