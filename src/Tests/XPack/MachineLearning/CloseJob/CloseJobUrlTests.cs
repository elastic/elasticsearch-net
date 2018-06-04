using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.CloseJob
{
	public class CloseJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/_close")
				.Fluent(c => c.CloseJob("job_id"))
				.Request(c => c.CloseJob(new CloseJobRequest("job_id")))
				.FluentAsync(c => c.CloseJobAsync("job_id"))
				.RequestAsync(c => c.CloseJobAsync(new CloseJobRequest("job_id")))
				;
		}
	}
}
