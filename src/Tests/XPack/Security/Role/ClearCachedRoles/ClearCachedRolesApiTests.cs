using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.XPack.Security.Role.ClearCachedRoles
{
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRolesApiTests : ApiIntegrationTestBase<XPackCluster, IClearCachedRolesResponse, IClearCachedRolesRequest, ClearCachedRolesDescriptor, ClearCachedRolesRequest>
	{
		public ClearCachedRolesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected override string UrlPath => $"/_xpack/security/role/{this.Role}/_clear_cache";

		protected override bool SupportsDeserialization => false;

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Role => $"role-{CallIsolatedValue}";

		protected override ClearCachedRolesRequest Initializer => new ClearCachedRolesRequest(this.Role);

		protected override ClearCachedRolesDescriptor NewDescriptor() => new ClearCachedRolesDescriptor(this.Role);

		protected override Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> Fluent => d => d;

		protected override void ExpectResponse(IClearCachedRolesResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().NotBeNullOrWhiteSpace();
		}
	}

}
