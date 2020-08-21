// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.ReloadSearchAnalyzers
{
	public class ReloadSearchAnalyzersUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_all/_reload_search_analyzers")
					.Fluent(c => c.Indices.ReloadSearchAnalyzers(All))
					.Request(c => c.Indices.ReloadSearchAnalyzers(new ReloadSearchAnalyzersRequest(All)))
					.FluentAsync(c => c.Indices.ReloadSearchAnalyzersAsync(All))
					.RequestAsync(c => c.Indices.ReloadSearchAnalyzersAsync(new ReloadSearchAnalyzersRequest(All)))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_reload_search_analyzers")
					.Fluent(c => c.Indices.ReloadSearchAnalyzers(index))
					.Request(c => c.Indices.ReloadSearchAnalyzers(new ReloadSearchAnalyzersRequest(index)))
					.FluentAsync(c => c.Indices.ReloadSearchAnalyzersAsync(index))
					.RequestAsync(c => c.Indices.ReloadSearchAnalyzersAsync(new ReloadSearchAnalyzersRequest(index)))
				;
		}
	}
}
