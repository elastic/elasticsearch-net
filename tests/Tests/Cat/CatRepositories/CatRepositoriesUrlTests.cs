// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatRepositories
{
	public class CatRepositoriesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/repositories")
					.Fluent(c => c.Cat.Repositories())
					.Request(c => c.Cat.Repositories(new CatRepositoriesRequest()))
					.FluentAsync(c => c.Cat.RepositoriesAsync())
					.RequestAsync(c => c.Cat.RepositoriesAsync(new CatRepositoriesRequest()))
				;

			await GET("/_cat/repositories?v=true")
					.Fluent(c => c.Cat.Repositories(s => s.Verbose()))
					.Request(c => c.Cat.Repositories(new CatRepositoriesRequest() { Verbose = true }))
					.FluentAsync(c => c.Cat.RepositoriesAsync(s => s.Verbose()))
					.RequestAsync(c => c.Cat.RepositoriesAsync(new CatRepositoriesRequest() { Verbose = true }))
				;
		}
	}
}
