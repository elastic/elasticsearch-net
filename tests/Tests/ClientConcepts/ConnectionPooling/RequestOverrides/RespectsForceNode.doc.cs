// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.VirtualizedCluster;
using Elastic.Transport.VirtualizedCluster.Audit;
using Tests.Framework;
using static Elastic.Transport.Diagnostics.Auditing.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class RespectsForceNode
	{
		/**=== Forcing nodes
		* Sometimes you might want to fire a single request to a specific node. You can do so using the `ForceNode`
		* request configuration. This will ignore the pool and not retry.
		*/

		[U]
		public async Task OnlyCallsForcedNode()
		{
			var audit = new Auditor(() => Virtual.Elasticsearch
				.Bootstrap(10)
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
