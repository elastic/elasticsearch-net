using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Search.SearchShards
{
	public class SearchShardsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ISearchShardsResponse, ISearchShardsRequest, SearchShardsDescriptor<Project>, SearchShardsRequest<Project>>
	{
		public SearchShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.SearchShards(f),
			fluentAsync: (c, f) => c.SearchShardsAsync(f),
			request: (c, r) => c.SearchShards(r),
			requestAsync: (c, r) => c.SearchShardsAsync(r)
		);


		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"project/_search_shards";

		protected override SearchShardsDescriptor<Project> NewDescriptor() => new SearchShardsDescriptor<Project>();

		protected override Func<SearchShardsDescriptor<Project>, ISearchShardsRequest> Fluent => s => s;

		protected override SearchShardsRequest<Project> Initializer => new SearchShardsRequest<Project>();
	}
}
