using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Aggregations
{
	public abstract class AggregationUsageTestBase
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected AggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/doc/_search";

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>(AgainstIndex, Type<Project>())
			{
				Query = QueryScope,
				Size = 0,
				TypedKeys = TestClient.Configuration.Random.TypedKeys,
				Aggregations = InitializerAggs
			};

		protected virtual QueryContainer QueryScope { get; }

		protected virtual object QueryScopeJson { get; }

		protected virtual Nest.Indices AgainstIndex { get; } = Index<Project>();

		protected abstract AggregationDictionary InitializerAggs { get; }

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Index(AgainstIndex)
			.Type<Project>()
			.TypedKeys(TestClient.Configuration.Random.TypedKeys)
			.Query(q => QueryScope)
			.Aggregations(FluentAggs);

		protected abstract Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs { get; }

		protected sealed override object ExpectJson => QueryScopeJson == null
			? (object)new { size = 0, aggs = AggregationJson }
			: new { size = 0, aggs = AggregationJson, query = QueryScopeJson };

		protected abstract object AggregationJson { get; }
	}

	public abstract class ProjectsOnlyAggregationUsageTestBase : AggregationUsageTestBase
	{
		protected override string UrlPath => $"/{DefaultSeeder.ProjectsAliasFilter}/doc/_search";

		protected ProjectsOnlyAggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Nest.Indices AgainstIndex => DefaultSeeder.ProjectsAliasFilter;

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();
	}
}
