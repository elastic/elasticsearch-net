// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.Enrich.Stats
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class EnrichStatsApiTests
		: ApiTestBase<XPackCluster, EnrichStatsResponse, IEnrichStatsRequest, EnrichStatsDescriptor, EnrichStatsRequest>
	{
		public EnrichStatsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => GET;

		protected override string UrlPath => $"/_enrich/_stats";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.Stats(),
			(client, f) => client.Enrich.StatsAsync(),
			(client, r) => client.Enrich.Stats(),
			(client, r) => client.Enrich.StatsAsync()
		);
	}
}
