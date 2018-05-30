using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateJob
{
	public class UpdateJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/_update")
				.Fluent(c => c.UpdateJob<object>("job_id", p => p))
				.Request(c => c.UpdateJob(new UpdateJobRequest("job_id")))
				.FluentAsync(c => c.UpdateJobAsync<object>("job_id", p => p))
				.RequestAsync(c => c.UpdateJobAsync(new UpdateJobRequest("job_id")))
				;
		}
	}
}
