using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatHelp
{
	public class CatHelpUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat")
			.Fluent(c => c.Cat.Help())
			.Request(c => c.Cat.Help(new CatHelpRequest()))
			.FluentAsync(c => c.Cat.HelpAsync())
			.RequestAsync(c => c.Cat.HelpAsync(new CatHelpRequest()));
	}
}
