using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetCalendars
{
	public class GetCalendarsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_xpack/ml/calendars")
				.Fluent(c => c.GetCalendars(p => p))
				.Request(c => c.GetCalendars(new GetCalendarsRequest()))
				.FluentAsync(c => c.GetCalendarsAsync(p => p))
				.RequestAsync(c => c.GetCalendarsAsync(new GetCalendarsRequest()));

			await POST("/_xpack/ml/calendars/1")
				.Request(c => c.GetCalendars(new GetCalendarsRequest(1)))
				.Fluent(c => c.GetCalendars(r => r.CalendarId(1)))
				.FluentAsync(c => c.GetCalendarsAsync(r => r.CalendarId(1)))
				.RequestAsync(c => c.GetCalendarsAsync(new GetCalendarsRequest(1)));
		}
	}
}
