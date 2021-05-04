// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.ForceMerge
{
	public class ForceMergeUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_all/_forcemerge")
					.Fluent(c => c.Indices.ForceMerge(All))
					.Request(c => c.Indices.ForceMerge(new ForceMergeRequest(All)))
					.FluentAsync(c => c.Indices.ForceMergeAsync(All))
					.RequestAsync(c => c.Indices.ForceMergeAsync(new ForceMergeRequest(All)))
				;

			await POST($"/_forcemerge")
					.Request(c => c.Indices.ForceMerge(new ForceMergeRequest()))
					.RequestAsync(c => c.Indices.ForceMergeAsync(new ForceMergeRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_forcemerge")
					.Fluent(c => c.Indices.ForceMerge(index))
					.Request(c => c.Indices.ForceMerge(new ForceMergeRequest(index)))
					.FluentAsync(c => c.Indices.ForceMergeAsync(index))
					.RequestAsync(c => c.Indices.ForceMergeAsync(new ForceMergeRequest(index)))
				;
		}
	}
}
