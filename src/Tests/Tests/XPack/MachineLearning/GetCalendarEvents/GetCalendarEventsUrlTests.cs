using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetCalendarEvents
{
	public class GetCalendarEventsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/calendars/1/events")
				.Request(c => c.GetCalendarEvents(new GetCalendarEventsRequest(1)))
				.Fluent(c => c.GetCalendarEvents(1, r => r))
				.FluentAsync(c => c.GetCalendarEventsAsync(1, r => r))
				.RequestAsync(c => c.GetCalendarEventsAsync(new GetCalendarEventsRequest(1)));
		}
	}
}
