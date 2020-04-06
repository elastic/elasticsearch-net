using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatTransforms
{
	public class CatTransformsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_cat/transforms")
				.Fluent(c => c.Cat.Transforms())
				.Request(c => c.Cat.Transforms(new CatTransformsRequest()))
				.FluentAsync(c => c.Cat.TransformsAsync())
				.RequestAsync(c => c.Cat.TransformsAsync(new CatTransformsRequest()));
	}
}
