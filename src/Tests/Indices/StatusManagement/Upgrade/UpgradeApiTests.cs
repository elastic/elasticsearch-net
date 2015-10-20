using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Nest.Static;

namespace Tests.Indices.StatusManagement.Upgrade
{
	[Collection(IntegrationContext.ReadOnly)]
	public class UpgradeApiTests : ApiIntegrationTestBase<IUpgradeResponse, IUpgradeRequest, UpgradeDescriptor, UpgradeRequest>
	{
		public UpgradeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Upgrade(AllIndices, f),
			fluentAsync: (client, f) => client.UpgradeAsync(AllIndices, f),
			request: (client, r) => client.Upgrade(r),
			requestAsync: (client, r) => client.UpgradeAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_upgrade?allow_no_indices=true";

		protected override Func<UpgradeDescriptor, IUpgradeRequest> Fluent => d => d.AllowNoIndices();

		protected override UpgradeRequest Initializer => new UpgradeRequest(AllIndices) { AllowNoIndices = true };

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
