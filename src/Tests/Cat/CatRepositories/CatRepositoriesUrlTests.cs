using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatRepositories
{
	public class CatRepositoriesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/repositories")
				.Fluent(c => c.CatRepositories())
				.Request(c => c.CatRepositories(new CatRepositoriesRequest()))
				.FluentAsync(c => c.CatRepositoriesAsync())
				.RequestAsync(c => c.CatRepositoriesAsync(new CatRepositoriesRequest()))
				;

			await GET("/_cat/repositories?v=true")
				.Fluent(c => c.CatRepositories(s=>s.Verbose()))
				.Request(c => c.CatRepositories(new CatRepositoriesRequest() { Verbose = true }))
				.FluentAsync(c => c.CatRepositoriesAsync(s=>s.Verbose()))
				.RequestAsync(c => c.CatRepositoriesAsync(new CatRepositoriesRequest() { Verbose = true }))
				;
		}
	}
}
