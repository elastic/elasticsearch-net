// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class GetCategoriesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("_ml/anomaly_detectors/job_id/results/categories/")
					.Fluent(c => c.MachineLearning.GetCategories("job_id"))
					.Request(c => c.MachineLearning.GetCategories(new GetCategoriesRequest("job_id")))
					.FluentAsync(c => c.MachineLearning.GetCategoriesAsync("job_id"))
					.RequestAsync(c => c.MachineLearning.GetCategoriesAsync(new GetCategoriesRequest("job_id")))
				;

			await POST("_ml/anomaly_detectors/job_id/results/categories/1")
					.Request(c => c.MachineLearning.GetCategories(new GetCategoriesRequest("job_id", 1)))
					.Fluent(c => c.MachineLearning.GetCategories("job_id", r => r.CategoryId(1)))
					.FluentAsync(c => c.MachineLearning.GetCategoriesAsync("job_id", r => r.CategoryId(1)))
					.RequestAsync(c => c.MachineLearning.GetCategoriesAsync(new GetCategoriesRequest("job_id", 1)))
				;
		}
	}
}
