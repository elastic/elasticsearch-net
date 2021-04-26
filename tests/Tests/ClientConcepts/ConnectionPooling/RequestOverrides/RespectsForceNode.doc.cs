/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.VirtualizedCluster;
using Elasticsearch.Net.VirtualizedCluster.Audit;
using Tests.Framework;
using static Elasticsearch.Net.AuditEvent;

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
			var audit = new Auditor(() => VirtualClusterWith
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
