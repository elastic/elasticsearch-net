// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

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
