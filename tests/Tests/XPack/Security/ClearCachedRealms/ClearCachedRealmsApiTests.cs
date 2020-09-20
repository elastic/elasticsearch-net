// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.ClearCachedRealms
{
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRealmsApiTests
		: ApiIntegrationTestBase<XPackCluster, ClearCachedRealmsResponse, IClearCachedRealmsRequest, ClearCachedRealmsDescriptor,
			ClearCachedRealmsRequest>
	{
		public ClearCachedRealmsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearCachedRealmsRequest Initializer => new ClearCachedRealmsRequest(Realm);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/realm/{U(Realm)}/_clear_cache";

		private string Realm => SecurityRealms.FileRealm;

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.ClearCachedRealms(Realm, f),
			(client, f) => client.Security.ClearCachedRealmsAsync(Realm, f),
			(client, r) => client.Security.ClearCachedRealms(r),
			(client, r) => client.Security.ClearCachedRealmsAsync(r)
		);

		protected override ClearCachedRealmsDescriptor NewDescriptor() => new ClearCachedRealmsDescriptor(Realm);

		protected override void ExpectResponse(ClearCachedRealmsResponse response)
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
