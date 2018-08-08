using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cat.CatTasks
{
	public class CatTasksApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatTasksRecord>, ICatTasksRequest, CatTasksDescriptor, CatTasksRequest>
	{
		public CatTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatTasks(),
			fluentAsync: (client, f) => client.CatTasksAsync(),
			request: (client, r) => client.CatTasks(r),
			requestAsync: (client, r) => client.CatTasksAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/tasks";
	}
}
