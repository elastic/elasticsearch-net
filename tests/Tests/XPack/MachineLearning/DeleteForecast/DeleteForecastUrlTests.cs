// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteForecast
{
	public class DeleteForecastUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await DELETE("/_ml/anomaly_detectors/job_id/_forecast/forecast_id")
				.Fluent(c => c.MachineLearning.DeleteForecast("job_id", "forecast_id"))
				.Request(c => c.MachineLearning.DeleteForecast(new DeleteForecastRequest("job_id", "forecast_id")))
				.FluentAsync(c => c.MachineLearning.DeleteForecastAsync("job_id", "forecast_id"))
				.RequestAsync(c => c.MachineLearning.DeleteForecastAsync(new DeleteForecastRequest("job_id", "forecast_id")));

			await DELETE("/_ml/anomaly_detectors/job_id/_forecast/forecast_id%2Cforecast_id2")
				.Fluent(c => c.MachineLearning.DeleteForecast("job_id", "forecast_id,forecast_id2"))
				.Request(c => c.MachineLearning.DeleteForecast(new DeleteForecastRequest("job_id", "forecast_id,forecast_id2")))
				.FluentAsync(c => c.MachineLearning.DeleteForecastAsync("job_id", "forecast_id,forecast_id2"))
				.RequestAsync(c => c.MachineLearning.DeleteForecastAsync(new DeleteForecastRequest("job_id", "forecast_id,forecast_id2")));
		}
	}
}
