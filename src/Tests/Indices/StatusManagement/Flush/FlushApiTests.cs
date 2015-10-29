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

namespace Tests.Indices.StatusManagement.Flush
{
	[Collection(IntegrationContext.ReadOnly)]
	public class FlushApiTests : ApiIntegrationTestBase<IShardsOperationResponse, IFlushRequest, FlushDescriptor, FlushRequest>
	{
		public FlushApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Flush(AllIndices, f),
			fluentAsync: (client, f) => client.FlushAsync(AllIndices, f),
			request: (client, r) => client.Flush(r),
			requestAsync: (client, r) => client.FlushAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_flush?allow_no_indices=true";

		protected override Func<FlushDescriptor, IFlushRequest> Fluent => d => d.AllowNoIndices();

		protected override FlushRequest Initializer => new FlushRequest(AllIndices) { AllowNoIndices = true };

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
