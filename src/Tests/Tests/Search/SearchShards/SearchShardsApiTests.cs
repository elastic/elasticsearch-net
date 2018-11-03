using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.SearchShards
{
	public class SearchShardsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchShardsResponse, ISearchShardsRequest, SearchShardsDescriptor<Project>,
			SearchShardsRequest<Project>>
	{
		public SearchShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;


		protected override int ExpectStatusCode => 200;

		protected override Func<SearchShardsDescriptor<Project>, ISearchShardsRequest> Fluent => s => s;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchShardsRequest<Project> Initializer => new SearchShardsRequest<Project>();
		protected override string UrlPath => $"project/_search_shards";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.SearchShards(f),
			(c, f) => c.SearchShardsAsync(f),
			(c, r) => c.SearchShards(r),
			(c, r) => c.SearchShardsAsync(r)
		);

		protected override SearchShardsDescriptor<Project> NewDescriptor() => new SearchShardsDescriptor<Project>();
	}
}
