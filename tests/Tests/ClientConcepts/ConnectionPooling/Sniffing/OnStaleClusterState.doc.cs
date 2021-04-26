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
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.VirtualizedCluster;
using Elastic.Transport.VirtualizedCluster.Audit;
using FluentAssertions;
using Tests.Framework;
using static Elastic.Transport.Diagnostics.Auditing.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Sniffing
{
	public class OnStaleClusterState
	{
		/**=== Sniffing periodically
		*
		* Connection pools that return true for `SupportsReseeding` can be configured to sniff periodically.
		* In addition to sniffing on startup and sniffing on failures, sniffing periodically can benefit scenarios where
		* clusters are often scaled horizontally during peak hours. An application might have a healthy view of a subset of the nodes,
		* but without sniffing periodically, it will never find the nodes that have been added as part of horizontal scaling,
		* to help out with load
		*/
		[U]
		[SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
		public async Task ASniffOnStartupHappens()
		{
			var audit = new Auditor(() => Virtual.Elasticsearch
				.Bootstrap(10)
				.MasterEligible(9202, 9203, 9204)
				.ClientCalls(r => r.SucceedAlways())
				.Sniff(s => s.SucceedAlways(Virtual.Elasticsearch
					.Bootstrap(100)
					.MasterEligible(9202, 9203, 9204)
					.ClientCalls(r => r.SucceedAlways())
					.Sniff(ss => ss.SucceedAlways(Virtual.Elasticsearch
						.Bootstrap(10)
						.MasterEligible(9202, 9203, 9204)
						.ClientCalls(r => r.SucceedAlways())
					))
				))
				.SniffingConnectionPool()
				.Settings(s => s
					.DisablePing()
					.SniffOnConnectionFault(false)
					.SniffOnStartup(false)
					.SniffLifeSpan(TimeSpan.FromMinutes(30))
				)
			);
			/** healthy cluster all nodes return healthy responses*/
			audit = await audit.TraceCalls(
				new ClientCall { { HealthyResponse, 9200 } },
				new ClientCall { { HealthyResponse, 9201 } },
				new ClientCall { { HealthyResponse, 9202 } },
				new ClientCall { { HealthyResponse, 9203 } },
				new ClientCall { { HealthyResponse, 9204 } },
				new ClientCall { { HealthyResponse, 9205 } },
				new ClientCall { { HealthyResponse, 9206 } },
				new ClientCall { { HealthyResponse, 9207 } },
				new ClientCall { { HealthyResponse, 9208 } },
				new ClientCall { { HealthyResponse, 9209 } },
				new ClientCall {
					{ HealthyResponse, 9200 },
					{ pool => pool.Nodes.Count.Should().Be(10) }
				}
			);
			/** Now let's forward the clock 31 minutes. Our sniff lifespan should now go stale
			* and the first call should do a sniff, which discovers we've scaled up to 100 nodes!
			*/
			audit.ChangeTime(d => d.AddMinutes(31));

			audit = await audit.TraceCalls(
				new ClientCall {
					{ SniffOnStaleCluster },
					{ SniffSuccess, 9202 },
					{ HealthyResponse, 9201 },
					{ pool => pool.Nodes.Count.Should().Be(100) }
				}
			);

			/** If we move the clock forward again by another 31 minutes, we now discover that we've scaled back
			 * down to 10 nodes
			 */
			audit.ChangeTime(d => d.AddMinutes(31));

			audit = await audit.TraceCalls(
				new ClientCall {

					{ SniffOnStaleCluster },
					{ SniffSuccess, 9202 },
					{ HealthyResponse, 9200 },
					{ pool => pool.Nodes.Count.Should().Be(10) }
				}
			);
		}

	}
}
