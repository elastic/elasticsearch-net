/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
