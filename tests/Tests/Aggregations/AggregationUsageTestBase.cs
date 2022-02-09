// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Elasticsearch.Ephemeral;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations;

public abstract class AggregationUsageTestBase<TCluster>
		: ApiIntegrationTestBase<TCluster, SearchResponse<Project>, SearchRequestDescriptor<Project>, SearchRequest<Project>>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
{
	protected AggregationUsageTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected virtual Indices AgainstIndex { get; } = Infer.Index<Project>();

	protected abstract object AggregationJson { get; }

	protected override bool ExpectIsValid => true;

	protected sealed override object ExpectJson => QueryScopeJson == null
		? new { size = 0, aggregations = AggregationJson }
		: new { size = 0, aggregations = AggregationJson, query = QueryScopeJson };

	protected override int ExpectStatusCode => 200;

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Size(0)
		.Index(AgainstIndex)
		.Query(QueryScope)
		.Aggregations(FluentAggs);

	protected abstract Action<AggregationContainerDescriptor<Project>> FluentAggs { get; }

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override SearchRequest<Project> Initializer =>
		new(AgainstIndex)
		{
			Query = QueryScope,
			Size = 0,
			Aggregations = InitializerAggs
		};

	protected abstract AggregationDictionary InitializerAggs { get; }

	protected virtual QueryContainer QueryScope { get; } = new TermQuery { Field = "type", Value = Project.TypeName };

	protected virtual object QueryScopeJson { get; } = new { term = new { type = new { value = Project.TypeName } } };

	protected override string ExpectedUrlPathAndQuery => $"/project/_search";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Search(f),
		(client, f) => client.SearchAsync(f),
		(client, r) => client.Search<Project>(r),
		(client, r) => client.SearchAsync<Project>(r)
	);
}
