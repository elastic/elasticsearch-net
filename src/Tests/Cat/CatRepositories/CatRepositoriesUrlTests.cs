using System.Threading.Tasks;
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
				.Fluent(c => c.CatRepositories(s=>s.V()))
				.Request(c => c.CatRepositories(new CatRepositoriesRequest() { V = true }))
				.FluentAsync(c => c.CatRepositoriesAsync(s=>s.V()))
				.RequestAsync(c => c.CatRepositoriesAsync(new CatRepositoriesRequest() { V = true }))
				;
		}
	}
}
