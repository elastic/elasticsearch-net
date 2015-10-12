using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Cluster.ClusterReroute
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterRerouteApiTests : ApiTestBase<IClusterRerouteResponse, IClusterRerouteRequest, ClusterRerouteDescriptor, ClusterRerouteRequest>
	{

		public ClusterRerouteApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterReroute(f),
			fluentAsync: (client, f) => client.ClusterRerouteAsync(f),
			request: (client, r) => client.ClusterReroute(r),
			requestAsync: (client, r) => client.ClusterRerouteAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_cluster/reroute";

		protected override Func<ClusterRerouteDescriptor, IClusterRerouteRequest> Fluent => c => c
			.Allocate(a => a
				.Index<Project>()
				.Node("x")
				.AllowPrimary(false)
				.Shard(0)
			)
			.Move(a => a
				.ToNode("y")
				.FromNode("x")
				.Index("project")
				.Shard(0)
			)
			.Cancel(a => a
				.Index("project")
				.Node("x")
				.Shard(1)
			);

		protected override ClusterRerouteRequest Initializer => new ClusterRerouteRequest
		{
			Commands = new List<IClusterRerouteCommand>
			{
				new AllocateClusterRerouteCommand { AllowPrimary = false, Index = IndexName.From<Project>(), Node = "x", Shard = 0},
				new MoveClusterRerouteCommand { Index = IndexName.From<Project>(), FromNode = "x", ToNode = "y", Shard = 0},
				new CancelClusterRerouteCommand() { Index = "project", Node = "x", Shard = 1}
			}
		};

		protected override object ExpectJson => new
		{
			commands = new []
			{
				new Dictionary<string, object> { { "allocate", new
				{
					allow_primary = false,
					index = "project",
					node = "x",
					shard = 0
				} } },
				new Dictionary<string, object> { { "move", new
				{
					to_node = "y",
					from_node = "x",
					index = "project",
					shard = 0
				} } },
				new Dictionary<string, object> { { "cancel", new
				{
					index = "project",
					node ="x",
					shard = 1
				} } },
			}
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});

	}

}
