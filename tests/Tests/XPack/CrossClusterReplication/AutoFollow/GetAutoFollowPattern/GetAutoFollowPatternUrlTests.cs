// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.GetAutoFollowPattern
{
	public class GetAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.GET($"/_ccr/auto_follow/{name}")
				.Fluent(c => c.CrossClusterReplication.GetAutoFollowPattern(name))
				.Request(c => c.CrossClusterReplication.GetAutoFollowPattern(new GetAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.GetAutoFollowPatternAsync(name))
				.RequestAsync(c => c.CrossClusterReplication.GetAutoFollowPatternAsync(new GetAutoFollowPatternRequest(name)));

			await UrlTester.GET($"/_ccr/auto_follow")
				.Fluent(c => c.CrossClusterReplication.GetAutoFollowPattern())
				.Request(c => c.CrossClusterReplication.GetAutoFollowPattern(new GetAutoFollowPatternRequest()))
				.FluentAsync(c => c.CrossClusterReplication.GetAutoFollowPatternAsync())
				.RequestAsync(c => c.CrossClusterReplication.GetAutoFollowPatternAsync(new GetAutoFollowPatternRequest()));

		}
	}
}
