// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.PostCalendarEvents
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class PostCalendarEventsApiTests
		: MachineLearningIntegrationTestBase<PostCalendarEventsResponse, IPostCalendarEventsRequest, PostCalendarEventsDescriptor,
			PostCalendarEventsRequest>
	{
		public PostCalendarEventsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutCalendar(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			events = GetScheduledJsonEvents()
		};

		protected override int ExpectStatusCode => 200;

		private static readonly int StartDate = DateTime.Now.Year;

		private IEnumerable<object> GetScheduledJsonEvents()
		{
			for (var i = 0; i < 10; i++)
				yield return new
				{
					start_time = new DateTimeOffset(StartDate + i, 1, 1, 0, 0, 0, TimeSpan.Zero)
						.ToUnixTimeMilliseconds()
						.ToString(),
					end_time = new DateTimeOffset(StartDate + 1 + i, 1, 1, 0, 0, 0, TimeSpan.Zero)
						.ToUnixTimeMilliseconds()
						.ToString(),
					description = $"Event {i}",
					calendar_id = CallIsolatedValue
				};
		}

		private IEnumerable<ScheduledEvent> GetScheduledEvents()
		{
			for (var i = 0; i < 10; i++)
				yield return new ScheduledEvent
				{
					StartTime = new DateTimeOffset(StartDate + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
					EndTime = new DateTimeOffset(StartDate + 1 + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
					Description = $"Event {i}",
					CalendarId = CallIsolatedValue
				};
		}

		protected override Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> Fluent => f => f.Events(GetScheduledEvents());

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override PostCalendarEventsRequest Initializer => new PostCalendarEventsRequest(CallIsolatedValue)
		{
			Events = GetScheduledEvents()
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}/events";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PostCalendarEvents(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PostCalendarEventsAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PostCalendarEvents(r),
			(client, r) => client.MachineLearning.PostCalendarEventsAsync(r)
		);

		protected override PostCalendarEventsDescriptor NewDescriptor() =>
			new PostCalendarEventsDescriptor(CallIsolatedValue).Events(GetScheduledEvents());

		protected override void ExpectResponse(PostCalendarEventsResponse response)
		{
			response.ShouldBeValid();

			response.Events.Should().NotBeNull();
			response.Events.Count().Should().Be(10);

			var @event = response.Events.First();
			@event.CalendarId.Should().Be(CallIsolatedValue);
			@event.Description.Should().Be($"Event 0");
		}
	}
}
