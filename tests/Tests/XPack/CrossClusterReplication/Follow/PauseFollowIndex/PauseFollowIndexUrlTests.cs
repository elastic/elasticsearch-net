// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Follow.PauseFollowIndex
{
	public class PauseFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/pause_follow")
				.Fluent(c => c.CrossClusterReplication.PauseFollowIndex(name, d => d))
				.Request(c => c.CrossClusterReplication.PauseFollowIndex(new PauseFollowIndexRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.PauseFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.PauseFollowIndexAsync(new PauseFollowIndexRequest(name)));

		}
	}
}
