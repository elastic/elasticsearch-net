using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatAliases
{
	public class CatAliasesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/aliases")
				.Fluent(c => c.CatAliases())
				.Request(c => c.CatAliases(new CatAliasesRequest()))
				.FluentAsync(c => c.CatAliasesAsync())
				.RequestAsync(c => c.CatAliasesAsync(new CatAliasesRequest()))
				;

			await GET("/_cat/aliases/foo")
				.Fluent(c => c.CatAliases(a => a.Name("foo")))
				.Request(c => c.CatAliases(new CatAliasesRequest("foo")))
				.FluentAsync(c => c.CatAliasesAsync(a => a.Name("foo")))
				.RequestAsync(c => c.CatAliasesAsync(new CatAliasesRequest("foo")))
				;
		}
	}
}
