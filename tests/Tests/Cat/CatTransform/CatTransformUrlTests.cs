using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatTransform
{
	public class CatTransformUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_cat/transforms")
				.Fluent(c => c.Cat.Transform())
				.Request(c => c.Cat.Transform(new CatTransformRequest()))
				.FluentAsync(c => c.Cat.TransformAsync())
				.RequestAsync(c => c.Cat.TransformAsync(new CatTransformRequest()));
	}
}
