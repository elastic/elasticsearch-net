using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.Security.Role.ClearCachedRoles
{
	[Collection(IntegrationContext.Shield)]
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRolesApiTests : ApiIntegrationTestBase<IClearCachedRolesResponse, IClearCachedRolesRequest, ClearCachedRolesDescriptor, ClearCachedRolesRequest>
	{
		public ClearCachedRolesApiTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClearCachedRoles(this.Role, f),
			fluentAsync: (client, f) => client.ClearCachedRolesAsync(this.Role, f),
			request: (client, r) => client.ClearCachedRoles(r),
			requestAsync: (client, r) => client.ClearCachedRolesAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			client.PutRole(new PutRoleRequest(this.Role));
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_shield/role/{this.Role}/_clear_cache";

		protected override bool SupportsDeserialization => false;

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Role => $"role-{CallIsolatedValue}";

		protected override ClearCachedRolesRequest Initializer => new ClearCachedRolesRequest(this.Role);

		protected override ClearCachedRolesDescriptor NewDescriptor() => new ClearCachedRolesDescriptor(this.Role);

		protected override Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> Fluent => d => d;

		protected override void ExpectResponse(IClearCachedRolesResponse response)
		{
			response.ClusterName.Should().StartWith("shield-cluster-");
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().StartWith("shield-node-");
			node.Status.Should().NotBeNull();
			node.Status.Success.Should().BeTrue();
		}
	}

}
