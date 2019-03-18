using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteForecast
{
	public class DeleteForecastUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await DELETE("/_xpack/ml/anomaly_detectors/job_id/_forecast/forecast_id")
				.Fluent(c => c.DeleteForecast("job_id", "forecast_id"))
				.Request(c => c.DeleteForecast(new DeleteForecastRequest("job_id", "forecast_id")))
				.FluentAsync(c => c.DeleteForecastAsync("job_id", "forecast_id"))
				.RequestAsync(c => c.DeleteForecastAsync(new DeleteForecastRequest("job_id", "forecast_id")));

			await DELETE("/_xpack/ml/anomaly_detectors/job_id/_forecast/forecast_id%2Cforecast_id2")
				.Fluent(c => c.DeleteForecast("job_id", "forecast_id,forecast_id2"))
				.Request(c => c.DeleteForecast(new DeleteForecastRequest("job_id", "forecast_id,forecast_id2")))
				.FluentAsync(c => c.DeleteForecastAsync("job_id", "forecast_id,forecast_id2"))
				.RequestAsync(c => c.DeleteForecastAsync(new DeleteForecastRequest("job_id", "forecast_id,forecast_id2")));
		}
	}
}
