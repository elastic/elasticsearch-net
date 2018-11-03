using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.Monitoring.IndicesRecovery
{
	public class RecoveryStatusApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IRecoveryStatusResponse, IRecoveryStatusRequest, RecoveryStatusDescriptor, RecoveryStatusRequest>
	{
		public RecoveryStatusApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override RecoveryStatusRequest Initializer => new RecoveryStatusRequest(Infer.AllIndices);
		protected override string UrlPath => "/_recovery";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.RecoveryStatus(Infer.AllIndices, f),
			(client, f) => client.RecoveryStatusAsync(Infer.AllIndices, f),
			(client, r) => client.RecoveryStatus(r),
			(client, r) => client.RecoveryStatusAsync(r)
		);
	}
}
