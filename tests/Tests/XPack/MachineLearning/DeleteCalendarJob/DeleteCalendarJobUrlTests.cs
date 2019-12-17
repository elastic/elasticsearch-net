using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.DeleteCalendarJob
{
	public class DeleteCalendarJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_ml/calendars/calendar_id/jobs/job_id")
			.Fluent(c => c.MachineLearning.DeleteCalendarJob("calendar_id", "job_id", p => p))
			.Request(c => c.MachineLearning.DeleteCalendarJob(new DeleteCalendarJobRequest("calendar_id", "job_id")))
			.FluentAsync(c => c.MachineLearning.DeleteCalendarJobAsync("calendar_id", "job_id", p => p))
			.RequestAsync(c => c.MachineLearning.DeleteCalendarJobAsync(new DeleteCalendarJobRequest("calendar_id", "job_id")));
	}
}
