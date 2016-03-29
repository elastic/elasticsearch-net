using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.Flush
{
	[Collection(IntegrationContext.ReadOnly)]
	public class FlushApiTests : ApiIntegrationTestBase<IFlushResponse, IFlushRequest, FlushDescriptor, FlushRequest>
	{
		public FlushApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Flush(Index<Project>(), f),
			fluentAsync: (client, f) => client.FlushAsync(Index<Project>(), f),
			request: (client, r) => client.Flush(r),
			requestAsync: (client, r) => client.FlushAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_flush?allow_no_indices=true";

		protected override Func<FlushDescriptor, IFlushRequest> Fluent => d => d.AllowNoIndices();

		protected override FlushRequest Initializer => new FlushRequest(Index<Project>()) { AllowNoIndices = true };
	}
}
