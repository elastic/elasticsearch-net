using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.MachineLearning.MachineLearningInfo
{
	public class InfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await UrlTester.GET("_ml/info")
					.Fluent(c => c.MachineLearningInfo())
					.Request(c => c.MachineLearningInfo(new MachineLearningInfoRequest()))
					.FluentAsync(c => c.MachineLearningInfoAsync())
					.RequestAsync(c => c.MachineLearningInfoAsync(new MachineLearningInfoRequest()));
		}
	}
}
