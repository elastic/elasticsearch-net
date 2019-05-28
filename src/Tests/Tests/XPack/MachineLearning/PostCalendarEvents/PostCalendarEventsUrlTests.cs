using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutCalendar
{
	public class PostCalendarEventsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/calendars/calendar_id/events")
			.Fluent(c => c.MachineLearning.PostCalendarEvents("calendar_id", p => p))
			.Request(c => c.MachineLearning.PostCalendarEvents(new PostCalendarEventsRequest("calendar_id")))
			.FluentAsync(c => c.MachineLearning.PostCalendarEventsAsync("calendar_id", p => p))
			.RequestAsync(c => c.MachineLearning.PostCalendarEventsAsync(new PostCalendarEventsRequest("calendar_id")));
	}
}
