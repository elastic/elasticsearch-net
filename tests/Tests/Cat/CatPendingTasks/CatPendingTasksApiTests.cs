// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatPendingTasks
{
	public class CatPendingTasksApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatPendingTasksRecord>, ICatPendingTasksRequest, CatPendingTasksDescriptor,
			CatPendingTasksRequest>
	{
		public CatPendingTasksApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/pending_tasks";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.PendingTasks(),
			(client, f) => client.Cat.PendingTasksAsync(),
			(client, r) => client.Cat.PendingTasks(r),
			(client, r) => client.Cat.PendingTasksAsync(r)
		);
	}
}
