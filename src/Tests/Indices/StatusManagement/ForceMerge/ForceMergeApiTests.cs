using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.ForceMerge
{
	[SkipVersion("<2.1.0", "")]
	public class ForceMergeApiTests : ApiIntegrationAgainstNewIndexTestBase<IntrusiveOperationCluster, IForceMergeResponse, IForceMergeRequest, ForceMergeDescriptor, ForceMergeRequest>
	{
		public ForceMergeApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ForceMerge(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.ForceMergeAsync(CallIsolatedValue, f),
			request: (client, r) => client.ForceMerge(r),
			requestAsync: (client, r) => client.ForceMergeAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_forcemerge?allow_no_indices=true";

		protected override Func<ForceMergeDescriptor, IForceMergeRequest> Fluent => d => d.AllowNoIndices();

		protected override ForceMergeRequest Initializer => new ForceMergeRequest(CallIsolatedValue) { AllowNoIndices = true };

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
		});
	}
}
