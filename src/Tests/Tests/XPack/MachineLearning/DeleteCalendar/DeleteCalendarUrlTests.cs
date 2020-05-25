using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteCalendar
{
	public class DeleteCalendarUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_xpack/ml/calendars/calendar_id")
			.Fluent(c => c.DeleteCalendar("calendar_id", p => p))
			.Request(c => c.DeleteCalendar(new DeleteCalendarRequest("calendar_id")))
			.FluentAsync(c => c.DeleteCalendarAsync("calendar_id", p => p))
			.RequestAsync(c => c.DeleteCalendarAsync(new DeleteCalendarRequest("calendar_id")));
	}
}
