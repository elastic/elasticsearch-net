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
using Elastic.Transport;
using Elastic.Transport.VirtualizedCluster;
using Elastic.Transport.VirtualizedCluster.Audit;
using Tests.Framework;
using static Elastic.Transport.Diagnostics.Auditing.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class RespectsMaxRetryOverrides
	{
		/**=== Maximum retries per request
		*
		* By default retry as many times as we have nodes. However retries still respect the request timeout.
		* Meaning if you have a 100 node cluster and a request timeout of 20 seconds we will retry as many times as we can
		* but give up after 20 seconds
		*/

		[U]
		public async Task DefaultMaxIsNumberOfNodes()
		{
			var audit = new Auditor(() => Virtual.Elasticsearch
				.Bootstrap(10)
				.ClientCalls(r => r.FailAlways())
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall(r => r.MaxRetries(2)) {
					{ BadResponse, 9200 },
					{ BadResponse, 9201 },
					{ BadResponse, 9202 },
					{ MaxRetriesReached }
				}
			);
		}

		/**
		* When you have a 100 node cluster you might want to ensure a fixed number of retries.
		* Remember that the actual number of requests is initial attempt + set number of retries
		*/

		[U]
		public async Task FixedMaximumNumberOfRetries()
		{
			var audit = new Auditor(() => Virtual.Elasticsearch
				.Bootstrap(10)
				.ClientCalls(r => r.FailAlways())
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().MaximumRetries(5))
			);

			audit = await audit.TraceCall(
				new ClientCall(r => r.MaxRetries(2)) {
					{ BadResponse, 9200 },
					{ BadResponse, 9201 },
					{ BadResponse, 9202 },
					{ MaxRetriesReached }
				}
			);
		}

		/**
		* This makes setting any retry setting on a single node connection pool a NOOP, this is by design!
		* Connection pooling and connection failover is about trying to fail sanely whilst still utilizing available resources and
		* not giving up on the fail fast principle. It's *NOT* a mechanism for forcing requests to succeed.
		*/
		[U]
		public async Task DoesNotRetryOnSingleNodeConnectionPool()
		{
			var audit = new Auditor(() => Virtual.Elasticsearch
				.Bootstrap(10)
				.ClientCalls(r => r.FailAlways().Takes(TimeSpan.FromSeconds(3)))
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.SingleNodeConnection()
				.Settings(s => s.DisablePing().MaximumRetries(10))
			);

			audit = await audit.TraceCall(
				new ClientCall(r => r.MaxRetries(10)) {
					{ BadResponse, 9200 }
				}
			);

		}
	}
}
