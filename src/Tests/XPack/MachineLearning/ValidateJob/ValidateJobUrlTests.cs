using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.ValidateJob
{
	public class ValidateJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/_validate")
				.Fluent(c => c.ValidateJob<Project>(v => v))
				.Request(c => c.ValidateJob(new ValidateJobRequest()))
				.FluentAsync(c => c.ValidateJobAsync<Project>(v => v))
				.RequestAsync(c => c.ValidateJobAsync(new ValidateJobRequest()))
				;
		}
	}
}
