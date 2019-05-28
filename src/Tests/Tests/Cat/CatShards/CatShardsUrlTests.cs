using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatShards
{
	public class CatShardsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/shards")
					.Fluent(c => c.Cat.Shards())
					.Request(c => c.Cat.Shards(new CatShardsRequest()))
					.FluentAsync(c => c.Cat.ShardsAsync())
					.RequestAsync(c => c.Cat.ShardsAsync(new CatShardsRequest()))
				;

			await GET("/_cat/shards/project")
				.Fluent(c => c.Cat.Shards(r => r.Index<Project>()))
				.Request(c => c.Cat.Shards(new CatShardsRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.Cat.ShardsAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.Cat.ShardsAsync(new CatShardsRequest(Nest.Indices.Index<Project>())));
		}
	}
}
