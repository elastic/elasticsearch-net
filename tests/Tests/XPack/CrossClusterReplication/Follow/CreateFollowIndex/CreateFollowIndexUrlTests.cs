// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.CrossClusterReplication.Follow.CreateFollowIndex
{
	public class CreateFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await PUT($"/{name}/_ccr/follow")
				.Fluent(c => c.CrossClusterReplication.CreateFollowIndex(name, d => d))
				.Request(c => c.CrossClusterReplication.CreateFollowIndex(new CreateFollowIndexRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.CreateFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.CreateFollowIndexAsync(new CreateFollowIndexRequest(name)));

		}
	}
}
