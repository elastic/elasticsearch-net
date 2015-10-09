using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.ClusterSettings.ClusterGetSettings
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterGetSettingsApiTests : ApiTestBase<IClusterGetSettingsResponse, IClusterGetSettingsRequest, ClusterGetSettingsDescriptor, ClusterGetSettingsRequest>
	{
		public ClusterGetSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterGetSettings(),
			fluentAsync: (client, f) => client.ClusterGetSettingsAsync(),
			request: (client, r) => client.ClusterGetSettings(r),
			requestAsync: (client, r) => client.ClusterGetSettingsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/settings";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});

	}

}
