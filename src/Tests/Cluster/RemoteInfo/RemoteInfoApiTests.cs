using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Cluster.RemoteInfo
{
	[SkipVersion("<5.4.0", "new API")]
	public class RemoteInfoApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IRemoteInfoResponse, IRemoteInfoRequest, RemoteInfoDescriptor, RemoteInfoRequest>
	{
		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var enableRemoteClusters = client.ClusterPutSettings(new ClusterPutSettingsRequest
			{
				Transient = new Dictionary<string, object>
				{
					{ "search", new Dictionary<string, object> {
						{ "remote", new Dictionary<string, object> {
							{ "cluster_one", new Dictionary<string, object> {
								{"seeds", new[] {"127.0.0.1:9300", "127.0.0.1:9301"}}
							}},
							{ "cluster_two", new Dictionary<string, object> {
								{"seeds", new[] {"127.0.0.1:9300"}}
							}}
						}}
						}
					}
				}
			});
			enableRemoteClusters.IsValid.Should().BeTrue(enableRemoteClusters.DebugInformation);

			var remoteSearch = client.Search<Project>(s => s.Index(Index<Project>("cluster_one").And<Project>("cluster_two")));
			remoteSearch.IsValid.Should().BeTrue(remoteSearch.DebugInformation);
		}

		public RemoteInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RemoteInfo(),
			fluentAsync: (client, f) => client.RemoteInfoAsync(),
			request: (client, r) => client.RemoteInfo(r),
			requestAsync: (client, r) => client.RemoteInfoAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_remote/info";

		protected override void ExpectResponse(IRemoteInfoResponse response)
		{
			response.Remotes.Should().NotBeEmpty().And.HaveCount(2)
				.And.ContainKey("cluster_one")
				.And.ContainKey("cluster_two");

			foreach(var remote in response.Remotes.Values)
			{
				remote.Connected.Should().BeTrue();
				remote.HttpAddresses.Should().NotBeNullOrEmpty();
				remote.Seeds.Should().NotBeNullOrEmpty();
				remote.InitialConnectTimeout.Should().NotBeNull().And.Be("30s");
				remote.MaxConnectionsPerCluster.Should().BeGreaterThan(0, "max_connections_per_cluster");
				remote.NumNodesConnected.Should().BeGreaterThan(0, "num_nodes_connected");
			}
		}
	}
}
