using System;
using Elasticsearch.Net_5_2_0;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest_5_2_0.Infer;

namespace Tests.Indices.StatusManagement.SyncedFlush
{
	public class SyncedFlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, ISyncedFlushResponse, ISyncedFlushRequest, SyncedFlushDescriptor, SyncedFlushRequest>
	{
		public SyncedFlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SyncedFlush(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.SyncedFlushAsync(CallIsolatedValue, f),
			request: (client, r) => client.SyncedFlush(r),
			requestAsync: (client, r) => client.SyncedFlushAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush/synced?allow_no_indices=true";

		protected override Func<SyncedFlushDescriptor, ISyncedFlushRequest> Fluent => d => d.AllowNoIndices();

		protected override SyncedFlushRequest Initializer => new SyncedFlushRequest(CallIsolatedValue) { AllowNoIndices = true };
	}
}
