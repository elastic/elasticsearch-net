using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.Refresh
{
	[Collection(IntegrationContext.ReadOnly)]
	public class RefreshApiTests : ApiIntegrationTestBase<IRefreshResponse, IRefreshRequest, RefreshDescriptor, RefreshRequest>
	{
		public RefreshApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Refresh(AllIndices, f),
			fluentAsync: (client, f) => client.RefreshAsync(AllIndices, f),
			request: (client, r) => client.Refresh(r),
			requestAsync: (client, r) => client.RefreshAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_refresh?allow_no_indices=true";

		protected override Func<RefreshDescriptor, IRefreshRequest> Fluent => d => d.AllowNoIndices();

		protected override RefreshRequest Initializer => new RefreshRequest(AllIndices) { AllowNoIndices = true };
	}
}
