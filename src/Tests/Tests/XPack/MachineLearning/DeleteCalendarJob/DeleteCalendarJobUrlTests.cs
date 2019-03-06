using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutCalendarJob
{
	public class DeleteCalendarJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/calendars/calendar_id/jobs/job_id")
			.Fluent(c => c.DeleteCalendarJob("calendar_id", "job_id", p => p))
			.Request(c => c.DeleteCalendarJob(new DeleteCalendarJobRequest("calendar_id", "job_id")))
			.FluentAsync(c => c.DeleteCalendarJobAsync("calendar_id", "job_id", p => p))
			.RequestAsync(c => c.DeleteCalendarJobAsync(new DeleteCalendarJobRequest("calendar_id", "job_id")));
	}
}
