using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Sticky
{
	public class StickySniffingConnectionPool
	{
		/** Sticky sniffing connection pool
		 *
		 * This pool is a is an extended StickyConnectionPool that supports sniffing and sorting
		 * the nodes a sniff returns.
		*/
		[U] public void EachViewStartsAtNextPositionAndWrapsOver()
		{
			var numberOfNodes = 10;
			var uris = Enumerable.Range(9200, numberOfNodes).Select(p => new Uri("http://localhost:" + p));
			var pool = new Elasticsearch.Net.StickySniffingConnectionPool(uris, (n)=>0f);

            /**
			* Here we have setup a sticky connection pool seeded with 10 nodes all weighted the same.
			* So what order we expect? Imagine the following:
			*
			* Thread A calls `.CreateView()` and gets returned the first live node
			* Thread B calls `.CreateView()` and gets returned the same node, since the first
            * node is still good
			*/
            var startingPositions = Enumerable.Range(0, numberOfNodes)
				.Select(i => pool.CreateView().First())
				.Select(n => n.Uri.Port)
				.ToList();

			var expectedOrder = Enumerable.Repeat(9200, numberOfNodes);
			startingPositions.Should().ContainInOrder(expectedOrder);
		}

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task FavorsNodeWithGreatestWeightAndFallsOver()
		{
			IEnumerable<Node> Nodes(int start) => Enumerable.Range(start, 4)
				.Select(i => new Uri($"http://localhost:{9200 + i}"))
				.Select((u, i) => new Node(u)
				{
					Settings = new Dictionary<string, object> {{"rack", $"rack_{u.Port - 9200}"}}
				});

			/** We set up a cluster with 4 nodes all having a different rack id
				our Sticky Sniffing Connection Pool gives the most weight to rack_2 and rack_11.
				We initially only seed nodes `9200-9203` in racks 0 to 3. So we should be sticky on rack_2.
				We setup node 9202 to fail after two client calls in which case we sniff and find nodes
				`9210-9213` in which case we should become sticky on rack_11.
			 */
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(Nodes(0))
				.ClientCalls(p => p.OnPort(9202).Succeeds(Twice).ThrowsAfterSucceeds())
				.ClientCalls(p => p.FailAlways())
				.Sniff(s=>s.SucceedAlways(Framework.Cluster
					.Nodes(Nodes(10))
					.ClientCalls(p => p.SucceedAlways()))
				)
				.StickySniffingConnectionPool(n=>
					(n.Settings.TryGetValue("rack", out var v) && v.ToString() == "rack_2" ? 10 : 0)
					+(n.Settings.TryGetValue("rack", out var r) && r.ToString() == "rack_11" ? 10 : 0)
				)
				.Settings(p => p.DisablePing().SniffOnStartup(false))
			);

			/** Our first call happens on 9202 because we sorted that to the top as its on rack_2
			* After two succesful calls our sticky node throws an exception and we sniff and failover.
			* Sniffing happens on the next node in order (9200) and the sniffing response returns nodes
			* 9210 to 9213. We should now be stick on 9211 as its on rack_11
			*/
			await audit.TraceCalls(
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall {
					{ BadResponse, 9202 },
					{ SniffOnFail },
					{ SniffSuccess, 9200 },
					{ HealthyResponse, 9211},
				},
				/** Now we are sticky on 9211 onwards */
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9211 } }
			);
		}
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffOnStartupReseedsPutsMostWeightedNodeToFront()
		{
			IEnumerable<Node> Nodes(int start) => Enumerable.Range(start, 4)
				.Select(i => new Uri($"http://localhost:{9200 + i}"))
				.Select((u, i) => new Node(u)
				{
					Settings = new Dictionary<string, object> {{"rack", $"rack_{u.Port - 9200}"}}
				});

			/** We seed a cluster with an array of 4 Uri's starting at port 9200.
			* Our sniffing sorted connection pool is set up to favor nodes in rack_2
			*/
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(4)
				.ClientCalls(p => p.SucceedAlways())
				.Sniff(s=>s.SucceedAlways(Framework.Cluster
					.Nodes(Nodes(0))
					.ClientCalls(p => p.SucceedAlways()))
				)
				.StickySniffingConnectionPool(n=>
					(n.Settings.TryGetValue("rack", out var v) && v.ToString() == "rack_2" ? 10 : 0)
				)
				.Settings(p => p.DisablePing())
			);

			/** Sniff happens on 9200 because our seed has no knowledge of rack ids
			* However when we reseed the nodes from the sniff response we sort 9202 to to top
			* because it lives in rack_2
			*/
			await audit.TraceCalls(
				new ClientCall
				{
					{ SniffOnStartup },
					{ SniffSuccess, 9200 },
					{ HealthyResponse, 9202},
				},
				/** We are sticky on 9202 for as long as it keeps returning valid responses */
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9202} }
			);
		}

		//hide
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task PicksADifferentNodeEachTimeAnodeIsDown()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(4)
				.ClientCalls(p => p.Fails(Always))
				.StickySniffingConnectionPool()
				.Settings(p => p.DisablePing().SniffOnStartup(false).SniffOnConnectionFault(false))
			);

			await audit.TraceCalls(
				/** All the calls fail */
				new ClientCall {
					{ BadResponse, 9200},
					{ BadResponse, 9201},
					{ BadResponse, 9202},
					{ BadResponse, 9203},
					{ MaxRetriesReached },
					{ pool => pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(4) }
				},
				/** After all our registered nodes are marked dead we want to sample a single dead node
				* each time to quickly see if the cluster is back up. We do not want to retry all 4
				* nodes
				*/
				new ClientCall {
					{ AllNodesDead },
					{ Resurrection, 9200},
					{ BadResponse, 9200},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(4) }
				},
				new ClientCall {
					{ AllNodesDead },
					{ Resurrection, 9201},
					{ BadResponse, 9201},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(4) }
				},
				new ClientCall {
					{ AllNodesDead },
					{ Resurrection, 9202},
					{ BadResponse, 9202},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(4) }
				},
				new ClientCall {
					{ AllNodesDead },
					{ Resurrection, 9203},
					{ BadResponse, 9203},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(4) }
				}
			);
		}

	}
}
