// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.GetWatch
{
	// TODO: there was already a bunch of commented code in this file which needs to be revalidated
	public class GetWatchApiTests : ApiIntegrationTestBase<WatcherCluster, GetWatchResponse, IGetWatchRequest, GetWatchDescriptor, GetWatchRequest>
	{
		public GetWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetWatchDescriptor, IGetWatchRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetWatchRequest Initializer =>
			new GetWatchRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => PutWatch(client, values);

		public static void PutWatch(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
					.Input(i => i
						.Chain(c => c
							.Input("simple", ci => ci
								.Simple(s => s
									.Add("str", "val1")
									.Add("num", 23)
									.Add("obj", new { str = "val2" })
								)
							)
							.Input("http", ci => ci
								.Http(h => h
									.Request(r => r
										.Host("localhost")
										.Port(8080)
										.Method(HttpInputMethod.Post)
										.Path("/path.html")
										.Proxy(pr => pr
											.Host("proxy")
											.Port(6000)
										)
										.Scheme(ConnectionScheme.Https)
										.Authentication(a => a
											.Basic(b => b
												.Username("Username123")
												.Password("Password123")
											)
										)
										.Body(
											"{\"query\" : {\"range\": {\"@timestamp\" : {\"from\": \"{{ctx.trigger.triggered_time}}||-5m\",\"to\": \"{{ctx.trigger.triggered_time}}\"}}}}")
										.Headers(he => he
											.Add("header1", "value1")
										)
										.Params(pa => pa
											.Add("lat", "52.374031")
											.Add("lon", "4.88969")
											.Add("appid", "appid")
										)
										.ConnectionTimeout("3s")
										.ReadTimeout(TimeSpan.FromMilliseconds(500))
									)
									.ResponseContentType(ResponseContentType.Text)
								)
							)
							.Input("search", ci => ci
								.Search(s => s
									.Request(si => si
										.Indices<Project>()
										.Body<Project>(b => b
											.Size(0)
											.Aggregations(a => a
												.Nested("nested_tags", n => n
													.Path(np => np.Tags)
													.Aggregations(aa => aa
														.Terms("top_project_tags", ta => ta
															.Field(f => f.Tags.First().Name)
														)
													)
												)
											)
										)
									)
								)
							)
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
									.Source("return [ 'time' : ctx.trigger.scheduled_time ]")
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
							.Attachments(ea => ea
								.HttpAttachment("http_attachment", ha => ha
									.Inline()
									.ContentType(RequestData.MimeType)
									.Request(r => r
										.Url("http://localhost:8080/http_attachment")
									)
								)
								.DataAttachment("data_attachment", da => da
									.Format(DataAttachmentFormat.Json)
								)
							)
						)
						.Index("reminder_index", i => i
							.Index("put-watch-test-index")
							.ExecutionTimeField("execution_time")
						)
						.PagerDuty("reminder_pagerduty", pd => pd
							.Account("my_pagerduty_account")
							.Description("pager duty description")
							.AttachPayload()
							.EventType(PagerDutyEventType.Trigger)
							.IncidentKey("incident_key")
							.Context(c => c
								.Context(PagerDutyContextType.Image, cd => cd
									.Src("http://example.com/image")
								)
								.Context(PagerDutyContextType.Link, cd => cd
									.Href("http://example.com/link")
								)
							)
						)
						.Slack("reminder_slack", sl => sl
							.Account("monitoring")
							.Message(sm => sm
								.From("nest integration test")
								.To("#nest")
								.Text("slack message")
								.Attachments(sa => sa
									.Attachment(saa => saa
										.Title("Attachment 1")
										.AuthorName("Russ Cam")
									)
								)
							)
						)
						.Webhook("webhook", w => w
							.Scheme(ConnectionScheme.Https)
							.Host("localhost")
							.Port(9200)
							.Method(HttpInputMethod.Post)
							.Path("/_bulk")
							.Authentication(au => au
								.Basic(b => b
									.Username("username")
									.Password("password")
								)
							)
							.Body("{{ctx.payload._value}}")
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception($"Problem setting up integration test: {putWatchResponse.DebugInformation}");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Get(CallIsolatedValue, f),
			(client, f) => client.Watcher.GetAsync(CallIsolatedValue, f),
			(client, r) => client.Watcher.Get(r),
			(client, r) => client.Watcher.GetAsync(r)
		);

		protected override GetWatchDescriptor NewDescriptor() => new GetWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetWatchResponse response)
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

			//TODO find out how to reuse Watch on request and response properly

//			var trigger = watch.Trigger;
//			trigger.Should().NotBeNull();
//			trigger.Schedule.Should().NotBeNull();
//			trigger.Schedule.Cron.Should().NotBeNull();
//			trigger.Schedule.Cron.ToString().Should().Be("0 5 9 * * ?");
//
//			watch.Input.Should().NotBeNull();
//			var chainInput = watch.Input.Chain;
//			chainInput.Should().NotBeNull();
//			chainInput.Inputs.Should().NotBeNull().And.HaveCount(3);
//
//			var simpleInput = ((IInputContainer)chainInput.Inputs["simple"]).Simple;
//			simpleInput.Should().NotBeNull();
//			simpleInput.Payload.Should().NotBeNull();
//
//			var httpInput = ((IInputContainer)chainInput.Inputs["http"]).Http;
//			httpInput.Should().NotBeNull();
//			httpInput.Request.Should().NotBeNull();
//
//			var searchInput = ((IInputContainer)chainInput.Inputs["search"]).Search;
//			searchInput.Should().NotBeNull();
//			searchInput.Request.Should().NotBeNull();
//
//			watch.Transform.Should().NotBeNull();
//			watch.Transform.Chain.Should().NotBeNull();
//			var chainTransforms = watch.Transform.Chain.Transforms;
//			chainTransforms.Should().NotBeNull().And.HaveCount(2);
//			var firstTransform = chainTransforms.First();
//			firstTransform.Should().NotBeNull();
//			((ITransformContainer)firstTransform).Search.Should().NotBeNull();
//
//			var lastTransform = chainTransforms.Last();
//			lastTransform.Should().NotBeNull();
//			((ITransformContainer)lastTransform).Script.Should().NotBeNull();
//
//			watch.Condition.Should().NotBeNull();
//			watch.Condition.Always.Should().NotBeNull();
//
//			watch.Actions.Should().NotBeNull().And.ContainKey("reminder_email");
//			watch.Actions.Should().NotBeNull().And.ContainKey("reminder_index");
//			watch.Actions.Should().NotBeNull().And.ContainKey("reminder_pagerduty");
//			watch.Actions.Should().NotBeNull().And.ContainKey("reminder_slack");
//			watch.Actions.Should().NotBeNull().And.ContainKey("reminder_hipchat");
//			watch.Actions.Should().NotBeNull().And.ContainKey("webhook");
//
//			var webhook = (IWebhookAction)watch.Actions["webhook"];
//			webhook.Authentication.Should().NotBeNull();
		}
	}

	public class GetNonExistentWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, GetWatchResponse, IGetWatchRequest, GetWatchDescriptor, GetWatchRequest>
	{
		public GetNonExistentWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 404;

		protected override Func<GetWatchDescriptor, IGetWatchRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetWatchRequest Initializer => new GetWatchRequest(CallIsolatedValue + "x");

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue + "x"}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => GetWatchApiTests.PutWatch(client, values);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Get(CallIsolatedValue + "x", f),
			(client, f) => client.Watcher.GetAsync(CallIsolatedValue + "x", f),
			(client, r) => client.Watcher.Get(r),
			(client, r) => client.Watcher.GetAsync(r)
		);

		protected override GetWatchDescriptor NewDescriptor() => new GetWatchDescriptor(CallIsolatedValue + "x");

		protected override void ExpectResponse(GetWatchResponse response)
		{
			response.Found.Should().BeFalse();
			response.Id.Should().Be(CallIsolatedValue + "x");
			response.Status.Should().BeNull();
			response.Watch.Should().BeNull();
		}
	}
}
