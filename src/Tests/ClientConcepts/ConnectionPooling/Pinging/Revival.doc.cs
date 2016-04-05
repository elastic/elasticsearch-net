using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Pinging
{
	public class Revival
	{
		/**== Pinging - Revival
		*
		* When a node is marked dead it will only be put in the dog house for a certain amount of time. Once it comes out of the dog house, or revived, we schedule a ping
		* before the actual call to make sure its up and running. If its still down we put it _back in the dog house_ a little longer.
		* Take a look at the <<request-timeout, Connecting documentation>> for an explanation on these timeouts.
		*/
		[U]
		public async Task PingAfterRevival()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(3)
				.ClientCalls(r => r.SucceedAlways())
				.ClientCalls(r => r.OnPort(9202).Fails(Once))
				.Ping(p => p.SucceedAlways())
				.StaticConnectionPool()
				.AllDefaults()
			);

			audit = await audit.TraceCalls(
				new ClientCall { { PingSuccess, 9200 }, { HealthyResponse, 9200 } },
				new ClientCall { { PingSuccess, 9201 }, { HealthyResponse, 9201 } },
				new ClientCall {
					{ PingSuccess, 9202},
					{ BadResponse, 9202},
					{ HealthyResponse, 9200},
					{ pool =>  pool.Nodes.Where(n=>!n.IsAlive).Should().HaveCount(1) }
				},
				new ClientCall { { HealthyResponse, 9201 } },
				new ClientCall { { HealthyResponse, 9200 } },
				new ClientCall { { HealthyResponse, 9201 } },
				new ClientCall {
					{ HealthyResponse, 9200 },
					{ pool => pool.Nodes.First(n=>!n.IsAlive).DeadUntil.Should().BeAfter(DateTime.UtcNow) }
				}
			);

			audit = await audit.TraceCalls(
				new ClientCall { { HealthyResponse, 9201 } },
				new ClientCall { { HealthyResponse, 9200 } },
				new ClientCall { { HealthyResponse, 9201 } }
			);

			audit.ChangeTime(d => d.AddMinutes(20));

			audit = await audit.TraceCalls(
				new ClientCall { { HealthyResponse, 9201 } },
				new ClientCall {
					{ Resurrection, 9202 },
					{ PingSuccess, 9202 },
					{ HealthyResponse, 9202 }
				}
			);
		}
	}
}
