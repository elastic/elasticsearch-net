using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatJobs
{
	public class CatJobsUrlTests : UrlTestsBase
	{
		[U]
		public override async Task Urls()
		{
			await GET("/_cat/ml/anomaly_detectors")
				.Fluent(c => c.Cat.Jobs())
				.Request(c => c.Cat.Jobs(new CatJobsRequest()))
				.FluentAsync(c => c.Cat.JobsAsync())
				.RequestAsync(c => c.Cat.JobsAsync(new CatJobsRequest()));

			await GET("/_cat/ml/anomaly_detectors/job-id")
				.Fluent(c => c.Cat.Jobs(f => f.JobId("job-id")))
				.Request(c => c.Cat.Jobs(new CatJobsRequest("job-id")))
				.FluentAsync(c => c.Cat.JobsAsync(f => f.JobId("job-id")))
				.RequestAsync(c => c.Cat.JobsAsync(new CatJobsRequest("job-id")));
		}
	}
}
