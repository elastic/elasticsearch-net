using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.StatusManagement.Flush
{
	public class FlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase<IntrusiveOperationCluster, IFlushResponse, IFlushRequest, FlushDescriptor, FlushRequest>
	{
		public FlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<FlushDescriptor, IFlushRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override FlushRequest Initializer => new FlushRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Flush(CallIsolatedValue, f),
			(client, f) => client.FlushAsync(CallIsolatedValue, f),
			(client, r) => client.Flush(r),
			(client, r) => client.FlushAsync(r)
		);
	}
}
