// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.VirtualizedCluster;
using Elasticsearch.Net.VirtualizedCluster.Audit;
using Tests.Framework;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.RequestOverrides
{
	public class RespectsAllowedStatusCode
	{
		/**=== Allowed status codes
		*/

		[U]
		public async Task CanOverrideBadResponse()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.ClientCalls(r => r.FailAlways(400))
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().MaximumRetries(0))
			);

			audit = await audit.TraceCalls(
				new ClientCall {
					{ BadResponse, 9200 }
				},
				new ClientCall(r => r.AllowedStatusCodes(400)) {
					{ HealthyResponse, 9201 }
				}
			);
		}
	}
}
