using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetOverallBuckets
{
	public class GetOverallBucketsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/job_id/results/overall_buckets")
			.Fluent(c => c.MachineLearning.GetOverallBuckets("job_id"))
			.Request(c => c.MachineLearning.GetOverallBuckets(new GetOverallBucketsRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.GetOverallBucketsAsync("job_id"))
			.RequestAsync(c => c.MachineLearning.GetOverallBucketsAsync(new GetOverallBucketsRequest("job_id")));
	}
}
