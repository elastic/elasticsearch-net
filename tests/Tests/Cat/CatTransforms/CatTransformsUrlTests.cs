using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatTransforms
{
	public class CatTransformsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/transforms")
				.Fluent(c => c.Cat.Transforms())
				.Request(c => c.Cat.Transforms(new CatTransformsRequest()))
				.FluentAsync(c => c.Cat.TransformsAsync())
				.RequestAsync(c => c.Cat.TransformsAsync(new CatTransformsRequest()));

			await GET("/_cat/transforms/transform-id")
				.Fluent(c => c.Cat.Transforms(f => f.TransformId("transform-id")))
				.Request(c => c.Cat.Transforms(new CatTransformsRequest("transform-id")))
				.FluentAsync(c => c.Cat.TransformsAsync(f => f.TransformId("transform-id")))
				.RequestAsync(c => c.Cat.TransformsAsync(new CatTransformsRequest("transform-id")));
		}
	}
}
