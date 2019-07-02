using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.CloseJob
{
	public class CloseJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/_close")
			.Fluent(c => c.MachineLearning.CloseJob("job_id"))
			.Request(c => c.MachineLearning.CloseJob(new CloseJobRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.CloseJobAsync("job_id"))
			.RequestAsync(c => c.MachineLearning.CloseJobAsync(new CloseJobRequest("job_id")));
	}
}
