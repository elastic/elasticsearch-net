using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.Watcher.GetWatch
{
	public class GetWatchApiTests : ApiIntegrationTestBase<XPackCluster, IGetWatchResponse, IGetWatchRequest, GetWatchDescriptor, GetWatchRequest>
	{
		public GetWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => PutWatch(client, values);

		public static void PutWatch(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.PutWatch(callUniqueValue.Value, p => p
					.Input(i => i
						.Simple(s => s
							.Add("key", "value")
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Cron("0 5 9 * * ?")
						)
					)
					.Transform(tr => tr
						.Chain(ct => ct
							.Transform(ctt => ctt
								.Search(st => st
									.Request(str => str
										.Indices(typeof(Project))
										.SearchType(SearchType.DfsQueryThenFetch)
										.IndicesOptions(io => io
											.ExpandWildcards(ExpandWildcards.Open)
											.IgnoreUnavailable()
										)
										.Body<Project>(b => b
											.Query(q => q
												.Match(m => m
													.Field("state")
													.Query(StateOfBeing.Stable.ToString().ToLowerInvariant())
												)
											)
										)
									)
									.Timeout("10s")
								)
							)
							.Transform(ctt => ctt
								.Script(st => st
									.Inline("return [ 'time' : ctx.trigger.scheduled_time ]")
								)
							)
						)
					)
					.Actions(a => a
						.Email("reminder_email", e => e
							.To("me@example.com")
							.Subject("Something's strange in the neighbourhood")
							.Body(b => b
								.Text("Dear {{ctx.payload.name}}, by the time you read these lines, I'll be gone")
							)
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception($"Problem setting up integration test: {putWatchResponse.DebugInformation}");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetWatch(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetWatchAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetWatch(r),
			requestAsync: (client, r) => client.GetWatchAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/watcher/watch/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override GetWatchDescriptor NewDescriptor() => new GetWatchDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<GetWatchDescriptor, IGetWatchRequest> Fluent => f => f;

		protected override GetWatchRequest Initializer =>
			new GetWatchRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetWatchResponse response)
		{
			response.Found.Should().BeTrue();
			response.Id.Should().Be(CallIsolatedValue);

			var watchStatus = response.Status;
			watchStatus.Should().NotBeNull();
			watchStatus.Version.Should().Be(1);
			watchStatus.State.Should().NotBeNull();
			watchStatus.State.Active.Should().BeTrue();
			watchStatus.State.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);

			watchStatus.Actions.Should().NotBeNull().And.ContainKey("reminder_email");
			var watchStatusAction = watchStatus.Actions["reminder_email"];

			watchStatusAction.Acknowledgement.State.Should().Be(AcknowledgementState.AwaitsSuccessfulExecution);
			watchStatusAction.Acknowledgement.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);

			var watch = response.Watch;
			watch.Should().NotBeNull();

			var trigger = watch.Trigger;
			trigger.Should().NotBeNull();
			trigger.Schedule.Should().NotBeNull();
			trigger.Schedule.Cron.Should().NotBeNull();
			trigger.Schedule.Cron.ToString().Should().Be("0 5 9 * * ?");

			watch.Input.Should().NotBeNull();
			var simpleInput = watch.Input.Simple;
			simpleInput.Should().NotBeNull();
			simpleInput.Payload.Should().ContainKey("key");

			watch.Transform.Should().NotBeNull();
			watch.Transform.Chain.Should().NotBeNull();
			var chainTransforms = watch.Transform.Chain.Transforms;
			chainTransforms.Should().NotBeNull().And.HaveCount(2);
			//chainTransforms.First().Should().NotBeNull().And.BeOfType<ISearchTransform>();
			//chainTransforms.Last().Should().NotBeNull().And.BeOfType<IScriptTransform>();

			watch.Condition.Should().NotBeNull();
			watch.Condition.Always.Should().NotBeNull();

			watch.Actions.Should().NotBeNull().And.ContainKey("reminder_email");
		}
	}

	public class GetNonExistentWatchApiTests : ApiIntegrationTestBase<XPackCluster, IGetWatchResponse, IGetWatchRequest, GetWatchDescriptor, GetWatchRequest>
	{

		//TODO this setup should not be necessary but in 6.0.0-alpha1 if no `.watches` index exists the response is actually an error
		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => GetWatchApiTests.PutWatch(client, values);

		public GetNonExistentWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetWatch(CallIsolatedValue + "x", f),
			fluentAsync: (client, f) => client.GetWatchAsync(CallIsolatedValue + "x", f),
			request: (client, r) => client.GetWatch(r),
			requestAsync: (client, r) => client.GetWatchAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/watcher/watch/{CallIsolatedValue + "x"}";

		protected override bool SupportsDeserialization => true;

		protected override GetWatchDescriptor NewDescriptor() => new GetWatchDescriptor(CallIsolatedValue + "x");

		protected override object ExpectJson => null;

		protected override Func<GetWatchDescriptor, IGetWatchRequest> Fluent => f => f;

		protected override GetWatchRequest Initializer => new GetWatchRequest(CallIsolatedValue + "x");

		protected override void ExpectResponse(IGetWatchResponse response)
		{
			response.Found.Should().BeFalse();
			response.Id.Should().Be(CallIsolatedValue + "x");
			response.Status.Should().BeNull();
			response.Watch.Should().BeNull();
		}
	}
}
