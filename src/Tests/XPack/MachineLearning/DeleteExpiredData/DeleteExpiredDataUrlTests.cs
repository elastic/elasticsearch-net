using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteExpiredData
{
	public class DeleteExpiredDataUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_xpack/ml/_delete_expired_data")
				.Fluent(c => c.DeleteExpiredData())
				.Request(c => c.DeleteExpiredData(new DeleteExpiredDataRequest()))
				.FluentAsync(c => c.DeleteExpiredDataAsync())
				.RequestAsync(c => c.DeleteExpiredDataAsync(new DeleteExpiredDataRequest()))
				;
		}
	}
}
