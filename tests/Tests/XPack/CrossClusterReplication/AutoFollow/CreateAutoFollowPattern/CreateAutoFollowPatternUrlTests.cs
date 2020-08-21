// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.CreateAutoFollowPattern
{
	public class CreateAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await PUT($"/_ccr/auto_follow/{name}")
				.Fluent(c => c.CrossClusterReplication.CreateAutoFollowPattern(name, d => d))
				.Request(c => c.CrossClusterReplication.CreateAutoFollowPattern(new CreateAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.CreateAutoFollowPatternAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.CreateAutoFollowPatternAsync(new CreateAutoFollowPatternRequest(name)));

		}
	}
}
