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
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults();

			 await virtualWorld.TraceCall(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFail, 9200},
				{ AuditEvent.SniffFail, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.Ping, 9200},
				{ AuditEvent.HealhyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffOnStartUpTakesNewClusterState()
		{
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways(Cluster.Nodes(8, startFrom: 9204)))
				.SniffingConnectionPool()
				.AllDefaults();

			await virtualWorld.TraceCall(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFail, 9200},
				{ AuditEvent.SniffFail, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.Ping, 9204},
				{ AuditEvent.HealhyResponse, 9204}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffTriesAllNodes()
		{
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9209).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults();

			await virtualWorld.TraceCall(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFail, 9200},
				{ AuditEvent.SniffFail, 9201},
				{ AuditEvent.SniffFail, 9202},
				{ AuditEvent.SniffFail, 9203},
				{ AuditEvent.SniffFail, 9204},
				{ AuditEvent.SniffFail, 9205},
				{ AuditEvent.SniffFail, 9206},
				{ AuditEvent.SniffFail, 9207},
				{ AuditEvent.SniffFail, 9208},
				{ AuditEvent.SniffSuccess, 9209},
				{ AuditEvent.Ping, 9200},
				{ AuditEvent.HealhyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffPrefersMasterNodes()
		{
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(new[] {
					new Node(new Uri("http://localhost:9200")) { MasterEligable = false },
					new Node(new Uri("http://localhost:9201")) { MasterEligable = false },
					new Node(new Uri("http://localhost:9202")) { MasterEligable = true },
				})
				.Sniff(s => s.SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults();

			await virtualWorld.TraceCall(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.Ping, 9200},
				{ AuditEvent.HealhyResponse, 9200}
			});
		}

		[U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task SniffPrefersMasterNodesButStillFailsOver()
		{
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(new[] {
					new Node(new Uri("http://localhost:9200")) { MasterEligable = true },
					new Node(new Uri("http://localhost:9201")) { MasterEligable = true },
					new Node(new Uri("http://localhost:9202")) { MasterEligable = false },
				})
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults();

			await virtualWorld.TraceCall(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFail, 9200},
				{ AuditEvent.SniffFail, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.Ping, 9200},
				{ AuditEvent.HealhyResponse, 9200}
			});
		}
	}
}
