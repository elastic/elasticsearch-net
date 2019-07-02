using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteExpiredData
{
	public class DeleteExpiredDataUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/_delete_expired_data")
			.Fluent(c => c.MachineLearning.DeleteExpiredData())
			.Request(c => c.MachineLearning.DeleteExpiredData(new DeleteExpiredDataRequest()))
			.FluentAsync(c => c.MachineLearning.DeleteExpiredDataAsync())
			.RequestAsync(c => c.MachineLearning.DeleteExpiredDataAsync(new DeleteExpiredDataRequest()));
	}
}
