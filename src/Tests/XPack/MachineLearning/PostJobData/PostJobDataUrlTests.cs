using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PostJobData
{
	public class PostJobDataUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/_data")
				.Fluent(c => c.PostJobData("job_id", p => p))
				.Request(c => c.PostJobData(new PostJobDataRequest("job_id")))
				.FluentAsync(c => c.PostJobDataAsync("job_id", p => p))
				.RequestAsync(c => c.PostJobDataAsync(new PostJobDataRequest("job_id")))
				;
		}
	}
}
