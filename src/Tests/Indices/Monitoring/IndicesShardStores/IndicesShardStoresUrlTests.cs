using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.Monitoring.IndicesShardStores
{
	public class IndicesShardStoresUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_shard_stores")
				.Fluent(c => c.IndicesShardStores())
				.Request(c => c.IndicesShardStores(new IndicesShardStoresRequest()))
				.FluentAsync(c => c.IndicesShardStoresAsync())
				.RequestAsync(c => c.IndicesShardStoresAsync(new IndicesShardStoresRequest()))
				;

			var index = "index1,index2";
			await GET($"/index1%2Cindex2/_shard_stores")
				.Fluent(c => c.IndicesShardStores(s=>s.Index(index)))
				.Request(c => c.IndicesShardStores(new IndicesShardStoresRequest(index)))
				.FluentAsync(c => c.IndicesShardStoresAsync(s=>s.Index(index)))
				.RequestAsync(c => c.IndicesShardStoresAsync(new IndicesShardStoresRequest(index)))
				;

		}
	}
}
