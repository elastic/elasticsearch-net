// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net.VirtualizedCluster;
using Elasticsearch.Net.VirtualizedCluster.Audit;
using Elasticsearch.Net.VirtualizedCluster.Rules;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.ProductChecking
{
	public class ProductCheckAtStartup
	{
		/**[[product-check-at-startup]]
		* == Product check at startup
		*
		* Since v7.14.0, the client performs a required product check before the first call.
		* This pre-flight product check allows the client to establish the version of Elasticsearch that it is communicating with.
		*
		* The product check requires one additional HTTP request to be sent to the server as part of the request pipeline before
		* the main API call is sent. In most cases, this will succeed during the very first API call that the client sends.
		* Once the product check succeeds, no further product check HTTP requests are sent for subsequent API calls.
		*/
		[U] public async Task ProductCheckPerformedOnlyOnFirstCallWhenSuccessful()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(1)
				.ClientCalls(r => r.SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCalls(skipProductCheck: false,
				new ClientCall() {
					{ ProductCheckOnStartup },
					{ ProductCheckSuccess, 9200 }, // <1> as this is the first call, the product check is executed
					{ HealthyResponse, 9200 } // <2> following the product check, the actual request is sent
				},
				new ClientCall() {
					{ HealthyResponse, 9200 } // <3> subsequent calls no longer perform product check
				}
			);
		}

		[U]
		public async Task ProductCheckPerformedOnSecondCallWhenFirstCheckFails()
		{
			/** Here's an example with a single node cluster which fails for some reason during the first product check attempt. */
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(1, productCheckAlwaysSucceeds: false)
				.ProductCheck(r => r.Fails(TimesHelper.Once))
				.ProductCheck(r => r.SucceedAlways())
				.ClientCalls(r => r.SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCalls(skipProductCheck: false,
				new ClientCall() {
					{ ProductCheckOnStartup },
					{ ProductCheckFailure, 9200 }, // <1> as this is the first call, the product check is executed, but fails
					{ HealthyResponse, 9200 } // <2> the actual request is still sent and succeeds
				},
				new ClientCall() {
					{ ProductCheckOnStartup },
					{ ProductCheckSuccess, 9200 }, // <3> as the previous product check failed, it runs again on the second call
					{ HealthyResponse, 9200 }
				},
				new ClientCall() {
					{ HealthyResponse, 9200 } // <4> subsequent calls no longer perform product check
				}
			);
		}

		[U]
		public async Task ProductCheckAttemptsAllNodes()
		{
			/** Here's an example with a three node cluster which fails for some reason during the first and second product check attempts. */
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(3, productCheckAlwaysSucceeds: false)
				.ProductCheck(r => r.FailAlways())
				.ProductCheck(r => r.OnPort(9202).SucceedAlways())
				.ClientCalls(r => r.SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCalls(skipProductCheck: false,
				new ClientCall() {
					{ ProductCheckOnStartup },
					{ ProductCheckFailure, 9200 }, // <1> this is the first call, the product check is executed, but fails on this node
					{ ProductCheckFailure, 9201 }, // <2> the next node is also tried and fails
					{ ProductCheckSuccess, 9202 }, // <3> the third node is tried, successfully responds and the product check succeeds
					{ HealthyResponse, 9200 } // <4> the actual request is sent and succeeds
				},
				new ClientCall() {
					{ HealthyResponse, 9201 } // <5> subsequent calls no longer perform product check
				}
			);
		}
	}
}
