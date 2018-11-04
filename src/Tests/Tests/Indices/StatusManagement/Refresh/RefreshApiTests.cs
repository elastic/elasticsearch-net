using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, IRefreshResponse, IRefreshRequest, RefreshDescriptor, RefreshRequest>
	{
		public RefreshApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<RefreshDescriptor, IRefreshRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override RefreshRequest Initializer => new RefreshRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_refresh?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Refresh(CallIsolatedValue, f),
			(client, f) => client.RefreshAsync(CallIsolatedValue, f),
			(client, r) => client.Refresh(r),
			(client, r) => client.RefreshAsync(r)
		);
	}
}
