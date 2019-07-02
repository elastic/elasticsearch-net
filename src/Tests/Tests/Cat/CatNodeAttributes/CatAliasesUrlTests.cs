using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatNodeAttributes
{
	public class CatNodeAttributesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/nodeattrs")
			.Fluent(c => c.Cat.NodeAttributes())
			.Request(c => c.Cat.NodeAttributes(new CatNodeAttributesRequest()))
			.FluentAsync(c => c.Cat.NodeAttributesAsync())
			.RequestAsync(c => c.Cat.NodeAttributesAsync(new CatNodeAttributesRequest()));
	}
}
