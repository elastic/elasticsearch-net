using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetJobStats
{
	public class GetJobStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/anomaly_detectors/_stats")
					.Fluent(c => c.MachineLearning.GetJobStats())
					.Request(c => c.MachineLearning.GetJobStats(new GetJobStatsRequest()))
					.FluentAsync(c => c.MachineLearning.GetJobStatsAsync())
					.RequestAsync(c => c.MachineLearning.GetJobStatsAsync(new GetJobStatsRequest()))
				;

			await GET("/_ml/anomaly_detectors/job_id/_stats")
					.Fluent(c => c.MachineLearning.GetJobStats(r => r.JobId("job_id")))
					.Request(c => c.MachineLearning.GetJobStats(new GetJobStatsRequest("job_id")))
					.FluentAsync(c => c.MachineLearning.GetJobStatsAsync(r => r.JobId("job_id")))
					.RequestAsync(c => c.MachineLearning.GetJobStatsAsync(new GetJobStatsRequest("job_id")))
				;
		}
	}
}
