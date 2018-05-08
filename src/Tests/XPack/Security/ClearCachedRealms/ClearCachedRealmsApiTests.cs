using System;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.XPack.Security.ClearCachedRealms
{
	[SkipVersion("<2.3.0", "")]
	public class ClearCachedRealmsApiTests : ApiIntegrationTestBase<XPackCluster, IClearCachedRealmsResponse, IClearCachedRealmsRequest, ClearCachedRealmsDescriptor, ClearCachedRealmsRequest>
	{
		public ClearCachedRealmsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClearCachedRealms(this.Realm, f),
			fluentAsync: (client, f) => client.ClearCachedRealmsAsync(this.Realm, f),
			request: (client, r) => client.ClearCachedRealms(r),
			requestAsync: (client, r) => client.ClearCachedRealmsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_xpack/security/realm/{U(this.Realm)}/_clear_cache";

		protected override bool SupportsDeserialization => false;

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Realm => SecurityRealms.FileRealm;

		protected override ClearCachedRealmsRequest Initializer => new ClearCachedRealmsRequest(this.Realm);

		protected override ClearCachedRealmsDescriptor NewDescriptor() => new ClearCachedRealmsDescriptor(this.Realm);

		protected override Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> Fluent => d => d;

		protected override void ExpectResponse(IClearCachedRealmsResponse response)
		{
			response.ClusterName.Should().StartWith("xpack-cluster-");
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;
			node.Should().NotBeNull();
			node.Name.Should().StartWith("xpack-node-");
		}
	}

}
