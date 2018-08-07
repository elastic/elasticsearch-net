using System;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Pinging
{
	public class Revival
	{
		/**=== Ping on revival
		*
		* When a node is marked dead it will only be __put in the dog house__ for a certain amount of time.
        * Once it __comes out of the dog house__, or revived, a ping is scheduled before an actual API call, to ensure
        * that it's up and running. If it's still down, it's put _back in the dog house_ a little longer.
		*
		* Take a look at the <<request-timeout, Request timeouts>> for an explanation on what each timeout is.
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
