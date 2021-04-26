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
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ClusterState
{
	public class ClusterStateUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cluster/state")
					.Fluent(c => c.Cluster.State())
					.Request(c => c.Cluster.State(new ClusterStateRequest()))
					.FluentAsync(c => c.Cluster.StateAsync())
					.RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest()))

				;

			var metrics = ClusterStateMetric.MasterNode | ClusterStateMetric.Metadata;
			await GET("/_cluster/state/metadata%2Cmaster_node")
					.Fluent(c => c.Cluster.State(null, p => p.Metric(metrics)))
					.Request(c => c.Cluster.State(new ClusterStateRequest(metrics)))
					.FluentAsync(c => c.Cluster.StateAsync(null, p => p.Metric(metrics)))
					.RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest(metrics)))
				;

			metrics |= ClusterStateMetric.All;
			var index = "indexx";
			await GET($"/_cluster/state/_all/{index}")
					.Fluent(c => c.Cluster.State(index, p => p.Metric(metrics)))
					.Request(c => c.Cluster.State(new ClusterStateRequest(metrics, index)))
					.FluentAsync(c => c.Cluster.StateAsync(index, p => p.Metric(metrics)))
					.RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest(metrics, index)))
				;
		}
	}
}
