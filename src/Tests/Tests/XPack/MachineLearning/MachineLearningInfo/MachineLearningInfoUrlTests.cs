using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.MachineLearning.MachineLearningInfo
{
	public class InfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await UrlTester.GET("_ml/info")
				.Fluent(c => c.MachineLearning.Info())
				.Request(c => c.MachineLearning.Info(new MachineLearningInfoRequest()))
				.FluentAsync(c => c.MachineLearning.InfoAsync())
				.RequestAsync(c => c.MachineLearning.InfoAsync(new MachineLearningInfoRequest()));
	}
}
