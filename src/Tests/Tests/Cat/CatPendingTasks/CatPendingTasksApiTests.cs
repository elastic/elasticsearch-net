using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatPendingTasks
{
	public class CatPendingTasksApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatPendingTasksRecord>, ICatPendingTasksRequest, CatPendingTasksDescriptor,
			CatPendingTasksRequest>
	{
		public CatPendingTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/pending_tasks";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatPendingTasks(),
			(client, f) => client.CatPendingTasksAsync(),
			(client, r) => client.CatPendingTasks(r),
			(client, r) => client.CatPendingTasksAsync(r)
		);
	}
}
