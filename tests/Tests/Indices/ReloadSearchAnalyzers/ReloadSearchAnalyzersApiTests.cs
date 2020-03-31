using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.ReloadSearchAnalyzers
{
	public class ReloadSearchAnalyzersApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ReloadSearchAnalyzersResponse, IReloadSearchAnalyzersRequest, ReloadSearchAnalyzersDescriptor, ReloadSearchAnalyzersRequest>
	{
		public ReloadSearchAnalyzersApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override ReloadSearchAnalyzersDescriptor NewDescriptor() => new ReloadSearchAnalyzersDescriptor(CallIsolatedValue);

		protected override Func<ReloadSearchAnalyzersDescriptor, IReloadSearchAnalyzersRequest> Fluent => d => d.Index(CallIsolatedValue);
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReloadSearchAnalyzersRequest Initializer => new ReloadSearchAnalyzersRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_reload_search_analyzers";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.ReloadSearchAnalyzers(CallIsolatedValue, f),
			(client, f) => client.Indices.ReloadSearchAnalyzersAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.ReloadSearchAnalyzers(r),
			(client, r) => client.Indices.ReloadSearchAnalyzersAsync(r)
		);
	}
}
