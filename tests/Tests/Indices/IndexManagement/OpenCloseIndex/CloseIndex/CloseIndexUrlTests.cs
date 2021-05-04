// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Nest.Indices;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	public class CloseIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await POST($"/{index}/_close")
					.Fluent(c => c.Indices.Close(indices, s => s))
					.Request(c => c.Indices.Close(new CloseIndexRequest(indices)))
					.FluentAsync(c => c.Indices.CloseAsync(indices))
					.RequestAsync(c => c.Indices.CloseAsync(new CloseIndexRequest(indices)))
				;
		}
	}
}
