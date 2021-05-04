// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.MappingManagement.GetMapping
{
	public class GetMappingUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			await GET($"/index1%2Cindex2/_mapping")
					.Fluent(c => c.Indices.GetMapping<Project>(m => m.Index(index)))
					.Request(c => c.Indices.GetMapping(new GetMappingRequest(index)))
					.FluentAsync(c => c.Indices.GetMappingAsync<Project>(m => m.Index(index)))
					.RequestAsync(c => c.Indices.GetMappingAsync(new GetMappingRequest(index)))
				;
		}
	}
}
