using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatCount
{
	public class CatCountUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/count")
					.Fluent(c => c.Cat.Count())
					.Request(c => c.Cat.Count(new CatCountRequest()))
					.FluentAsync(c => c.Cat.CountAsync())
					.RequestAsync(c => c.Cat.CountAsync(new CatCountRequest()))
				;

			await GET("/_cat/count/foo")
					.Fluent(c => c.Cat.Count(a => a.Index("foo")))
					.Request(c => c.Cat.Count(new CatCountRequest("foo")))
					.FluentAsync(c => c.Cat.CountAsync(a => a.Index("foo")))
					.RequestAsync(c => c.Cat.CountAsync(new CatCountRequest("foo")))
				;
		}
	}
}
