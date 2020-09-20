// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Xunit;

namespace Tests.XPack.Watcher.ExecuteWatch
{
	public class ExecuteWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, ExecuteWatchResponse, IExecuteWatchRequest, ExecuteWatchDescriptor, ExecuteWatchRequest>
	{
		private readonly DateTimeOffset _triggeredDateTime = new DateTimeOffset(2016, 11, 17, 13, 00, 00, TimeSpan.Zero);

		public ExecuteWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			action_modes = new
			{
				email_admin = "force_simulate",
				webhook_action = "force_simulate",
				slack_action = "force_simulate",
				pagerduty_action = "force_simulate",
			},
			alternative_input = new
			{
				foo = "bar"
			},
			ignore_condition = true,
			record_execution = true,
			trigger_data = new
			{
				scheduled_time = "2016-11-17T13:00:00+00:00",
				triggered_time = "2016-11-17T13:00:00+00:00"
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ExecuteWatchDescriptor, IExecuteWatchRequest> Fluent => f => f
			.Id(CallIsolatedValue)
			.TriggerData(te => te
				.ScheduledTime(_triggeredDateTime)
				.TriggeredTime(_triggeredDateTime)
			)
			.AlternativeInput(i => i.Add("foo", "bar"))
			.IgnoreCondition()
			.ActionModes(a => a
				.Add("email_admin", ActionExecutionMode.ForceSimulate)
				.Add("webhook_action", ActionExecutionMode.ForceSimulate)
				.Add("slack_action", ActionExecutionMode.ForceSimulate)
				.Add("pagerduty_action", ActionExecutionMode.ForceSimulate)
			)
			.RecordExecution();

		protected override HttpMethod HttpMethod => HttpMethod.PUT;


		protected override ExecuteWatchRequest Initializer =>
			new ExecuteWatchRequest(CallIsolatedValue)
			{
				TriggerData = new ScheduleTriggerEvent
				{
					ScheduledTime = _triggeredDateTime,
					TriggeredTime = _triggeredDateTime
				},
				AlternativeInput = new Dictionary<string, object>
				{
					{ "foo", "bar" }
				},
				IgnoreCondition = true,
				ActionModes = new Dictionary<string, ActionExecutionMode>
				{
					{ "email_admin", ActionExecutionMode.ForceSimulate },
					{ "webhook_action", ActionExecutionMode.ForceSimulate },
					{ "slack_action", ActionExecutionMode.ForceSimulate },
					{ "pagerduty_action", ActionExecutionMode.ForceSimulate },
				},
				RecordExecution = true
			};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}/_execute";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
					.Input(i => i
						.Search(s => s
							.Request(r => r
								.Indices("logstash")
								.Body<object>(b => b
									.Query(q => q
											.Match(m => m
												.Field("response")
												.Query("404")
											) && +q
											.DateRange(ffrr => ffrr
												.Field("@timestamp")
												.GreaterThanOrEquals("{{ctx.trigger.scheduled_time}}||-5m")
												.LessThanOrEquals("{{ctx.trigger.triggered_time}}")
											)
									)
								)
							)
						)
					)
					.Condition(c => c
						.Script(ss => ss
							.Source("ctx.payload.hits.total > 1")
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Cron("0 0 0 1 * ? 2099")
						)
					)
					.Metadata(meta => meta.Add("foo", "bar"))
					.Actions(a => a
						.Email("email_admin", e => e
							.To("someone@domain.host.com")
							.Subject("404 recently encountered")
						)
						.Index("index_action", i => i
							.Index("test")
						)
						.Logging("logging_action", l => l
							.Text("404 recently encountered")
						)
						.Webhook("webhook_action", w => w
							.Host("foo.com")
							.Port(80)
							.Path("/bar")
							.Method(HttpInputMethod.Post)
							.Body("{}")
						)
						.PagerDuty("pagerduty_action", pd => pd
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
						.Slack("slack_action", sl => sl
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
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Execute(f),
			(client, f) => client.Watcher.ExecuteAsync(f),
			(client, r) => client.Watcher.Execute(r),
			(client, r) => client.Watcher.ExecuteAsync(r)
		);

		protected override void ExpectResponse(ExecuteWatchResponse response)
		{
			response.ShouldBeValid();
			response.WatchRecord.Should().NotBeNull();
			response.WatchRecord.WatchId.Should().Be(CallIsolatedValue);
			response.WatchRecord.State.Should().NotBeNull().And.Be(ActionExecutionState.Executed);
			response.WatchRecord.TriggerEvent.Should().NotBeNull();
			response.WatchRecord.TriggerEvent.Type.Should().Be("manual");
			response.WatchRecord.TriggerEvent.TriggeredTime.Should().Be(_triggeredDateTime);
			response.WatchRecord.TriggerEvent.Manual.Should().NotBeNull();
			response.WatchRecord.TriggerEvent.Manual.Schedule.Should().NotBeNull();
			response.WatchRecord.TriggerEvent.Manual.Schedule.ScheduledTime.Match(
				f => f.Should().Be(_triggeredDateTime),
				s => Assert.True(false, "expected a datetime")
			);

			response.WatchRecord.Result.Should().NotBeNull();
			response.WatchRecord.Result.Condition.Should().NotBeNull();
			response.WatchRecord.Result.Condition.Type.Should().Be(ConditionType.Always);
			response.WatchRecord.Result.Condition.Status.Should().Be(Status.Success);
			response.WatchRecord.Result.Condition.Met.Should().BeTrue();

			var resultActions = response.WatchRecord.Result.Actions;
			resultActions.Should().NotBeNullOrEmpty();
			resultActions.Count.Should().Be(6);

			var inputContainer = response.WatchRecord.Input as IInputContainer;
			inputContainer.Should().NotBeNull();
			inputContainer.Search.Should().NotBeNull();

			response.WatchRecord.Metadata.Should().NotBeNull();
			response.WatchRecord.Metadata.Should().Contain("foo", "bar");

			var emailAction = resultActions.FirstOrDefault(a => a.Id == "email_admin");
			emailAction.Should().NotBeNull();
			emailAction.Type.Should().Be(ActionType.Email);
			emailAction.Status.Should().Be(Status.Simulated);
			emailAction.Email.Should().NotBeNull();
			emailAction.Email.Message.SentDate.Should().HaveValue();

			var indexAction = resultActions.FirstOrDefault(a => a.Id == "index_action");
			indexAction.Should().NotBeNull();
			indexAction.Type.Should().Be(ActionType.Index);
			indexAction.Status.Should().Be(Status.Success);
			indexAction.Index.Response.Should().NotBeNull();
			indexAction.Index.Response.Index.Should().Be("test");
			indexAction.Index.Response.Created.Should().BeTrue();
			indexAction.Index.Response.Version.Should().Be(1);

			var loggingAction = resultActions.FirstOrDefault(a => a.Id == "logging_action");
			loggingAction.Should().NotBeNull();
			loggingAction.Type.Should().Be(ActionType.Logging);
			loggingAction.Status.Should().Be(Status.Success);
			loggingAction.Logging.LoggedText.Should().Be("404 recently encountered");

			var webhookAction = resultActions.FirstOrDefault(a => a.Id == "webhook_action");
			webhookAction.Should().NotBeNull();
			webhookAction.Type.Should().Be(ActionType.Webhook);
			webhookAction.Status.Should().Be(Status.Simulated);
			webhookAction.Webhook.Should().NotBeNull();
			webhookAction.Webhook.Request.Should().NotBeNull();
			webhookAction.Webhook.Request.Host.Should().Be("foo.com");

			var pagerDutyAction = resultActions.FirstOrDefault(a => a.Id == "pagerduty_action");
			pagerDutyAction.Should().NotBeNull();
			pagerDutyAction.Type.Should().Be(ActionType.PagerDuty);
			pagerDutyAction.Status.Should().Be(Status.Simulated);
			pagerDutyAction.PagerDuty.Should().NotBeNull();

			var slackAction = resultActions.FirstOrDefault(a => a.Id == "slack_action");
			slackAction.Should().NotBeNull();
			slackAction.Type.Should().Be(ActionType.Slack);
			slackAction.Status.Should().Be(Status.Simulated);
			slackAction.Slack.Should().NotBeNull();

			response.WatchRecord.Result.ExecutionTime.Should().HaveValue();
		}
	}

	public class ExecuteInlineWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, ExecuteWatchResponse, IExecuteWatchRequest, ExecuteWatchDescriptor, ExecuteWatchRequest>
	{
		private readonly DateTimeOffset _triggeredDateTime = new DateTimeOffset(2016, 11, 17, 13, 00, 00, TimeSpan.Zero);

		public ExecuteInlineWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			alternative_input = new
			{
				foo = "bar"
			},
			ignore_condition = true,
			trigger_data = new
			{
				scheduled_time = "2016-11-17T13:00:00+00:00",
				triggered_time = "2016-11-17T13:00:00+00:00"
			},
			watch = new
			{
				actions = new
				{
					email_admin = new
					{
						email = new
						{
							from = "nest-client@domain.example",
							subject = "404 recently encountered",
							to = new[] { "someone@domain.host.example" }
						}
					},
					index_action = new
					{
						index = new
						{
							index = "test"
						}
					},
					logging_action = new
					{
						logging = new
						{
							text = "404 recently encountered"
						}
					},
					webhook_action = new
					{
						webhook = new
						{
							body = "{}",
							host = "foo.com",
							method = "post",
							path = "/bar",
							port = 80
						}
					}
				},
				condition = new
				{
					script = new
					{
						source = "ctx.payload.hits.total > 1"
					}
				},
				input = new
				{
					search = new
					{
						request = new
						{
							body = new
							{
								query = new
								{
									@bool = new
									{
										filter = new[]
										{
											new
											{
												range = new Dictionary<string, object>
												{
													{
														"@timestamp", new Dictionary<string, object>
														{
															{ "gte", "{{ctx.trigger.scheduled_time}}||-5m" },
															{ "lte", "{{ctx.trigger.triggered_time}}" }
														}
													}
												}
											}
										},
										must = new[]
										{
											new
											{
												match = new
												{
													response = new
													{
														query = "404"
													}
												}
											}
										}
									}
								}
							},
							indices = new[] { "logstash" }
						}
					}
				},
				metadata = new
				{
					foo = "bar"
				},
				trigger = new
				{
					schedule = new
					{
						cron = "0 0 0 1 * ? 2099"
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ExecuteWatchDescriptor, IExecuteWatchRequest> Fluent => f => f
			.TriggerData(te => te
				.ScheduledTime(_triggeredDateTime)
				.TriggeredTime(_triggeredDateTime)
			)
			.AlternativeInput(i => i.Add("foo", "bar"))
			.IgnoreCondition()
			.Watch(wa => wa
				.Input(i => i
					.Search(s => s
						.Request(r => r
							.Indices("logstash")
							.Body<object>(b => b
								.Query(q => q
										.Match(m => m
											.Field("response")
											.Query("404")
										) && +q
										.DateRange(ffrr => ffrr
											.Field("@timestamp")
											.GreaterThanOrEquals("{{ctx.trigger.scheduled_time}}||-5m")
											.LessThanOrEquals("{{ctx.trigger.triggered_time}}")
										)
								)
							)
						)
					)
				)
				.Condition(c => c
					.Script(ss => ss
						.Source("ctx.payload.hits.total > 1")
					)
				)
				.Trigger(t => t
					.Schedule(s => s
						.Cron("0 0 0 1 * ? 2099")
					)
				)
				.Metadata(meta => meta.Add("foo", "bar"))
				.Actions(a => a
					.Email("email_admin", e => e
						.From("nest-client@domain.example")
						.To("someone@domain.host.example")
						.Subject("404 recently encountered")
					)
					.Index("index_action", i => i
						.Index("test")
					)
					.Logging("logging_action", l => l
						.Text("404 recently encountered")
					)
					.Webhook("webhook_action", w => w
						.Host("foo.com")
						.Port(80)
						.Path("/bar")
						.Method(HttpInputMethod.Post)
						.Body("{}")
					)
				)
			)
			.RequestConfiguration(r => r
				.RequestTimeout(TimeSpan.FromMinutes(2))
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override ExecuteWatchRequest Initializer =>
			new ExecuteWatchRequest
			{
				TriggerData = new ScheduleTriggerEvent
				{
					ScheduledTime = _triggeredDateTime,
					TriggeredTime = _triggeredDateTime
				},
				AlternativeInput = new Dictionary<string, object>
				{
					{ "foo", "bar" }
				},
				IgnoreCondition = true,
				Watch = new Watch
				{
					Trigger = new ScheduleContainer
					{
						Cron = "0 0 0 1 * ? 2099"
					},
					Metadata = new Dictionary<string, object>
					{
						{ "foo", "bar" }
					},
					Input = new SearchInput
					{
						Request = new SearchInputRequest
						{
							Indices = new IndexName[] { "logstash" },
							Body = new SearchRequest
							{
								Query = new MatchQuery
								{
									Field = "response",
									Query = "404"
								} && +new DateRangeQuery
								{
									Field = "@timestamp",
									GreaterThanOrEqualTo = "{{ctx.trigger.scheduled_time}}||-5m",
									LessThanOrEqualTo = "{{ctx.trigger.triggered_time}}"
								}
							}
						}
					},
					Condition = new InlineScriptCondition("ctx.payload.hits.total > 1"),
					Actions = new EmailAction("email_admin")
						{
							From = "nest-client@domain.example",
							To = new[] { "someone@domain.host.example" },
							Subject = "404 recently encountered"
						} && new IndexAction("index_action")
						{
							Index = "test",
						} && new LoggingAction("logging_action")
						{
							Text = "404 recently encountered"
						}
						&& new WebhookAction("webhook_action")
						{
							Host = "foo.com",
							Port = 80,
							Path = "/bar",
							Method = HttpInputMethod.Post,
							Body = "{}"
						}
				},
				RequestConfiguration = new RequestConfiguration
				{
					RequestTimeout = TimeSpan.FromMinutes(2)
				}
			};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_watcher/watch/_execute";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
					.Input(i => i
						.Search(s => s
							.Request(r => r
								.Indices("logstash")
								.Body<object>(b => b
									.Query(q => q
											.Match(m => m
												.Field("response")
												.Query("404")
											) && +q
											.DateRange(ffrr => ffrr
												.Field("@timestamp")
												.GreaterThanOrEquals("{{ctx.trigger.scheduled_time}}||-5m")
												.LessThanOrEquals("{{ctx.trigger.triggered_time}}")
											)
									)
								)
							)
						)
					)
					.Condition(c => c
						.Script(ss => ss
							.Source("ctx.payload.hits.total > 1")
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Cron("0 0 0 1 * ? 2099")
						)
					)
					.Metadata(meta => meta.Add("foo", "bar"))
					.Actions(a => a
						.Email("email_admin", e => e
							.To("someone@domain.host.com")
							.Subject("404 recently encountered")
						)
						.Index("index_action", i => i
							.Index("test")
						)
						.Logging("logging_action", l => l
							.Text("404 recently encountered")
						)
						.Webhook("webhook_action", w => w
							.Host("foo.com")
							.Port(80)
							.Path("/bar")
							.Method(HttpInputMethod.Post)
							.Body("{}")
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Execute(f),
			(client, f) => client.Watcher.ExecuteAsync(f),
			(client, r) => client.Watcher.Execute(r),
			(client, r) => client.Watcher.ExecuteAsync(r)
		);

		protected override void ExpectResponse(ExecuteWatchResponse response)
		{
			response.ShouldBeValid();
			response.WatchRecord.TriggerEvent.Should().NotBeNull();
			response.WatchRecord.TriggerEvent.TriggeredTime.Should().Be(_triggeredDateTime);
			response.WatchRecord.TriggerEvent.Manual.Should().NotBeNull();
			response.WatchRecord.TriggerEvent.Manual.Schedule.Should().NotBeNull();
			response.WatchRecord.TriggerEvent.Manual.Schedule.ScheduledTime.Match(
				f => f.Should().Be(_triggeredDateTime),
				s => Assert.True(false, "expected a datetime")
			);

			response.WatchRecord.Result.Input.Type.Should().Be(InputType.Simple);
			response.WatchRecord.Result.Input.Payload.Should().NotBeEmpty();
			response.WatchRecord.Result.Input.Payload["foo"].As<string>().Should().Be("bar");
			response.WatchRecord.Result.Condition.Met.Should().BeTrue();
			response.WatchRecord.Result.Actions.Should().NotBeEmpty();

			var emailAction = response.WatchRecord.Result.Actions.FirstOrDefault(a => a.Id == "email_admin");
			emailAction.Should().NotBeNull();
		}
	}
}
