using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class RespectsForceNode
	{
		/**== Forcing nodes
		* Sometimes you might want to fire a single request to a specific node. You can do so using the `ForceNode`
		* request configuration. This will ignore the pool and not retry.
		*/

		[U]
		public async Task OnlyCallsForcedNode()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.ClientCalls(r => r.OnPort(9208).FailAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall(r => r.ForceNode(new Uri("http://localhost:9208"))) {
					{ BadResponse, 9208 }
				}
			);
		}
	}
}
