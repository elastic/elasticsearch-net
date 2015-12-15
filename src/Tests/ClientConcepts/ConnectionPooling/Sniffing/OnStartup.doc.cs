using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using static Tests.Framework.TimesHelper;

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

			 await audit.TraceCall(new ClientCall {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFailure, 9200},
				{ AuditEvent.SniffFailure, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.PingSuccess , 9200},
				{ AuditEvent.HealthyResponse, 9200}
			});
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
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFailure, 9200},
				{ AuditEvent.SniffFailure, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.PingSuccess, 9204},
				{ AuditEvent.HealthyResponse, 9204}
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
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFailure, 9200},
				{ AuditEvent.SniffFailure, 9201},
				{ AuditEvent.SniffFailure, 9202},
				{ AuditEvent.SniffFailure, 9203},
				{ AuditEvent.SniffFailure, 9204},
				{ AuditEvent.SniffFailure, 9205},
				{ AuditEvent.SniffFailure, 9206},
				{ AuditEvent.SniffFailure, 9207},
				{ AuditEvent.SniffFailure, 9208},
				{ AuditEvent.SniffSuccess, 9209},
				{ AuditEvent.PingSuccess, 9200},
				{ AuditEvent.HealthyResponse, 9200}
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
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.PingSuccess, 9200},
				{ AuditEvent.HealthyResponse, 9200}
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
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFailure, 9200},
				{ AuditEvent.SniffFailure, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.PingSuccess, 9200},
				{ AuditEvent.HealthyResponse, 9200}
			});
		}
	}
}
