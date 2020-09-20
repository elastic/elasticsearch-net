// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.StatusManagement.Flush
{
	public class FlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase<IntrusiveOperationCluster, FlushResponse, IFlushRequest, FlushDescriptor, FlushRequest>
	{
		public FlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<FlushDescriptor, IFlushRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override FlushRequest Initializer => new FlushRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Flush(CallIsolatedValue, f),
			(client, f) => client.Indices.FlushAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.Flush(r),
			(client, r) => client.Indices.FlushAsync(r)
		);
	}
}
