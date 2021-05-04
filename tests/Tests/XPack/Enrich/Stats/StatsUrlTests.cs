// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Enrich.Stats
{
	public class EnrichStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_enrich/_stats")
			.Fluent(c => c.Enrich.Stats())
			.Request(c => c.Enrich.Stats(new EnrichStatsRequest()))
			.FluentAsync(c => c.Enrich.StatsAsync())
			.RequestAsync(c => c.Enrich.StatsAsync(new EnrichStatsRequest()));
	}
}
