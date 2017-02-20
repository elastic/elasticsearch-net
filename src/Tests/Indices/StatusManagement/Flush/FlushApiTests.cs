using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.Flush
{
	public class FlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase<IntrusiveOperationCluster, IFlushResponse, IFlushRequest, FlushDescriptor, FlushRequest>
	{

		public FlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Flush(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.FlushAsync(CallIsolatedValue, f),
			request: (client, r) => client.Flush(r),
			requestAsync: (client, r) => client.FlushAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush?allow_no_indices=true";

		protected override Func<FlushDescriptor, IFlushRequest> Fluent => d => d.AllowNoIndices();

		protected override FlushRequest Initializer => new FlushRequest(CallIsolatedValue) { AllowNoIndices = true };
	}
}
