using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_ml/anomaly_detectors/job_id")
			.Fluent(c => c.MachineLearning.PutJob<object>("job_id", p => p))
			.Request(c => c.MachineLearning.PutJob(new PutJobRequest("job_id")))
			.FluentAsync(c => c.MachineLearning.PutJobAsync<object>("job_id", p => p))
			.RequestAsync(c => c.MachineLearning.PutJobAsync(new PutJobRequest("job_id")));
	}
}
