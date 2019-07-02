using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.OpenJob
{
	public class OpenJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/_open")
			.Fluent(c => c.MachineLearning.OpenJob("job_id"))
			.Request(c => c.MachineLearning.OpenJob(new OpenJobRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.OpenJobAsync("job_id"))
			.RequestAsync(c => c.MachineLearning.OpenJobAsync(new OpenJobRequest("job_id")));
	}
}
