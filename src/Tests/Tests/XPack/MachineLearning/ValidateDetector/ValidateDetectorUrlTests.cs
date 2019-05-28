using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.ValidateDetector
{
	public class ValidateDetectorUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/anomaly_detectors/_validate/detector")
			.Fluent(c => c.MachineLearning.ValidateDetector<Project>(v => v))
			.Request(c => c.MachineLearning.ValidateDetector(new ValidateDetectorRequest()))
			.FluentAsync(c => c.MachineLearning.ValidateDetectorAsync<Project>(v => v))
			.RequestAsync(c => c.MachineLearning.ValidateDetectorAsync(new ValidateDetectorRequest()));
	}
}
