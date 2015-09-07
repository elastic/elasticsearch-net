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

namespace Tests.ClientConcepts.LowLevel
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
			var audit = new Auditor(() => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults()
			);

			 await audit.TraceCall(new Audits {
				{ SniffOnStartup},
				{ SniffFailure, 9200},
				{ SniffFailure, 9201},
				{ SniffSuccess, 9202},
				{ PingSuccess , 9200},
				{ HealhyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffOnStartUpTakesNewClusterState()
		{
			var audit = new Auditor(() => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways(Cluster.Nodes(8, startFrom: 9204)))
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new Audits {
				{ SniffOnStartup},
				{ SniffFailure, 9200},
				{ SniffFailure, 9201},
				{ SniffSuccess, 9202},
				{ PingSuccess, 9204},
				{ HealhyResponse, 9204}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffTriesAllNodes()
		{
			var audit = new Auditor(() => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9209).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new Audits {
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
				{ HealhyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffPrefersMasterNodes()
		{
			var audit = new Auditor(() => Cluster
				.Nodes(new[] {
					new Node(new Uri("http://localhost:9200")) { MasterEligable = false },
					new Node(new Uri("http://localhost:9201")) { MasterEligable = false },
					new Node(new Uri("http://localhost:9202")) { MasterEligable = true },
				})
				.Sniff(s => s.SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new Audits {
				{ SniffOnStartup},
				{ SniffSuccess, 9202},
				{ PingSuccess, 9200},
				{ HealhyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffPrefersMasterNodesButStillFailsOver()
		{
			var audit = new Auditor(() => Cluster
				.Nodes(new[] {
					new Node(new Uri("http://localhost:9200")) { MasterEligable = true },
					new Node(new Uri("http://localhost:9201")) { MasterEligable = true },
					new Node(new Uri("http://localhost:9202")) { MasterEligable = false },
				})
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCall(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFailure, 9200},
				{ AuditEvent.SniffFailure, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.PingSuccess, 9200},
				{ AuditEvent.HealhyResponse, 9200}
			});
		}
	}
}
