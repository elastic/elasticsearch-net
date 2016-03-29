using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class DisableSniffPingPerRequest
	{
		/**== Disabling sniffing and pinging on a request basis
		*
		* Even if you are using a sniffing connection pool thats set up to sniff on start/failure
		* and pinging enabled, you can opt out of this behaviour on a _per request_ basis.
		*
		* In our first test we set up a cluster that pings and sniffs on startup
		* but we disable the sniffing on our first request so we only see the ping and the response
		*/

		[U] public async Task DisableSniff()
		{
			/** Let's set up the cluster and configure clients to **always** sniff on startup */
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup()) // <1> sniff on startup
			);


			audit = await audit.TraceCalls(
				/** Now We disable sniffing on the request so even though it's our first call, we do not want to sniff on startup */
				new ClientCall(r => r.DisableSniffing()) // <1> disable sniffing
				{
					{ PingSuccess, 9200 }, // <2> first call is a successful ping
					{ HealthyResponse, 9200 }
				},
				/** Instead, the sniff on startup is deferred to the second call into the cluster that
				* does not disable sniffing on a per request basis
				*/
				new ClientCall()
				{
					{ SniffOnStartup }, // <3> sniff on startup call happens here, on the second call
					{ SniffSuccess, 9200 },
					{ PingSuccess, 9200 },
					{ HealthyResponse, 9200 }
				},
				/** And after that no sniff on startup will happen again */
				new ClientCall()
				{
					{ PingSuccess, 9201 },
					{ HealthyResponse, 9201 }
				}
            );
		}

		/** Now, let's disable pinging on the request */
		[U] public async Task DisablePing()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup())
			);

			audit = await audit.TraceCall(
				new ClientCall(r => r.DisablePing()) // <1> disable ping
				{
					{ SniffOnStartup },
					{ SniffSuccess, 9200 }, // <2> No ping after sniffing
					{ HealthyResponse, 9200 }
				}
            );
		}

		/** Finally, let's demonstrate disabling both sniff and ping on the request */
		[U] public async Task DisableSniffAndPing()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.SniffingConnectionPool()
				.Settings(s => s.SniffOnStartup())
			);

			audit = await audit.TraceCall(
				new ClientCall(r=>r.DisableSniffing().DisablePing()) // <1> diable ping and sniff
				{
					{ HealthyResponse, 9200 } // <2> no ping or sniff before the call
				}
            );
		}
	}
}
