// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.StatusManagement.ForceMerge
{
	[SkipVersion("<2.1.0", "")]
	public class ForceMergeApiTests
		: ApiIntegrationAgainstNewIndexTestBase<IntrusiveOperationCluster, ForceMergeResponse, IForceMergeRequest, ForceMergeDescriptor,
			ForceMergeRequest>
	{
		public ForceMergeApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ForceMergeDescriptor, IForceMergeRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ForceMergeRequest Initializer => new ForceMergeRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_forcemerge?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.ForceMerge(CallIsolatedValue, f),
			(client, f) => client.Indices.ForceMergeAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.ForceMerge(r),
			(client, r) => client.Indices.ForceMergeAsync(r)
		);

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
		});
	}
}
