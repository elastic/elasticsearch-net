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

namespace Tests.XPack.Security.ClearCachedPrivileges
{
	[SkipVersion("<7.9.0", "Introduced in 7.9.0")]
	public class ClearCachedPrivilegesApiTests
		: ApiIntegrationTestBase<XPackCluster, ClearCachedPrivilegesResponse, IClearCachedPrivilegesRequest, ClearCachedPrivilegesDescriptor,
			ClearCachedPrivilegesRequest>
	{
		public ClearCachedPrivilegesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearCachedPrivilegesDescriptor, IClearCachedPrivilegesRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearCachedPrivilegesRequest Initializer => new ClearCachedPrivilegesRequest(Application);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/privilege/{U(Application)}/_clear_cache";

		private string Application => "myapp";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.ClearCachedPrivileges(Application, f),
			(client, f) => client.Security.ClearCachedPrivilegesAsync(Application, f),
			(client, r) => client.Security.ClearCachedPrivileges(r),
			(client, r) => client.Security.ClearCachedPrivilegesAsync(r)
		);

		protected override ClearCachedPrivilegesDescriptor NewDescriptor() => new ClearCachedPrivilegesDescriptor(Application);

		protected override void ExpectResponse(ClearCachedPrivilegesResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			response.NodeStatistics.Should().NotBeNull();
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().NotBeNullOrEmpty();
		}
	}
}
