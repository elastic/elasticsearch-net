using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.SetUpgradeMode
{
	public class SetUpgradeModeUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/set_upgrade_mode")
			.Fluent(c => c.MachineLearning.SetUpgradeMode())
			.Request(c => c.MachineLearning.SetUpgradeMode(new SetUpgradeModeRequest()))
			.FluentAsync(c => c.MachineLearning.SetUpgradeModeAsync())
			.RequestAsync(c => c.MachineLearning.SetUpgradeModeAsync(new SetUpgradeModeRequest()));
	}
}
