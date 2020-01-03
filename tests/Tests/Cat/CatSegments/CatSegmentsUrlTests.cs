using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatSegments
{
	public class CatSegmentsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/segments")
					.Fluent(c => c.Cat.Segments())
					.Request(c => c.Cat.Segments(new CatSegmentsRequest()))
					.FluentAsync(c => c.Cat.SegmentsAsync())
					.RequestAsync(c => c.Cat.SegmentsAsync(new CatSegmentsRequest()))
				;

			await GET("/_cat/segments/project")
				.Fluent(c => c.Cat.Segments(r => r.Index<Project>()))
				.Request(c => c.Cat.Segments(new CatSegmentsRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.Cat.SegmentsAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.Cat.SegmentsAsync(new CatSegmentsRequest(Nest.Indices.Index<Project>())));
		}
	}
}
