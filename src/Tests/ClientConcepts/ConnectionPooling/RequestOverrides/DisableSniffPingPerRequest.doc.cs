using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class DisableSniffPingPerRequest
	{
		/** == Disabling sniffing and pinging on a request basis 
		*
		* Even if you are using a sniffing connection pool thats set up to sniff on start/failure
		* and pinging enabled, you can opt out of this behaviour on a per request basis
		*
		* In our first test we set up a cluster that pings and sniffs on startup 
		* but we disable the sniffing on our first request so we only see the ping and the response
		*/

		[U] public async Task DisableSniff()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup())
			);

			audit = await audit.TraceCalls(
				/**
				* We disable sniffing so eventhoug its our first call we do not want to sniff on startup
				*/
				new ClientCall(r=>r.DisableSniffing()) {
					{ PingSuccess, 9200 },
					{ HealthyResponse, 9200 }
				},
				/**
				* Instead the sniff on startup is deffered to the second call into the cluster that 
				* does not disable sniffing on a per request basis
				*/
				new ClientCall()
				{
					{ SniffOnStartup },
					{ SniffSuccess, 9200 },
					{ PingSuccess, 9200 },
					{ HealthyResponse, 9200 }
				},
				/**
				* And after that no sniff on startup will happen again
				*/
				new ClientCall()
				{ 
					{ PingSuccess, 9201 },
					{ HealthyResponse, 9201 }
				}
            );
		}

		[U] public async Task DisablePing()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup())
			);

			audit = await audit.TraceCall(
				new ClientCall(r=>r.DisablePing()) {
					{ SniffOnStartup },
					{ SniffSuccess, 9200 },
					{ HealthyResponse, 9200 }
				}
            );
		}

		[U] public async Task DisableSniffAndPing()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup())
			);

			audit = await audit.TraceCall(
				new ClientCall(r=>r.DisableSniffing().DisablePing()) {
					{ HealthyResponse, 9200 }
				}
            );
		}
	}
}
