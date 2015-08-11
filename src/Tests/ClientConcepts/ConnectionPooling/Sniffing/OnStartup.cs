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

namespace Tests.ClientConcepts.LowLevel
{
	public class OnStartupSniffing
	{
		/** == Sniffing on startup
		* 
		* Connection pools that return true for `SupportsReseeding` by default sniff on startup.
		*/

		[U]
		public async Task ASniffOnStartupHappens()
		{
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways())
				.SniffingConnectionPool()
				.AllDefaults();

			 await virtualWorld.Sees(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFail, 9200},
				{ AuditEvent.SniffFail, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.Ping, 9200},
				{ AuditEvent.HealhyResponse, 9200}
			});
		}

		[U]
		public async Task SniffOnStartUpTakesNewClusterState()
		{
			var virtualWorld = new AuditTrailTester();
			virtualWorld.Cluster = () => Cluster
				.Nodes(10)
				.Sniff(s => s.FailAlways())
				.Sniff(s => s.OnPort(9202).SucceedAlways(Cluster.Nodes(8, startFrom: 9202)))
				.SniffingConnectionPool()
				.AllDefaults();

			 await virtualWorld.Sees(new Audits {
				{ AuditEvent.SniffOnStartup},
				{ AuditEvent.SniffFail, 9200},
				{ AuditEvent.SniffFail, 9201},
				{ AuditEvent.SniffSuccess, 9202},
				{ AuditEvent.Ping, 9202},
				{ AuditEvent.HealhyResponse, 9202}
			});
		}

	}
}
