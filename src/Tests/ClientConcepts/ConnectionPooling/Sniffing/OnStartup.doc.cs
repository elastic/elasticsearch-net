using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Sniffing
{
	public class OnStartupSniffing
	{
		/** == Sniffing on startup
		* 
		* Connection pools that return true for `SupportsReseeding` by default sniff on startup.
		*/

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task ASniffOnStartupHappens()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9202).Succeeds(Always))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			 await audit.TraceCall(new ClientCall
			 {
				{ SniffOnStartup},
				{ SniffFailure, 9200},
				{ SniffFailure, 9201},
				{ SniffSuccess, 9202},
				{ PingSuccess , 9200},
				{ HealthyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task ASniffOnStartupHappensOnce()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9202).Succeeds(Always))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			 await audit.TraceCalls(
				 new ClientCall
				 {
					{ SniffOnStartup},
					{ SniffFailure, 9200},
					{ SniffFailure, 9201},
					{ SniffSuccess, 9202},
					{ PingSuccess , 9200},
					{ HealthyResponse, 9200}
				},
				new ClientCall
				{
					{ PingSuccess, 9201},
					{ HealthyResponse, 9201}
				}
			);
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffOnStartUpTakesNewClusterState()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9202).Succeeds(Always, Framework.Cluster.Nodes(8, startFrom: 9204)))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new ClientCall {
				{ SniffOnStartup},
				{ SniffFailure, 9200},
				{ SniffFailure, 9201},
				{ SniffSuccess, 9202},
				{ PingSuccess, 9204},
				{ HealthyResponse, 9204}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffTriesAllNodes()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9209).Succeeds(Always))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new ClientCall {
				{ SniffOnStartup},
				{ SniffFailure, 9200},
				{ SniffFailure, 9201},
				{ SniffFailure, 9202},
				{ SniffFailure, 9203},
				{ SniffFailure, 9204},
				{ SniffFailure, 9205},
				{ SniffFailure, 9206},
				{ SniffFailure, 9207},
				{ SniffFailure, 9208},
				{ SniffSuccess, 9209},
				{ PingSuccess, 9200},
				{ HealthyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffPrefersMasterNodes()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(new[] {
					new Node(new Uri("http://localhost:9200")) { MasterEligable = false },
					new Node(new Uri("http://localhost:9201")) { MasterEligable = false },
					new Node(new Uri("http://localhost:9202")) { MasterEligable = true },
				})
				.Sniff(s => s.Succeeds(Always))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new ClientCall {
				{ SniffOnStartup},
				{ SniffSuccess, 9202},
				{ PingSuccess, 9200},
				{ HealthyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffPrefersMasterNodesButStillFailsOver()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(new[] {
					new Node(new Uri("http://localhost:9200")) { MasterEligable = true },
					new Node(new Uri("http://localhost:9201")) { MasterEligable = true },
					new Node(new Uri("http://localhost:9202")) { MasterEligable = false },
				})
				.Sniff(s => s.Fails(Always))
				.Sniff(s => s.OnPort(9202).Succeeds(Always))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new ClientCall {
				{ SniffOnStartup},
				{ SniffFailure, 9200},
				{ SniffFailure, 9201},
				{ SniffSuccess, 9202},
				{ PingSuccess, 9200},
				{ HealthyResponse, 9200}
			});
		}
	}
}
