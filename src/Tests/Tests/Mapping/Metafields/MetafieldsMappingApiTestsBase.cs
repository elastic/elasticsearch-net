using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Metafields
{
	public abstract class MetafieldsMappingApiTestsBase
		: ApiTestBase<ReadOnlyCluster, PutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		protected MetafieldsMappingApiTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{CallIsolatedValue}/project/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Map(f),
			(client, f) => client.MapAsync(f),
			(client, r) => client.Map(r),
			(client, r) => client.MapAsync(r)
		);
	}
}
