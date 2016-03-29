using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.Upgrade
{
	[Collection(IntegrationContext.ReadOnly)]
	public class UpgradeApiTests : ApiIntegrationTestBase<IUpgradeResponse, IUpgradeRequest, UpgradeDescriptor, UpgradeRequest>
	{
		public UpgradeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Upgrade(Index<Project>(), f),
			fluentAsync: (client, f) => client.UpgradeAsync(Index<Project>(), f),
			request: (client, r) => client.Upgrade(r),
			requestAsync: (client, r) => client.UpgradeAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_upgrade?allow_no_indices=true";

		protected override Func<UpgradeDescriptor, IUpgradeRequest> Fluent => d => d.AllowNoIndices();

		protected override UpgradeRequest Initializer => new UpgradeRequest(Index<Project>()) { AllowNoIndices = true };
	}
}
