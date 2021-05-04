// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.MappingManagement.PutMapping
{
	public class PutMappingUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT($"/project/_mapping")
				.Fluent(c => c.Map<Project>(m => m))
				.Request(c => c.Map(new PutMappingRequest("project")))
				.Request(c => c.Map(new PutMappingRequest<Project>()))
				.FluentAsync(c => c.MapAsync<Project>(m => m))
				.RequestAsync(c => c.MapAsync(new PutMappingRequest<Project>()));

			await PUT($"/project/_mapping")
					.Request(c => c.Map(new PutMappingRequest("project")))
					.RequestAsync(c => c.MapAsync(new PutMappingRequest("project")))
				;
		}
	}
}
