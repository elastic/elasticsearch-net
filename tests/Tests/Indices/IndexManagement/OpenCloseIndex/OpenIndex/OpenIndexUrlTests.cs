// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.OpenIndex
{
	public class OpenIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await UrlTester.POST($"/{index}/_open")
					.Fluent(c => c.Indices.Open(indices, s => s))
					.Request(c => c.Indices.Open(new OpenIndexRequest(indices)))
					.FluentAsync(c => c.Indices.OpenAsync(indices))
					.RequestAsync(c => c.Indices.OpenAsync(new OpenIndexRequest(indices)))
				;
		}
	}
}
