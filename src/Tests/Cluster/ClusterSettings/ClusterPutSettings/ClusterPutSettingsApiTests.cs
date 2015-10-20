using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.ClusterSettings.ClusterPutSettings
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterPutSettingsApiTests : ApiIntegrationTestBase<IClusterPutSettingsResponse, IClusterPutSettingsRequest, ClusterPutSettingsDescriptor, ClusterPutSettingsRequest>
	{
		public ClusterPutSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterPutSettings(f),
			fluentAsync: (client, f) => client.ClusterPutSettingsAsync(f),
			request: (client, r) => client.ClusterPutSettings(r),
			requestAsync: (client, r) => client.ClusterPutSettingsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => "/_cluster/settings";

		protected override ClusterPutSettingsRequest Initializer => new ClusterPutSettingsRequest
		{
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});

	}

}
