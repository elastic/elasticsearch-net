using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetCalendarEvents
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class GetCalendarEventsApiTests : MachineLearningIntegrationTestBase<IGetCalendarEventsResponse, IGetCalendarEventsRequest, GetCalendarEventsDescriptor, GetCalendarEventsRequest>
	{
		public GetCalendarEventsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutCalendar(client, callUniqueValue.Value);
				PostCalendarEvents(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetCalendarEventsRequest Initializer => new GetCalendarEventsRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_xpack/ml/calendars/{CallIsolatedValue}/events";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetCalendarEvents(CallIsolatedValue),
			(client, f) => client.GetCalendarEventsAsync(CallIsolatedValue, f),
			(client, r) => client.GetCalendarEvents(r),
			(client, r) => client.GetCalendarEventsAsync(r)
		);

		protected override GetCalendarEventsDescriptor NewDescriptor() => new GetCalendarEventsDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IGetCalendarEventsResponse response)
		{
			response.ShouldBeValid();

			response.Count.Should().Be(10);

			response.Events.Should().NotBeNull();

			response.Events.Count().Should().Be(10);

			var @event = response.Events.First();

			@event.Description.Should().Be("Event 0");
			@event.CalendarId.Should().Be(CallIsolatedValue);
			@event.EventId.Should().NotBeNull();
		}
	}
}
