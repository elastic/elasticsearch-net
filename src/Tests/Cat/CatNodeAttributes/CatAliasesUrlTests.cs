using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatNodeAttributes
{
	public class CatNodeAttributesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/nodeattrs")
				.Fluent(c => c.CatNodeAttributes())
				.Request(c => c.CatNodeAttributes(new CatNodeAttributesRequest()))
				.FluentAsync(c => c.CatNodeAttributesAsync())
				.RequestAsync(c => c.CatNodeAttributesAsync(new CatNodeAttributesRequest()))
				;
		}
	}
}
