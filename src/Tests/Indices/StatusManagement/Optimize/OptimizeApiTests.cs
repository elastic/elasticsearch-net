using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Indices.StatusManagement.Optimize
{
	[Collection(IntegrationContext.ReadOnly)]
	public class OptimizeApiTests : ApiIntegrationTestBase<IShardsOperationResponse, IOptimizeRequest, OptimizeDescriptor, OptimizeRequest>
	{
		public OptimizeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Optimize(Index<Project>(), f),
			fluentAsync: (client, f) => client.OptimizeAsync(Index<Project>(), f),
			request: (client, r) => client.Optimize(r),
			requestAsync: (client, r) => client.OptimizeAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_optimize?allow_no_indices=true";

		protected override Func<OptimizeDescriptor, IOptimizeRequest> Fluent => d => d.AllowNoIndices();

		protected override OptimizeRequest Initializer => new OptimizeRequest(Index<Project>()) { AllowNoIndices = true };
	}
}
