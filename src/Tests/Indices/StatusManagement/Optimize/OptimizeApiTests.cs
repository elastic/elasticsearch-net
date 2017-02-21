using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.Optimize
{
	public class OptimizeApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, IOptimizeResponse, IOptimizeRequest, OptimizeDescriptor, OptimizeRequest>
	{
		public OptimizeApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Optimize(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.OptimizeAsync(CallIsolatedValue, f),
			request: (client, r) => client.Optimize(r),
			requestAsync: (client, r) => client.OptimizeAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_optimize?allow_no_indices=true";

		protected override Func<OptimizeDescriptor, IOptimizeRequest> Fluent => d => d.AllowNoIndices();

		protected override OptimizeRequest Initializer => new OptimizeRequest(CallIsolatedValue) { AllowNoIndices = true };

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
		});
	}
}
