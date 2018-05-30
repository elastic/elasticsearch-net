using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.ForecastJob
{
	public class ForecastJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/_forecast")
				.Fluent(c => c.ForecastJob("job_id"))
				.Request(c => c.ForecastJob(new ForecastJobRequest("job_id")))
				.FluentAsync(c => c.ForecastJobAsync("job_id"))
				.RequestAsync(c => c.ForecastJobAsync(new ForecastJobRequest("job_id")))
				;
		}
	}
}
