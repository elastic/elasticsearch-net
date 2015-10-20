using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Nest.Static;

namespace Tests.Indices.StatusManagement.SyncedFlush
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SyncedFlushApiTests : ApiIntegrationTestBase<IShardsOperationResponse, ISyncedFlushRequest, SyncedFlushDescriptor, SyncedFlushRequest>
	{
		public SyncedFlushApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SyncedFlush(AllIndices, f),
			fluentAsync: (client, f) => client.SyncedFlushAsync(AllIndices, f),
			request: (client, r) => client.SyncedFlush(r),
			requestAsync: (client, r) => client.SyncedFlushAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_flush/synced?allow_no_indices=true";

		protected override Func<SyncedFlushDescriptor, ISyncedFlushRequest> Fluent => d => d.AllowNoIndices();

		protected override SyncedFlushRequest Initializer => new SyncedFlushRequest(AllIndices) { AllowNoIndices = true };

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
