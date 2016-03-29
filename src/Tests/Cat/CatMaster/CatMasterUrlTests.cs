using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatMaster
{
	public class CatMasterUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/master")
				.Fluent(c => c.CatMaster())
				.Request(c => c.CatMaster(new CatMasterRequest()))
				.FluentAsync(c => c.CatMasterAsync())
				.RequestAsync(c => c.CatMasterAsync(new CatMasterRequest()))
				;
		}
	}
}
