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
	public class Revival
	{
		/** == Pinging
		* 
		* When a node is marked dead it will only be put in the dog house for a certain amount of time. Once it comes out of the dog house, or revivided, we schedule a ping 
		* before the actual call to make sure its up and running. If its still down we put it back in the dog house a little longer. For an explanation on these timeouts see: TODO LINK
		*/

		[U, SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task PingAfterRevival()
		{
			var audit = new Auditor(() => Cluster
				.Nodes(3)
				.ClientCalls(r=>r.SucceedAlways())
				.ClientCalls(r=>r.OnPort(9202).Fails(Once))
				.Ping(p => p.SucceedAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			await audit.TraceCalls(
				new CallTrace { { PingSuccess, 9200 }, { HealhyResponse, 9200 } },
				new CallTrace { { PingSuccess, 9201 }, { HealhyResponse, 9201 } },
				new CallTrace { 
					{ PingSuccess, 9202},
					{ BadResponse, 9202},
					{ HealhyResponse, 9200},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(1) }
				},
				new CallTrace { { HealhyResponse, 9201 } },
				new CallTrace { { HealhyResponse, 9200 } },
				new CallTrace { { HealhyResponse, 9201 } },
				new CallTrace { { HealhyResponse, 9200 } }
			);
		}
	}
}
