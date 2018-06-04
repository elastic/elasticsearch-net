using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class GetCategoriesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("_xpack/ml/anomaly_detectors/job_id/results/categories/")
				.Fluent(c => c.GetCategories("job_id"))
				.Request(c => c.GetCategories(new GetCategoriesRequest("job_id")))
				.FluentAsync(c => c.GetCategoriesAsync("job_id"))
				.RequestAsync(c => c.GetCategoriesAsync(new GetCategoriesRequest("job_id")))
				;

			await POST("_xpack/ml/anomaly_detectors/job_id/results/categories/1")
				.Request(c => c.GetCategories(new GetCategoriesRequest("job_id", 1)))
				.Fluent(c => c.GetCategories("job_id", r => r.CategoryId(1)))
				.FluentAsync(c => c.GetCategoriesAsync("job_id", r => r.CategoryId(1)))
				.RequestAsync(c => c.GetCategoriesAsync(new GetCategoriesRequest("job_id", 1)))
				;
		}
	}
}
