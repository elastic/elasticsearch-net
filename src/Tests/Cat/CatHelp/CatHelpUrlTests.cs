using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatHelp
{
	public class CatHelpUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat")
				.Fluent(c => c.CatHelp())
				.Request(c => c.CatHelp(new CatHelpRequest()))
				.FluentAsync(c => c.CatHelpAsync())
				.RequestAsync(c => c.CatHelpAsync(new CatHelpRequest()))
				;

		}
	}
}
