using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetOverallBuckets
{
	public class GetOverallBucketsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/results/overall_buckets")
				.Fluent(c => c.GetOverallBuckets("job_id"))
				.Request(c => c.GetOverallBuckets(new GetOverallBucketsRequest("job_id")))
				.FluentAsync(c => c.GetOverallBucketsAsync("job_id"))
				.RequestAsync(c => c.GetOverallBucketsAsync(new GetOverallBucketsRequest("job_id")))
				;
		}
	}
}
