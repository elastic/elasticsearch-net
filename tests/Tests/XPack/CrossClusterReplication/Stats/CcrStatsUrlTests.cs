// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Stats
{
	public class CcrStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await UrlTester.GET($"/_ccr/stats")
				.Fluent(c => c.CrossClusterReplication.Stats(d => d))
				.Request(c => c.CrossClusterReplication.Stats(new CcrStatsRequest()))
				.FluentAsync(c => c.CrossClusterReplication.StatsAsync(d => d))
				.RequestAsync(c => c.CrossClusterReplication.StatsAsync(new CcrStatsRequest()));
	}
}
