using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteJob
{
	public class DeleteJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_xpack/ml/anomaly_detectors/job_id")
				.Fluent(c => c.DeleteJob("job_id"))
				.Request(c => c.DeleteJob(new DeleteJobRequest("job_id")))
				.FluentAsync(c => c.DeleteJobAsync("job_id"))
				.RequestAsync(c => c.DeleteJobAsync(new DeleteJobRequest("job_id")))
				;
		}
	}
}
