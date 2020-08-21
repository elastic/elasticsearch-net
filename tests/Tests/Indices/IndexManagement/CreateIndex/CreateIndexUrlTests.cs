// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.CreateIndex
{
	public class CreateIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await PUT($"/{index}")
					.Fluent(c => c.Indices.Create(index, s => s))
					.Request(c => c.Indices.Create(new CreateIndexRequest(index)))
					.FluentAsync(c => c.Indices.CreateAsync(index))
					.RequestAsync(c => c.Indices.CreateAsync(new CreateIndexRequest(index)))
				;
		}
	}
}
