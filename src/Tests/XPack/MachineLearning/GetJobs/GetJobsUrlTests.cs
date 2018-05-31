using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetJobs
{
	public class GetJobsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/ml/anomaly_detectors")
				.Fluent(c => c.GetJobs())
				.Request(c => c.GetJobs(new GetJobsRequest()))
				.FluentAsync(c => c.GetJobsAsync())
				.RequestAsync(c => c.GetJobsAsync(new GetJobsRequest()))
				;

			await GET("/_xpack/ml/anomaly_detectors/job_id")
				.Fluent(c => c.GetJobs(r => r.JobId("job_id")))
				.Request(c => c.GetJobs(new GetJobsRequest("job_id")))
				.FluentAsync(c => c.GetJobsAsync(r => r.JobId("job_id")))
				.RequestAsync(c => c.GetJobsAsync(new GetJobsRequest("job_id")))
				;
		}
	}
}
