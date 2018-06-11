using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.ValidateDetector
{
	public class ValidateDetectorUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/_validate/detector")
				.Fluent(c => c.ValidateDetector<Project>(v => v))
				.Request(c => c.ValidateDetector(new ValidateDetectorRequest()))
				.FluentAsync(c => c.ValidateDetectorAsync<Project>(v => v))
				.RequestAsync(c => c.ValidateDetectorAsync(new ValidateDetectorRequest()))
				;
		}
	}
}
