// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_refresh")
					.Request(c => c.Indices.Refresh(new RefreshRequest()))
					.RequestAsync(c => c.Indices.RefreshAsync(new RefreshRequest()))
				;

			await POST($"/_all/_refresh")
					.Fluent(c => c.Indices.Refresh(All))
					.Request(c => c.Indices.Refresh(new RefreshRequest(All)))
					.FluentAsync(c => c.Indices.RefreshAsync(All))
					.RequestAsync(c => c.Indices.RefreshAsync(new RefreshRequest(All)))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_refresh")
					.Fluent(c => c.Indices.Refresh(index))
					.Request(c => c.Indices.Refresh(new RefreshRequest(index)))
					.FluentAsync(c => c.Indices.RefreshAsync(index))
					.RequestAsync(c => c.Indices.RefreshAsync(new RefreshRequest(index)))
				;
		}
	}
}
