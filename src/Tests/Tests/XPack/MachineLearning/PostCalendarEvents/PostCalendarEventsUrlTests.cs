using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutCalendar
{
	public class PostCalendarEventsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_xpack/ml/calendars/calendar_id/events")
			.Fluent(c => c.PostCalendarEvents("calendar_id", p => p))
			.Request(c => c.PostCalendarEvents(new PostCalendarEventsRequest("calendar_id")))
			.FluentAsync(c => c.PostCalendarEventsAsync("calendar_id", p => p))
			.RequestAsync(c => c.PostCalendarEventsAsync(new PostCalendarEventsRequest("calendar_id")));
	}
}
