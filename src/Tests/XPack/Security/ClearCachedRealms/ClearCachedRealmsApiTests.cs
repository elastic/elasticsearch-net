using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.Security.ClearCachedRealms
{
	[Collection(IntegrationContext.Shield)]
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRealmsApiTests : ApiIntegrationTestBase<IClearCachedRealmsResponse, IClearCachedRealmsRequest, ClearCachedRealmsDescriptor, ClearCachedRealmsRequest>
	{
		public ClearCachedRealmsApiTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClearCachedRealms(this.Realm, f),
			fluentAsync: (client, f) => client.ClearCachedRealmsAsync(this.Realm, f),
			request: (client, r) => client.ClearCachedRealms(r),
			requestAsync: (client, r) => client.ClearCachedRealmsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_shield/realm/{this.Realm}/_clear_cache";

		protected override bool SupportsDeserialization => false;

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Realm => $"realm-{CallIsolatedValue}";

		protected override ClearCachedRealmsRequest Initializer => new ClearCachedRealmsRequest(this.Realm);

		protected override ClearCachedRealmsDescriptor NewDescriptor() => new ClearCachedRealmsDescriptor(this.Realm);

		protected override Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> Fluent => d => d;

		protected override void ExpectResponse(IClearCachedRealmsResponse response)
		{
			response.ClusterName.Should().StartWith("shield-cluster-");
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().StartWith("shield-node-");
			node.Status.Should().NotBeNull();
			node.Status.Success.Should().BeFalse();
			node.Status.Type.Should().Be("failed_node_exception");
			node.Status.Reason.Should().Contain("Failed node");
			node.Status.CausedBy.Should().NotBeNull();
			node.Status.CausedBy.Reason.Should().Contain("could not find active realm");
		}
	}
}
