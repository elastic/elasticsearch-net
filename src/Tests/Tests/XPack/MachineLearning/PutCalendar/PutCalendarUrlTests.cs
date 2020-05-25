using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutCalendar
{
	public class PutCalendarUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_xpack/ml/calendars/calendar_id")
			.Fluent(c => c.PutCalendar("calendar_id", p => p))
			.Request(c => c.PutCalendar(new PutCalendarRequest("calendar_id")))
			.FluentAsync(c => c.PutCalendarAsync("calendar_id", p => p))
			.RequestAsync(c => c.PutCalendarAsync(new PutCalendarRequest("calendar_id")));
	}
}
