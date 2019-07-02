using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateJob
{
	public class UpdateJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/_update")
			.Fluent(c => c.MachineLearning.UpdateJob<object>("job_id", p => p))
			.Request(c => c.MachineLearning.UpdateJob(new UpdateJobRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.UpdateJobAsync<object>("job_id", p => p))
			.RequestAsync(c => c.MachineLearning.UpdateJobAsync(new UpdateJobRequest("job_id")));
	}
}
