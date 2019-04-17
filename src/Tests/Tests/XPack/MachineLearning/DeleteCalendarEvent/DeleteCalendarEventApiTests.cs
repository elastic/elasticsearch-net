using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteCalendarEvent
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class DeleteCalendarEventApiTests : MachineLearningIntegrationTestBase<DeleteCalendarEventResponse, IDeleteCalendarEventRequest, DeleteCalendarEventDescriptor, DeleteCalendarEventRequest>
	{
		public DeleteCalendarEventApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutCalendar(client, callUniqueValue.Value);
				PostCalendarEvent(client, callUniqueValue.Value);
			}
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var events = client.GetCalendarEvents(CallIsolatedValue, f => f);
			var eventId = events.Events.First().EventId;
			ExtendedValue("eventId", eventId);
		}

		private Id EventId => TryGetExtendedValue<Id>("eventId", out var eventId) ? eventId : "event_id";

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteCalendarEventRequest Initializer => new DeleteCalendarEventRequest(CallIsolatedValue, EventId);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}/events/{EventId}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteCalendarEvent(CallIsolatedValue, EventId, f),
			(client, f) => client.DeleteCalendarEventAsync(CallIsolatedValue, EventId, f),
			(client, r) => client.DeleteCalendarEvent(r),
			(client, r) => client.DeleteCalendarEventAsync(r)
		);

		protected override DeleteCalendarEventDescriptor NewDescriptor() => new DeleteCalendarEventDescriptor(CallIsolatedValue, EventId);

		protected override void ExpectResponse(DeleteCalendarEventResponse response)
		{
			response.ShouldBeValid();
		}
	}
}
