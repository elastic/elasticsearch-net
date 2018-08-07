using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.AcknowledgeWatch
{
	public class AcknowledgeWatchApiTests : ApiIntegrationTestBase<XPackCluster, IAcknowledgeWatchResponse, IAcknowledgeWatchRequest, AcknowledgeWatchDescriptor, AcknowledgeWatchRequest>
	{
		public AcknowledgeWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// set up a watch that can be acknowledged
			foreach (var callUniqueValue in values)
			{
				var watchId = callUniqueValue.Value;

				var putWatchResponse = client.PutWatch(watchId, p => p
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
							.DocType("acknowledgement")
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up PutWatch for integration test");

				var getWatchResponse = client.GetWatch(watchId);
				if (!getWatchResponse.IsValid)
					throw new Exception("Problem with GetWatch for integration test");

				getWatchResponse.Status.Actions["test_index"].Acknowledgement.State.Should().Be(AcknowledgementState.AwaitsSuccessfulExecution);

				var executeWatchResponse = client.ExecuteWatch(e => e
					.Id(watchId)
					.RecordExecution()
				);

				if (!executeWatchResponse.IsValid)
					throw new Exception("Problem with ExecuteWatch for integration test");

				getWatchResponse = client.GetWatch(watchId);
				if (!getWatchResponse.IsValid)
					throw new Exception("Problem with GetWatch for integration test");

				getWatchResponse.Status.Actions["test_index"].Acknowledgement.State.Should().Be(AcknowledgementState.Acknowledgable);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.AcknowledgeWatch(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.AcknowledgeWatchAsync(CallIsolatedValue, f),
			request: (client, r) => client.AcknowledgeWatch(r),
			requestAsync: (client, r) => client.AcknowledgeWatchAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"/_xpack/watcher/watch/{CallIsolatedValue}/_ack/test_index";

		protected override AcknowledgeWatchDescriptor NewDescriptor() => new AcknowledgeWatchDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> Fluent => f => f
			.ActionId("test_index");

		protected override AcknowledgeWatchRequest Initializer =>
			new AcknowledgeWatchRequest(CallIsolatedValue, "test_index");

		protected override void ExpectResponse(IAcknowledgeWatchResponse response)
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
