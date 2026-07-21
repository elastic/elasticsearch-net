// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Elasticsearch.Ephemeral;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations;

public abstract class AggregationUsageWithVerifyTestBase<TCluster>
		: ApiIntegrationTestBase<TCluster, SearchResponse<Project>, SearchRequestDescriptor<Project>, SearchRequest<Project>>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
{
	protected AggregationUsageWithVerifyTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected virtual Indices AgainstIndex { get; } = Infer.Index<Project>();

	protected override bool VerifyJson => true;
	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Size(0)
		.Index(AgainstIndex)
		.Query(QueryScope)
		.Aggregations(FluentAggregations);

	protected abstract Action<AggregationDescriptor<Project>> FluentAggregations { get; }

	protected override HttpMethod ExpectHttpMethod => HttpMethod.POST;

	protected override SearchRequest<Project> Initializer =>
		new(AgainstIndex)
		{
			Query = QueryScope,
			Size = 0,
			Aggregations = InitializerAggregations
		};

	protected abstract AggregationDictionary InitializerAggregations { get; }

	protected virtual Query QueryScope { get; } = new TermQuery("type") { Value = Project.TypeName };

	protected virtual object QueryScopeJson { get; } = new { term = new { type = new { value = Project.TypeName } } };

	protected override string ExpectedUrlPathAndQuery => $"/project/_search";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Search(f),
		(client, f) => client.SearchAsync(f),
		(client, r) => client.Search<Project>(r),
		(client, r) => client.SearchAsync<Project>(r)
	);
}
