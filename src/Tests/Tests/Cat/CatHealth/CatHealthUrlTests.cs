using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatHealth
{
	public class CatHealthUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/health")
			.Fluent(c => c.Cat.Health())
			.Request(c => c.Cat.Health(new CatHealthRequest()))
			.FluentAsync(c => c.Cat.HealthAsync())
			.RequestAsync(c => c.Cat.HealthAsync(new CatHealthRequest()));
	}
}
