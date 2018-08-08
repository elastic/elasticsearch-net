using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Pinging
{
	public class FirstUsage
	{
		/**=== Ping on first usage
		*
		* Pinging is enabled by default for the <<static-connection-pool, Static>>, <<sniffing-connection-pool, Sniffing>>
		* and <<sticky-connection-pool, Sticky>> connection pools.
		* This means that the first time a node is used or resurrected, a ping is issued a with a small (configurable) timeout,
		* allowing the client to fail and fallover to a healthy node much faster than attempting a request, that may be heavier than a ping.
		*/

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task PingFailsFallsOverToHealthyNodeWithoutPing()
		{
			/** Here's an example with a cluster with two nodes where the second node fails on ping */
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(2)
				.Ping(p => p.Succeeds(Always))
				.Ping(p => p.OnPort(9201).FailAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			/** When making the calls, the first call goes to 9200 which succeeds,
			* and the 2nd call does a ping on 9201 because it's used for the first time.
			* The ping fails so we wrap over to node 9200 which we've already pinged.
			*
			* Finally we assert that the connectionpool has one node that is marked as dead
			*/
			await audit.TraceCalls(

				new ClientCall {
					{ PingSuccess, 9200},
					{ HealthyResponse, 9200},
					{ pool =>
					{
						pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(0);
					} }
				},
				new ClientCall {
					{ PingFailure, 9201},
					{ HealthyResponse, 9200},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(1) }
				}
			);
		}
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task PingFailsFallsOverMultipleTimesToHealthyNode()
		{
			/** A cluster with 4 nodes where the second and third pings fail */
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(4)
				.Ping(p => p.SucceedAlways())
				.Ping(p => p.OnPort(9201).FailAlways())
				.Ping(p => p.OnPort(9202).FailAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCalls(
				new ClientCall {
					{ PingSuccess, 9200}, // <1> The first call goes to 9200, which succeeds
					{ HealthyResponse, 9200},
					{ pool =>
						pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(0)
					}
				},
				new ClientCall {
					{ PingFailure, 9201}, // <2> The 2nd call does a ping on 9201 because its used for the first time. This fails
					{ PingFailure, 9202}, // <3> So we ping 9202. This _also_ fails
					{ PingSuccess, 9203}, // <4> We then ping 9203 because we haven't used it before and it succeeds
					{ HealthyResponse, 9203},
					{ pool =>
						pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(2) // <5> Finally, we assert that the connection pool has two nodes that are marked as dead
					}
				}
			);
		}

		/**
		 * All nodes are pinged on first use, provided they are healthy
		 */
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task AllNodesArePingedOnlyOnFirstUseProvidedTheyAreHealthy()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(4)
				.Ping(p => p.SucceedAlways()) // <1> Pings on nodes always succeed
				.StaticConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCalls(
				new ClientCall { { PingSuccess, 9200}, { HealthyResponse, 9200} }, // <2> A successful ping on each node
				new ClientCall { { PingSuccess, 9201}, { HealthyResponse, 9201} },
				new ClientCall { { PingSuccess, 9202}, { HealthyResponse, 9202} },
				new ClientCall { { PingSuccess, 9203}, { HealthyResponse, 9203} },
				new ClientCall { { HealthyResponse, 9200} },
				new ClientCall { { HealthyResponse, 9201} },
				new ClientCall { { HealthyResponse, 9202} },
				new ClientCall { { HealthyResponse, 9203} },
				new ClientCall { { HealthyResponse, 9200} }
			);
		}
	}
}
