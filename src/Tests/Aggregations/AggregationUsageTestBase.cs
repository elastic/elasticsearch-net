using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Aggregations
{
	public static class AggregationsTests
	{
		public static bool UsesTypedKeys { get; } = Gimme.Random.Bool(TestClient.Configuration.Seed);
	}

	public abstract class AggregationUsageTestBase
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected AggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath =>
			$"/project/doc/_search?typed_keys={AggregationsTests.UsesTypedKeys.ToString().ToLowerInvariant()}";

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>(this.AgainstIndex)
			{
				Size =  0,
				TypedKeys = AggregationsTests.UsesTypedKeys,
				Aggregations = InitializerAggs
			};

		protected virtual Nest.Indices AgainstIndex { get; }

		protected abstract AggregationDictionary InitializerAggs { get; }

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Index(AgainstIndex)
			.TypedKeys(AggregationsTests.UsesTypedKeys)
			.Aggregations(this.FluentAggs);

		protected abstract Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs { get; }

		protected sealed override object ExpectJson => new
		{
			size = 0,
			aggs = AggregationJson
		};

		protected abstract object AggregationJson { get;  }
	}

	public abstract class ProjectsOnlyAggregationUsageTestBase : AggregationUsageTestBase
	{
		protected override string UrlPath =>
			$"/{DefaultSeeder.ProjectsAliasFilter}/doc/_search?typed_keys={AggregationsTests.UsesTypedKeys.ToString().ToLowerInvariant()}";

		protected ProjectsOnlyAggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Nest.Indices AgainstIndex => DefaultSeeder.ProjectsAliasFilter;
	}
}
