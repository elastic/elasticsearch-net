using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.DeleteCalendarEvent
{
	public class DeleteCalendarEventUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_ml/calendars/calendar_id/events/event_id")
			.Fluent(c => c.MachineLearning.DeleteCalendarEvent("calendar_id", "event_id", p => p))
			.Request(c => c.MachineLearning.DeleteCalendarEvent(new DeleteCalendarEventRequest("calendar_id","event_id")))
			.FluentAsync(c => c.MachineLearning.DeleteCalendarEventAsync("calendar_id", "event_id",p => p))
			.RequestAsync(c => c.MachineLearning.DeleteCalendarEventAsync(new DeleteCalendarEventRequest("calendar_id", "event_id")));
	}
}
