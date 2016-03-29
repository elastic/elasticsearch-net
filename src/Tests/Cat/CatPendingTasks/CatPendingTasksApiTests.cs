using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatPendingTasks
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatPendingTasksApiTests : ApiIntegrationTestBase<ICatResponse<CatPendingTasksRecord>, ICatPendingTasksRequest, CatPendingTasksDescriptor, CatPendingTasksRequest>
	{
		public CatPendingTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatPendingTasks(),
			fluentAsync: (client, f) => client.CatPendingTasksAsync(),
			request: (client, r) => client.CatPendingTasks(r),
			requestAsync: (client, r) => client.CatPendingTasksAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/pending_tasks";

	}

}
