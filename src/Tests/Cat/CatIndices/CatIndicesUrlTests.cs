using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatIndices
{
	public class CatIndicesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/indices")
				.Fluent(c => c.CatIndices())
				.Request(c => c.CatIndices(new CatIndicesRequest()))
				.FluentAsync(c => c.CatIndicesAsync())
				.RequestAsync(c => c.CatIndicesAsync(new CatIndicesRequest()))
				;

			await GET("/_cat/indices/project")
				.Fluent(c => c.CatIndices(i => i.Index<Project>()))
				.Request(c => c.CatIndices(new CatIndicesRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.CatIndicesAsync(i => i.Index<Project>()))
				.RequestAsync(c => c.CatIndicesAsync(new CatIndicesRequest(Nest.Indices.Index<Project>())))
				;
		}
	}
}
