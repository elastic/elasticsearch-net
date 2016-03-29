using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatHealth
{
	public class CatHealthUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/health")
				.Fluent(c => c.CatHealth())
				.Request(c => c.CatHealth(new CatHealthRequest()))
				.FluentAsync(c => c.CatHealthAsync())
				.RequestAsync(c => c.CatHealthAsync(new CatHealthRequest()))
				;
		}
	}
}
