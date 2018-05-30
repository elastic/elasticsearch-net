using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.FlushJob
{
	public class FlushJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/_flush")
				.Fluent(c => c.FlushJob("job_id"))
				.Request(c => c.FlushJob(new FlushJobRequest("job_id")))
				.FluentAsync(c => c.FlushJobAsync("job_id"))
				.RequestAsync(c => c.FlushJobAsync(new FlushJobRequest("job_id")))
				;
		}
	}
}
