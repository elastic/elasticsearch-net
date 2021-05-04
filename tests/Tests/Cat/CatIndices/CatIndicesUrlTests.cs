// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatIndices
{
	public class CatIndicesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/indices")
					.Fluent(c => c.Cat.Indices())
					.Request(c => c.Cat.Indices(new CatIndicesRequest()))
					.FluentAsync(c => c.Cat.IndicesAsync())
					.RequestAsync(c => c.Cat.IndicesAsync(new CatIndicesRequest()))
				;

			await GET("/_cat/indices/project")
					.Fluent(c => c.Cat.Indices(i => i.Index<Project>()))
					.Request(c => c.Cat.Indices(new CatIndicesRequest(Nest.Indices.Index<Project>())))
					.FluentAsync(c => c.Cat.IndicesAsync(i => i.Index<Project>()))
					.RequestAsync(c => c.Cat.IndicesAsync(new CatIndicesRequest(Nest.Indices.Index<Project>())))
				;
		}
	}
}
