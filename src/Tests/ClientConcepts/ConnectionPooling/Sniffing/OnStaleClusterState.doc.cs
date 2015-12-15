using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;
using Elasticsearch.Net.Providers;
using FluentAssertions;
using Tests.Framework;
using System.Linq;
using System.Collections.Generic;
using Tests.Framework.MockData;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using static Elasticsearch.Net.Connection.AuditEvent;
using static Tests.Framework.TimesHelper;

namespace Tests.ClientConcepts.ConnectionPooling.Sniffing
{
	public class OnStaleClusterState
	{
		/** == Sniffing periodically
		* 
		* Connection pools that return true for `SupportsReseeding` can be configured to sniff periodically.
		* In addition to sniffing on startup and sniffing on failures, sniffing periodically can benefit scenerio's where
		* clusters are often scaled horizontally during peak hours. An application might have a healthy view of a subset of the nodes
		* but without sniffing periodically it will never find the nodes that have been added to help out with load
		*/

		[U]
		[SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task ASniffOnStartupHappens()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.MasterEligable(9202, 9203, 9204)
				.ClientCalls(r => r.SucceedAlways())
				.Sniff(s => s.SucceedAlways(Framework.Cluster
					.Nodes(100)
					.MasterEligable(9202, 9203, 9204)
					.ClientCalls(r => r.SucceedAlways())
					.Sniff(ss => ss.SucceedAlways(Framework.Cluster
						.Nodes(10)
						.MasterEligable(9202, 9203, 9204)
						.ClientCalls(r => r.SucceedAlways())
					))
				))
				.SniffingConnectionPool()
				.Settings(s => s
					.DisablePing()
					.SniffOnConnectionFault(false)
					.SniffOnStartup(false)
					.SniffLifeSpan(TimeSpan.FromMinutes(30))
				)
			);
			/** healty cluster all nodes return healthy responses*/
			audit = await audit.TraceCalls(
				new ClientCall { { HealthyResponse, 9200 } },
				new ClientCall { { HealthyResponse, 9201 } },
				new ClientCall { { HealthyResponse, 9202 } },
				new ClientCall { { HealthyResponse, 9203 } },
				new ClientCall { { HealthyResponse, 9204 } },
				new ClientCall { { HealthyResponse, 9205 } },
				new ClientCall { { HealthyResponse, 9206 } },
				new ClientCall { { HealthyResponse, 9207 } },
				new ClientCall { { HealthyResponse, 9208 } },
				new ClientCall { { HealthyResponse, 9209 } },
				new ClientCall {
					{ HealthyResponse, 9200 },
                    { pool => pool.Nodes.Count.Should().Be(10) }
				}
			);
			/** Now let's forward the clock 31 minutes, our sniff lifespan should now go state
			* and the first call should do a sniff which discovered we scaled up to a 100 nodes!
			*/
			audit.ChangeTime(d => d.AddMinutes(31));
			audit = await audit.TraceCalls(
				new ClientCall {
					/** a sniff is done first and it prefers the first node master node */
					{ SniffOnStaleCluster },
					{ SniffSuccess, 9202 },
					{ HealthyResponse, 9201 },
                    { pool => pool.Nodes.Count.Should().Be(100) }
				}
			);

			audit.ChangeTime(d => d.AddMinutes(31));
			audit = await audit.TraceCalls(
				new ClientCall {
					//TODO discuss with @gmarz prefering master nodes is good, always picking the first though?
					/** a sniff is done first and it prefers the first node master node */
					{ SniffOnStaleCluster },
					{ SniffSuccess, 9202 },
					{ HealthyResponse, 9200 },
                    { pool => pool.Nodes.Count.Should().Be(10) }
				}
			);
		}

	}
}
