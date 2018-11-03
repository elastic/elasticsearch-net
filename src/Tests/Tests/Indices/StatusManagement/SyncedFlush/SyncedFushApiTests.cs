using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.StatusManagement.SyncedFlush
{
	public class SyncedFlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, ISyncedFlushResponse, ISyncedFlushRequest, SyncedFlushDescriptor, SyncedFlushRequest>
	{
		public SyncedFlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<SyncedFlushDescriptor, ISyncedFlushRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SyncedFlushRequest Initializer => new SyncedFlushRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush/synced?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.SyncedFlush(CallIsolatedValue, f),
			(client, f) => client.SyncedFlushAsync(CallIsolatedValue, f),
			(client, r) => client.SyncedFlush(r),
			(client, r) => client.SyncedFlushAsync(r)
		);
	}
}
