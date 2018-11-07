using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.OpenIndex
{
	public class OpenIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, IOpenIndexResponse, IOpenIndexRequest, OpenIndexDescriptor, OpenIndexRequest>
	{
		public OpenIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<OpenIndexDescriptor, IOpenIndexRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override OpenIndexRequest Initializer => new OpenIndexRequest(CallIsolatedValue)
		{
			IgnoreUnavailable = true
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_open?ignore_unavailable=true";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				client.CreateIndex(index);
				client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Yellow).Index(index));
				client.CloseIndex(index);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.OpenIndex(CallIsolatedValue, f),
			(client, f) => client.OpenIndexAsync(CallIsolatedValue, f),
			(client, r) => client.OpenIndex(r),
			(client, r) => client.OpenIndexAsync(r)
		);

		protected override OpenIndexDescriptor NewDescriptor() => new OpenIndexDescriptor(CallIsolatedValue);
	}
}
