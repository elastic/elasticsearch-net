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

 using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.VirtualizedCluster;
using Elasticsearch.Net.VirtualizedCluster.Audit;

namespace Tests.ClientConcepts.ConnectionPooling.Failover
{
	public class FallingOver
	{
		/**[[fail-over]]
		* === Fail over
		* When using a connection pool with more than one node, a request will be retried if
		* the call to a node throws an exception or returns a 502, 503 or 504 response
		*/
		[U]
		public async Task ExceptionFallsOverToNextNode()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.ClientCalls(r => r.FailAlways())
				.ClientCalls(r => r.OnPort(9201).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.HealthyResponse, 9201 },
				}
			);
		}

		/**[[bad-gateway]]
		*==== 502 Bad Gateway
		*
		* Will be treated as an error that requires retrying
		*/
		[U]
		public async Task Http502FallsOver()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.ClientCalls(r => r.FailAlways(502))
				.ClientCalls(r => r.OnPort(9201).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.HealthyResponse, 9201 },
				}
			);
		}

		/**[[service-unavailable]]
		*==== 503 Service Unavailable
		*
		* Will be treated as an error that requires retrying
		*/
		[U]
		public async Task Http503FallsOver()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.ClientCalls(r => r.FailAlways(503))
				.ClientCalls(r => r.OnPort(9201).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.HealthyResponse, 9201 },
				}
			);
		}

		/**[[gateway-timeout]]
		*==== 504 Gateway Timeout
		*
		* Will be treated as an error that requires retrying
		*/
		[U]
		public async Task Http504FallsOver()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.ClientCalls(r => r.FailAlways(504))
				.ClientCalls(r => r.OnPort(9201).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.HealthyResponse, 9201 },
				}
			);
		}

		/**
		* If a call returns a __valid__ HTTP status code other than 502 or 503, the request won't be retried.
		*
		* IMPORTANT: Different requests may have different status codes that are deemed __valid__. For example,
		* a *404 Not Found* response is a __valid__ status code for an index exists request
		*/
		[U]
		public async Task HttpTeapotDoesNotFallOver()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.ClientCalls(r => r.FailAlways(418))
				.ClientCalls(r => r.OnPort(9201).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
				}
			);
		}
	}
}
