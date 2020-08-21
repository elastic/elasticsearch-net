// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.SplitIndex
{
	public class SplitIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var source = "source";
			var target = "target";
			await PUT($"/{source}/_split/{target}")
					.Fluent(c => c.Indices.Split(source, target))
					.Request(c => c.Indices.Split(new SplitIndexRequest(source, target)))
					.FluentAsync(c => c.Indices.SplitAsync(source, target))
					.RequestAsync(c => c.Indices.SplitAsync(new SplitIndexRequest(source, target)))
				;
		}
	}
}
