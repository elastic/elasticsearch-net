using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
