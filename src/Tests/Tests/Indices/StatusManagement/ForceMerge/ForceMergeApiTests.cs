using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.StatusManagement.ForceMerge
{
	[SkipVersion("<2.1.0", "")]
	public class ForceMergeApiTests
		: ApiIntegrationAgainstNewIndexTestBase<IntrusiveOperationCluster, IForceMergeResponse, IForceMergeRequest, ForceMergeDescriptor,
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
			(client, f) => client.ForceMerge(CallIsolatedValue, f),
			(client, f) => client.ForceMergeAsync(CallIsolatedValue, f),
			(client, r) => client.ForceMerge(r),
			(client, r) => client.ForceMergeAsync(r)
		);

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
		});
	}
}
