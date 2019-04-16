using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.Role.ClearCachedRoles
{
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRolesApiTests
		: ApiIntegrationTestBase<XPackCluster, IClearCachedRolesResponse, IClearCachedRolesRequest, ClearCachedRolesDescriptor,
			ClearCachedRolesRequest>
	{
		public ClearCachedRolesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearCachedRolesRequest Initializer => new ClearCachedRolesRequest(Role);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/role/{Role}/_clear_cache";

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Role => $"role-{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClearCachedRoles(Role, f),
			(client, f) => client.ClearCachedRolesAsync(Role, f),
			(client, r) => client.ClearCachedRoles(r),
			(client, r) => client.ClearCachedRolesAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client) => client.PutRole(new PutRoleRequest(Role));

		protected override ClearCachedRolesDescriptor NewDescriptor() => new ClearCachedRolesDescriptor(Role);

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
