// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Flush
{
	public class FlushUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_all/_flush")
					.Fluent(c => c.Indices.Flush(All))
					.Request(c => c.Indices.Flush(new FlushRequest(All)))
					.FluentAsync(c => c.Indices.FlushAsync(All))
					.RequestAsync(c => c.Indices.FlushAsync(new FlushRequest(All)))
				;

			await POST($"/_flush")
					.Request(c => c.Indices.Flush(new FlushRequest()))
					.RequestAsync(c => c.Indices.FlushAsync(new FlushRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_flush")
					.Fluent(c => c.Indices.Flush(index))
					.Request(c => c.Indices.Flush(new FlushRequest(index)))
					.FluentAsync(c => c.Indices.FlushAsync(index))
					.RequestAsync(c => c.Indices.FlushAsync(new FlushRequest(index)))
				;
		}
	}
}
