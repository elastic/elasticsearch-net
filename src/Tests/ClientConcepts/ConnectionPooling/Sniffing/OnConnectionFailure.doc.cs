using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Sniffing
{
	public class OnConnectionFailure
	{
		/**== Sniffing on connection failure
		*
		* Sniffing on connection is enabled by default when using a connection pool that allows reseeding.
		* The only IConnectionPool we ship that allows this is the <<sniffing-connection-pool,SniffingConnectionPool>>.
		*
		* This can be very handy to force a refresh of the pools known healthy node by inspecting Elasticsearch itself.
		* A sniff tries to get the nodes by asking each currently known node until one response.
		*/

		[U] public async Task DoesASniffAfterConnectionFailure()
		{
			/**
			* Here we seed our connection with 5 known nodes 9200-9204 of which we think
			* 9202, 9203, 9204 are master eligible nodes. Our virtualized cluster will throw once when doing
			* a search on 9201. This should a sniff to be kicked off.
			*/
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(5)
				.MasterEligible(9202, 9203, 9204)
				.ClientCalls(r => r.SucceedAlways())
				.ClientCalls(r => r.OnPort(9201).Fails(Once))
				/**
				* When the call fails on 9201 the sniff succeeds and returns a new cluster of healthy nodes
				* this cluster only has 3 nodes and the known masters are 9200 and 9202 but a search on 9201
				* still fails once
				*/
				.Sniff(p => p.SucceedAlways(Framework.Cluster
					.Nodes(3)
					.MasterEligible(9200, 9202)
					.ClientCalls(r => r.OnPort(9201).Fails(Once))
					/**
					* After this second failure on 9201 another sniff will be returned a cluster that no
					* longer fails but looks completely different (9210-9212) we should be able to handle this
					*/
					.Sniff(s => s.SucceedAlways(Framework.Cluster
						.Nodes(3, 9210)
						.MasterEligible(9210, 9212)
						.ClientCalls(r => r.SucceedAlways())
						.Sniff(r => r.SucceedAlways())
					))
				))
				.SniffingConnectionPool()
				.Settings(s => s.DisablePing().SniffOnStartup(false))
			);

			audit = await audit.TraceCalls(
			/** */
				new ClientCall {
					{ HealthyResponse, 9200 },
					{ pool =>  pool.Nodes.Count.Should().Be(5) }
				},
				new ClientCall {
					{ BadResponse, 9201},
					/** We assert we do a sniff on our first known master node 9202 */
					{ SniffOnFail },
					{ SniffSuccess, 9202},
					{ HealthyResponse, 9200},
					/** Our pool should now have three nodes */
					{ pool =>  pool.Nodes.Count.Should().Be(3) }
				},
				new ClientCall {
					{ BadResponse, 9201},
					/** We assert we do a sniff on the first master node in our updated cluster */
					{ SniffOnFail },
					{ SniffSuccess, 9200},
					{ HealthyResponse, 9210},
					{ pool =>  pool.Nodes.Count.Should().Be(3) }
				},
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9212 } },
				new ClientCall { { HealthyResponse, 9210 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9212 } },
				new ClientCall { { HealthyResponse, 9210 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9212 } },
				new ClientCall { { HealthyResponse, 9210 } }
			);
		}

		[U] public async Task DoesASniffAfterConnectionFailureOnPing()
		{
			/** Here we set up our cluster exactly the same as the previous setup
			* Only we enable pinging (default is true) and make the ping fail
			*/
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(5)
				.MasterEligible(9202, 9203, 9204)
				.Ping(r => r.OnPort(9201).Fails(Once))
				.Sniff(p => p.SucceedAlways(Framework.Cluster
					.Nodes(3)
					.MasterEligible(9200, 9202)
					.Ping(r => r.OnPort(9201).Fails(Once))
					.Sniff(s => s.SucceedAlways(Framework.Cluster
						.Nodes(3, 9210)
						.MasterEligible(9210, 9211)
						.Ping(r => r.SucceedAlways())
						.Sniff(r => r.SucceedAlways())
					))
				))
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup(false))
			);

			audit = await audit.TraceCalls(
				new ClientCall {
					{ PingSuccess, 9200 },
					{ HealthyResponse, 9200 },
					{ pool =>  pool.Nodes.Count.Should().Be(5) }
				},
				new ClientCall {
					{ PingFailure, 9201},
					/** We assert we do a sniff on our first known master node 9202 */
					{ SniffOnFail },
					{ SniffSuccess, 9202},
					{ PingSuccess, 9200},
					{ HealthyResponse, 9200},
					/** Our pool should now have three nodes */
					{ pool =>  pool.Nodes.Count.Should().Be(3) }
				},
				new ClientCall {
					{ PingFailure, 9201},
					/** We assert we do a sniff on the first master node in our updated cluster */
					{ SniffOnFail },
					{ SniffSuccess, 9200},
					{ PingSuccess, 9210},
					{ HealthyResponse, 9210},
					{ pool =>  pool.Nodes.Count.Should().Be(3) }
				},
				new ClientCall { { PingSuccess, 9211 }, { HealthyResponse, 9211 } },
				new ClientCall { { PingSuccess, 9212 }, { HealthyResponse, 9212 } },
				/** 9210 was already pinged after the sniff returned the new nodes */
				new ClientCall { { HealthyResponse, 9210 } },
				new ClientCall { { HealthyResponse, 9211 } },
				new ClientCall { { HealthyResponse, 9212 } },
				new ClientCall { { HealthyResponse, 9210 } }
			);
		}

	}
}
