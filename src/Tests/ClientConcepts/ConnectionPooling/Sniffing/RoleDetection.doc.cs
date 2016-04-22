using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Tests.Framework.TimesHelper;

namespace Tests.ClientConcepts.ConnectionPooling.Sniffing
{
	public class RoleDetection
	{
		/**== Sniffing role detection
		*
		* When we sniff the cluster state, we detect the role of the node, whether it's master eligible and holds data.
		* We use this information when selecting a node to perform an API call on.
		*/
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task DetectsMasterNodes()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9202)
					.Succeeds(Always, Framework.Cluster.Nodes(8).MasterEligible(9200, 9201, 9202))
				)
				.SniffingConnectionPool()
				.AllDefaults()
			)
			{
				AssertPoolBeforeCall = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.MasterEligible).Should().HaveCount(10);
				},
				AssertPoolAfterCall = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(8);
					pool.Nodes.Where(n => n.MasterEligible).Should().HaveCount(3);
				}
			};
			await audit.TraceStartup();
		}

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task DetectsDataNodes()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9202)
					.Succeeds(Always, Framework.Cluster.Nodes(8).StoresNoData(9200, 9201, 9202))
				)
				.SniffingConnectionPool()
				.AllDefaults()
			)
			{
				AssertPoolBeforeCall = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(10);
				},

				AssertPoolAfterCall = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(8);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(5);
				}
			};
			await audit.TraceStartup();
		}
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task DetectsFqdn()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.SucceedAlways()
					.Succeeds(Always, Framework.Cluster.Nodes(8).StoresNoData(9200, 9201, 9202).SniffShouldReturnFqdn())
				)
				.SniffingConnectionPool()
				.AllDefaults()
			)
			{
				AssertPoolBeforeCall = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(10);
					pool.Nodes.Should().OnlyContain(n => n.Uri.Host == "localhost");
				},

				AssertPoolAfterCall = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(8);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(5);
					pool.Nodes.Should().OnlyContain(n => n.Uri.Host.StartsWith("fqdn") && !n.Uri.Host.Contains("/"));
				}
			};
			await audit.TraceStartup();
		}
	}

	[CollectionDefinition(IntegrationContext.SniffRoleDetection)]
	public class SniffRoleDetectionCluster : ClusterBase, ICollectionFixture<SniffRoleDetectionCluster>
	{
		protected override string[] ServerSettings => new[]
		{
			"es.node.data=false",
			"es.node.master=true",
		};
	}

	[Collection(IntegrationContext.SniffRoleDetection)]
	public class RealWorldRoleDetection
	{
		private readonly SniffRoleDetectionCluster _cluster;
		private IConnectionSettingsValues _settings;

		public RealWorldRoleDetection(SniffRoleDetectionCluster cluster)
		{
			this._cluster = cluster;
		}

		[I] public async Task SniffPicksUpRoles()
		{
			var node = SniffAndReturnNode();
			node.MasterEligible.Should().BeTrue();
			node.HoldsData.Should().BeFalse();

			node = await SniffAndReturnNodeAsync();
			node.MasterEligible.Should().BeTrue();
			node.HoldsData.Should().BeFalse();
		}

		private Node SniffAndReturnNode()
		{
			var pipeline = CreatePipeline();
			pipeline.Sniff();
			return AssertSniffResponse();
		}

		private async Task<Node> SniffAndReturnNodeAsync()
		{
			var pipeline = CreatePipeline();
			await pipeline.SniffAsync();
			return AssertSniffResponse();
		}

		private RequestPipeline CreatePipeline()
		{
			this._settings =
				this._cluster.Node.Client(u => new SniffingConnectionPool(new[] {u}), c => c.PrettyJson()).ConnectionSettings;
			var pipeline = new RequestPipeline(this._settings, DateTimeProvider.Default, new MemoryStreamFactory(),
				new SearchRequestParameters());
			return pipeline;
		}

		private Node AssertSniffResponse()
		{
			var nodes = this._settings.ConnectionPool.Nodes;
			nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = nodes.First();
			return node;
		}
	}

}
