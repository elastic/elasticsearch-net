using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.ValidateJob
{
	public class ValidateJobUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/_validate")
			.Fluent(c => c.MachineLearning.ValidateJob<Project>(v => v))
			.Request(c => c.MachineLearning.ValidateJob(new ValidateJobRequest()))
			.FluentAsync(c => c.MachineLearning.ValidateJobAsync<Project>(v => v))
			.RequestAsync(c => c.MachineLearning.ValidateJobAsync(new ValidateJobRequest()));
	}
}
