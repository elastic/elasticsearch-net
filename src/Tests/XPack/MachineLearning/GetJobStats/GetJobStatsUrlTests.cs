using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetJobStats
{
	public class GetJobStatsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/ml/anomaly_detectors/_stats")
				.Fluent(c => c.GetJobStats())
				.Request(c => c.GetJobStats(new GetJobStatsRequest()))
				.FluentAsync(c => c.GetJobStatsAsync())
				.RequestAsync(c => c.GetJobStatsAsync(new GetJobStatsRequest()))
				;

			await GET("/_xpack/ml/anomaly_detectors/job_id/_stats")
				.Fluent(c => c.GetJobStats(r => r.JobId("job_id")))
				.Request(c => c.GetJobStats(new GetJobStatsRequest("job_id")))
				.FluentAsync(c => c.GetJobStatsAsync(r => r.JobId("job_id")))
				.RequestAsync(c => c.GetJobStatsAsync(new GetJobStatsRequest("job_id")))
				;
		}
	}
}
