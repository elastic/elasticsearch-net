using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatCount
{
	public class CatCountUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/count")
				.Fluent(c => c.CatCount())
				.Request(c => c.CatCount(new CatCountRequest()))
				.FluentAsync(c => c.CatCountAsync())
				.RequestAsync(c => c.CatCountAsync(new CatCountRequest()))
				;

			await GET("/_cat/count/foo")
				.Fluent(c => c.CatCount(a => a.Index("foo")))
				.Request(c => c.CatCount(new CatCountRequest("foo")))
				.FluentAsync(c => c.CatCountAsync(a => a.Index("foo")))
				.RequestAsync(c => c.CatCountAsync(new CatCountRequest("foo")))
				;
		}
	}
}
