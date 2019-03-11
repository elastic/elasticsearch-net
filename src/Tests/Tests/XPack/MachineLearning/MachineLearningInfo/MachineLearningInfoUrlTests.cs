using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class InfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("_xpack/ml/info")
					.Fluent(c => c.MachineLearningInfo())
					.Request(c => c.MachineLearningInfo(new MachineLearningInfoRequest()))
					.FluentAsync(c => c.MachineLearningInfoAsync())
					.RequestAsync(c => c.MachineLearningInfoAsync(new MachineLearningInfoRequest()));
		}
	}
}
