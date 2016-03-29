using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatSegments
{
	public class CatSegmentsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/segments")
				.Fluent(c => c.CatSegments())
				.Request(c => c.CatSegments(new CatSegmentsRequest()))
				.FluentAsync(c => c.CatSegmentsAsync())
				.RequestAsync(c => c.CatSegmentsAsync(new CatSegmentsRequest()))
				;

			await GET("/_cat/segments/project")
				.Fluent(c => c.CatSegments(r => r.Index<Project>()))
				.Request(c => c.CatSegments(new CatSegmentsRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.CatSegmentsAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.CatSegmentsAsync(new CatSegmentsRequest(Nest.Indices.Index<Project>())));
		}
	}
}
