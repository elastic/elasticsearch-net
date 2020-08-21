// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.Monitoring.IndicesShardStores
{
	public class IndicesShardStoresUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_shard_stores")
					.Fluent(c => c.Indices.ShardStores())
					.Request(c => c.Indices.ShardStores(new IndicesShardStoresRequest()))
					.FluentAsync(c => c.Indices.ShardStoresAsync())
					.RequestAsync(c => c.Indices.ShardStoresAsync(new IndicesShardStoresRequest()))
				;

			var index = "index1,index2";
			await GET("/index1%2Cindex2/_shard_stores")
					.Fluent(c => c.Indices.ShardStores(index))
					.Request(c => c.Indices.ShardStores(new IndicesShardStoresRequest(index)))
					.FluentAsync(c => c.Indices.ShardStoresAsync(index))
					.RequestAsync(c => c.Indices.ShardStoresAsync(new IndicesShardStoresRequest(index)))
				;
		}
	}
}
