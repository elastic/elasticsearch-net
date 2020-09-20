// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, RefreshResponse, IRefreshRequest, RefreshDescriptor, RefreshRequest>
	{
		public RefreshApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<RefreshDescriptor, IRefreshRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override RefreshRequest Initializer => new RefreshRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_refresh?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Refresh(CallIsolatedValue, f),
			(client, f) => client.Indices.RefreshAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.Refresh(r),
			(client, r) => client.Indices.RefreshAsync(r)
		);
	}
}
