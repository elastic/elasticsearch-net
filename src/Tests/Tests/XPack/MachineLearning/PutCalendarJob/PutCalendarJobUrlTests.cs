using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutCalendarJob
{
	public class PutCalendarJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_xpack/ml/calendars/calendar_id/jobs/job_id")
			.Fluent(c => c.PutCalendarJob("calendar_id", "job_id", p => p))
			.Request(c => c.PutCalendarJob(new PutCalendarJobRequest("calendar_id", "job_id")))
			.FluentAsync(c => c.PutCalendarJobAsync("calendar_id", "job_id", p => p))
			.RequestAsync(c => c.PutCalendarJobAsync(new PutCalendarJobRequest("calendar_id", "job_id")));
	}
}
