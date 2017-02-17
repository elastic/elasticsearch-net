using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.Versions;
using Xunit;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

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
				AssertPoolBeforeStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.MasterEligible).Should().HaveCount(10);
				},
				AssertPoolAfterStartup = (pool) =>
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
				AssertPoolBeforeStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(10);
				},

				AssertPoolAfterStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(8);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(5);
				}
			};
			await audit.TraceStartup();
		}

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SkipsNodesThatDisableHttp()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.SucceedAlways()
					.Succeeds(Always, Framework.Cluster.Nodes(8).StoresNoData(9200, 9201, 9202).HttpDisabled(9201))
				)
				.SniffingConnectionPool()
				.AllDefaults()
			)
			{
				AssertPoolBeforeStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(10);
					pool.Nodes.Where(n => n.HttpEnabled).Should().HaveCount(10);
					pool.Nodes.Should().OnlyContain(n => n.Uri.Host == "localhost");
				},

				AssertPoolAfterStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(7, "we filtered the node that has no http enabled");
					pool.Nodes.Should().NotContain(n=>n.Uri.Port == 9201);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(5);
				}
			};
			await audit.TraceStartup();
		}

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SkipMasterOnlyNodes()
		{
			var masterNodes = new[] {9200, 9201, 9202};
			var totalNodesInTheCluster = 20;
			//We create a client with a sniffing node connectionpool that seeds all the known master nodes
			var audit = new Auditor(() => Framework.Cluster
				.MasterOnlyNodes(masterNodes.Length)
				// When the client sniffs on startup we see the cluster is 20 nodes in total
				.Sniff(s => s.SucceedAlways()
					.Succeeds(Always, Framework.Cluster.Nodes(totalNodesInTheCluster).StoresNoData(masterNodes).MasterEligible(masterNodes))
				)
				.SniffingConnectionPool()
				.Settings(s=>s.DisablePing()) //for testing simplicity we disable pings
			)
			{
				// before the sniff assert we only see three master only nodes
				AssertPoolBeforeStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(3, "we seeded 3 master only nodes at the start of the application");
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(0, "none of which hold data");
				},
				// after sniff assert we now know about the existence of 20 nodes.
				AssertPoolAfterStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					var nodes = pool.CreateView().ToList();
					nodes.Count().Should().Be(20, "Master nodes are included in the registration of nodes since we still favor sniffing on them");
				}
			};
			// assert that the sniff happened on 9200 before the first API call and that api call hit the first none master eligable node
			audit = await audit.TraceStartup(new ClientCall
			{
				{ SniffSuccess, 9200},
				{ HealthyResponse, 9203}
			});

			//now we do a 1000 different client calls and we assert on each that it was not send to any of the known master only nodes
			var seenNodes = new HashSet<int>();
			foreach (var _ in Enumerable.Range(0, 1000))
			{
				audit = await audit.TraceCalls(
					new ClientCall {{HealthyResponse, (a) =>
					{
						var port = a.Node.Uri.Port;
						masterNodes.Should().NotContain(port);
						seenNodes.Add(port);
					}}}
				);
			}
			//seen nodes is a hash set of all the ports we hit, we assert that this is the known total nodes - the known master only nodes
			seenNodes.Should().HaveCount(totalNodesInTheCluster - masterNodes.Length);
		}

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task RespectsCustomPredicate()
		{
			var totalNodesInTheCluster = 20;
			var setting = "node.attr.rack_id";
			var value = "rack_one";
			var nodesInRackOne = new[] {9204, 9210, 9213};

			//We create a client with a sniffing node connectionpool that seeds all 20 nodes
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(totalNodesInTheCluster)
				// When the client sniffs on startup we see the cluster is still 20 nodes in total
				// However we are now aware of the actual configured settings on the nodes
				.Sniff(s => s.SucceedAlways()
					.Succeeds(Always, Framework.Cluster.Nodes(totalNodesInTheCluster).HasSetting(setting, value, nodesInRackOne))
				)
				.SniffingConnectionPool()
				.Settings(s=>s
					.DisablePing() //for testing simplicity we disable pings
					//We only want to execute API calls to nodes in rack_one
					.NodePredicate(node=>node.Settings.ContainsKey(setting) && node.Settings[setting] == value)
				)
			)
			{
				AssertPoolAfterStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					var nodes = pool.CreateView().ToList();
					nodes.Count(n => n.Settings.ContainsKey(setting)).Should().Be(3, "only three nodes are in rack_one");
				}
			};
			// assert that the sniff happened on 9200 before the first API call and that api call hit the first node in rack_one
			audit = await audit.TraceStartup(new ClientCall
			{
				{ SniffSuccess, 9200},
				{ HealthyResponse, 9204}
			});

			//now we do a 1000 different client calls and we assert on each that it was sent to a node in rack one only
			//respecting our node predicate on connection settings
			var seenNodes = new HashSet<int>();
			foreach (var _ in Enumerable.Range(0, 1000))
			{
				audit = await audit.TraceCalls(
					new ClientCall {{HealthyResponse, (a) =>
					{
						var port = a.Node.Uri.Port;
						nodesInRackOne.Should().Contain(port);
						seenNodes.Add(port);
					}}}
				);
			}
			//assert we hit all the nodes in rack one when we fired off a 1000 api calls.
			seenNodes.Should().HaveCount(nodesInRackOne.Length);
		}

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task CustomPredicateYieldingNothingThrows()
		{
			var totalNodesInTheCluster = 20;

			//We create a client with a sniffing node connectionpool that seeds all 20 nodes and returns all 20 on sniff
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(totalNodesInTheCluster)
				.Sniff(s => s.SucceedAlways()
					.Succeeds(Always, Framework.Cluster.Nodes(totalNodesInTheCluster))
				)
				.SniffingConnectionPool()
				.Settings(s => s
					.DisablePing()
					// evil predicate that declines ALL nodes
					.NodePredicate(node => false)
				)
			);

			await audit.TraceUnexpectedElasticsearchException(new ClientCall
			{
				{ SniffOnStartup }, //audit logs we are sniffing for the very very first time one startup
				{ SniffSuccess }, //this goes ok because we ignore predicate when sniffing
				{ NoNodesAttempted } //when trying to do an actual API call the predicate prevents any nodes from being attempted
			}, e =>
			{
				e.FailureReason.Should().Be(PipelineFailure.Unexpected);
				//generating the debug information should not throw
				Func<string> debug = () => e.DebugInformation;
				debug.Invoking(s =>s()).ShouldNotThrow();
				/* EXAMPLE OF PREVIOUS
# FailureReason: Unrecoverable/Unexpected NoNodesAttempted while attempting POST on default-index/project/_search on an empty node, likely a node predicate on ConnectionSettings not matching ANY nodes
 - [1] SniffOnStartup: Took: 00:00:00
 - [2] SniffSuccess: Node: http://localhost:9200/ Took: 00:00:00
 - [3] NoNodesAttempted: Took: 00:00:00
# Inner Exception: No nodes were attempted, this can happen when a node predicate does not match any nodes
				*/
			});
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
				AssertPoolBeforeStartup = (pool) =>
				{
					pool.Should().NotBeNull();
					pool.Nodes.Should().HaveCount(10);
					pool.Nodes.Where(n => n.HoldsData).Should().HaveCount(10);
					pool.Nodes.Should().OnlyContain(n => n.Uri.Host == "localhost");
				},

				AssertPoolAfterStartup = (pool) =>
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

	public class SniffRoleDetectionCluster : ClusterBase
	{
		protected override string[] ServerSettings
		{
			get
			{
				var es = this.Node.Version > new ElasticsearchVersion("5.0.0-alpha2") ? "" : "es.";

				return new[]
				{
					$"{es}node.data=false",
					$"{es}node.master=true",
					$"{es}node.attr.rack_id=rack_one"
				};
			}
		}
	}

	public class RealWorldRoleDetection : IClusterFixture<SniffRoleDetectionCluster>
	{
		private readonly SniffRoleDetectionCluster _cluster;
		private IConnectionSettingsValues _settings;

		public RealWorldRoleDetection(SniffRoleDetectionCluster cluster)
		{
			this._cluster = cluster;
		}

		[I]
		[SkipVersion("5.0.0-alpha3", "Broken in this version. See https://github.com/elastic/elasticsearch/issues/18794")]
		public async Task SniffPicksUpRoles()
		{
			var node = SniffAndReturnNode();
			node.MasterEligible.Should().BeTrue();
			node.HoldsData.Should().BeFalse();
			node.Settings.Should().NotBeEmpty().And.Contain("node.attr.rack_id", "rack_one");

			node = await SniffAndReturnNodeAsync();
			node.MasterEligible.Should().BeTrue();
			node.HoldsData.Should().BeFalse();
			node.Settings.Should().NotBeEmpty().And.Contain("node.attr.rack_id", "rack_one");
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
			await pipeline.SniffAsync(default(CancellationToken));
			return AssertSniffResponse();
		}

		private RequestPipeline CreatePipeline()
		{
			var uri = TestClient.CreateUri(this._cluster.Node.Port);
			this._settings = new ConnectionSettings(new SniffingConnectionPool(new[] { uri }));
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
