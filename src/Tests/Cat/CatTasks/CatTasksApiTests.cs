using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatTasks
{
	[Collection(TypeOfCluster.ReadOnly)]
	public class CatTasksApiTests : ApiIntegrationTestBase<ICatResponse<CatTasksRecord>, ICatTasksRequest, CatTasksDescriptor, CatTasksRequest>
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
