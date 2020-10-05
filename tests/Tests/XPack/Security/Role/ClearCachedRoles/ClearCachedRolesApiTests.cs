// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.Role.ClearCachedRoles
{
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRolesApiTests
		: ApiIntegrationTestBase<XPackCluster, ClearCachedRolesResponse, IClearCachedRolesRequest, ClearCachedRolesDescriptor,
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
			(client, f) => client.Security.ClearCachedRoles(Role, f),
			(client, f) => client.Security.ClearCachedRolesAsync(Role, f),
			(client, r) => client.Security.ClearCachedRoles(r),
			(client, r) => client.Security.ClearCachedRolesAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client) => client.Security.PutRole(new PutRoleRequest(Role));

		protected override ClearCachedRolesDescriptor NewDescriptor() => new ClearCachedRolesDescriptor(Role);

		protected override void ExpectResponse(ClearCachedRolesResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
