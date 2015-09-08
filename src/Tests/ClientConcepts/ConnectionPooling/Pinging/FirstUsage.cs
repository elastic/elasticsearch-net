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

namespace Tests.ClientConcepts.ConnectionPooling.Pinging
{
	public class FirstUsage
	{
		/** == Pinging
		* 
		* Pinging is enabled by default for the Static & Sniffing connection pool. 
		* This means that the first time a node is used or resurrected we issue a ping with a smaller (configurable) timeout.
		* This allows us to fail and fallover to a healthy node faster
		*/

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task PingFailsFallsOverToHealthyNodeWithoutPing()
		{
			/** A cluster with 2 nodes where the second node fails on ping */
			var audit = new Auditor(() => Cluster
				.Nodes(2)
				.Ping(p => p.Succeeds(Always))
				.Ping(p => p.OnPort(9201).FailAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCalls(
				/** The first call goes to 9200 which succeeds */
				new CallTrace { 
					{ PingSuccess, 9200},
					{ HealhyResponse, 9200},
					{ pool =>
					{
						pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(0);
					} }
				},
				/** The 2nd call does a ping on 9201 because its used for the first time. 
				* It fails so we wrap over to node 9200 which we've already pinged */
				new CallTrace { 
					{ PingFailure, 9201},
					{ HealhyResponse, 9200},
					/** Finally we assert that the connectionpool has one node that is marked as dead */
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(1) }
				}
			);
		}
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task PingFailsFallsOverMultipleTimesToHealthyNode()
		{
			/** A cluster with 4 nodes where the second and third pings fail */
			var audit = new Auditor(() => Cluster
				.Nodes(4)
				.Ping(p => p.SucceedAlways())
				.Ping(p => p.OnPort(9201).FailAlways())
				.Ping(p => p.OnPort(9202).FailAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCalls(
				/** The first call goes to 9200 which succeeds */
				new CallTrace { 
					{ PingSuccess, 9200},
					{ HealhyResponse, 9200},
					{ pool =>
					{
						pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(0);
					} }
				},
				/** The 2nd call does a ping on 9201 because its used for the first time. 
				* It fails and so we ping 9202 which also fails. We then ping 9203 becuase 
				* we haven't used it before and it succeeds */
				new CallTrace { 
					{ PingFailure, 9201},
					{ PingFailure, 9202},
					{ PingSuccess, 9203},
					{ HealhyResponse, 9203},
					/** Finally we assert that the connectionpool has two nodes that are marked as dead */
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(2) }
				}
			);
		}
		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task AllNodesArePingedOnlyOnFirstUseProvidedTheyAreHealthy()
		{
			/**  A healthy cluster of 4 (min master nodes of 3 of course!) */ 
			var audit = new Auditor(() => Cluster
				.Nodes(4)
				.Ping(p => p.SucceedAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCalls(
				new CallTrace { { PingSuccess, 9200}, { HealhyResponse, 9200} },
				new CallTrace { { PingSuccess, 9201}, { HealhyResponse, 9201} },
				new CallTrace { { PingSuccess, 9202}, { HealhyResponse, 9202} },
				new CallTrace { { PingSuccess, 9203}, { HealhyResponse, 9203} },
				new CallTrace { { HealhyResponse, 9200} },
				new CallTrace { { HealhyResponse, 9201} },
				new CallTrace { { HealhyResponse, 9202} },
				new CallTrace { { HealhyResponse, 9203} },
				new CallTrace { { HealhyResponse, 9200} }
			);
		}
	}
}
