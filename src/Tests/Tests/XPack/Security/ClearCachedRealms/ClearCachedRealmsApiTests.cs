using System;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.ClearCachedRealms
{
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRealmsApiTests
		: ApiIntegrationTestBase<XPackCluster, IClearCachedRealmsResponse, IClearCachedRealmsRequest, ClearCachedRealmsDescriptor,
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

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Realm => SecurityRealms.FileRealm;

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClearCachedRealms(Realm, f),
			(client, f) => client.ClearCachedRealmsAsync(Realm, f),
			(client, r) => client.ClearCachedRealms(r),
			(client, r) => client.ClearCachedRealmsAsync(r)
		);

		protected override ClearCachedRealmsDescriptor NewDescriptor() => new ClearCachedRealmsDescriptor(Realm);

		protected override void ExpectResponse(IClearCachedRealmsResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().NotBeNullOrEmpty();
		}
	}
}
