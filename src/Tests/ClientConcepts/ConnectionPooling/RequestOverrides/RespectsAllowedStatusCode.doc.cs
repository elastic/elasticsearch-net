using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class RespectsAllowedStatusCode
	{
		/**== Allowed status codes
		*/

		[U]
		public async Task CanOverrideBadResponse()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.FailAlways(400))
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().MaximumRetries(0))
			);

			audit = await audit.TraceCalls(
				new ClientCall() {
					{ BadResponse, 9200 }
				},
				new ClientCall(r => r.AllowedStatusCodes(400)) {
					{ HealthyResponse, 9201 }
				}
			);
		}
	}
}
