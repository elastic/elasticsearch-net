using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteCalendar
{
	public class DeleteCalendarEventUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/calendars/calendar_id/events/event_id")
			.Fluent(c => c.MachineLearning.DeleteCalendarEvent("calendar_id", "event_id", p => p))
			.Request(c => c.MachineLearning.DeleteCalendarEvent(new DeleteCalendarEventRequest("calendar_id","event_id")))
			.FluentAsync(c => c.MachineLearning.DeleteCalendarEventAsync("calendar_id", "event_id",p => p))
			.RequestAsync(c => c.MachineLearning.DeleteCalendarEventAsync(new DeleteCalendarEventRequest("calendar_id", "event_id")));
	}
}
