using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IClearCacheResponse, IClearCacheRequest, ClearCacheDescriptor, ClearCacheRequest>
	{
		public ClearCacheApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearCacheDescriptor, IClearCacheRequest> Fluent => d => d.Recycler();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearCacheRequest Initializer => new ClearCacheRequest(AllIndices) { Recycler = true };
		protected override string UrlPath => "/_cache/clear?recycler=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClearCache(AllIndices, f),
			(client, f) => client.ClearCacheAsync(AllIndices, f),
			(client, r) => client.ClearCache(r),
			(client, r) => client.ClearCacheAsync(r)
		);
	}
}
