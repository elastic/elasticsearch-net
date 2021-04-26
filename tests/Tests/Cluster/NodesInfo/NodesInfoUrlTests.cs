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

namespace Tests.Cluster.NodesInfo
{
	public class NodesInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_nodes")
					.Fluent(c => c.Nodes.Info())
					.Request(c => c.Nodes.Info(new NodesInfoRequest()))
					.FluentAsync(c => c.Nodes.InfoAsync())
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest()))
				;

			await GET("/_nodes/foo")
					.Fluent(c => c.Nodes.Info(n => n.NodeId("foo")))
					.Request(c => c.Nodes.Info(new NodesInfoRequest("foo")))
					.FluentAsync(c => c.Nodes.InfoAsync(n => n.NodeId("foo")))
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest("foo")))
				;

			var metrics = NodesInfoMetric.Http | NodesInfoMetric.Jvm;
			await GET("/_nodes/jvm%2Chttp")
					.Fluent(c => c.Nodes.Info(p => p.Metric(metrics)))
					.Request(c => c.Nodes.Info(new NodesInfoRequest(metrics)))
					.FluentAsync(c => c.Nodes.InfoAsync(p => p.Metric(metrics)))
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest(metrics)))
				;

			await GET("/_nodes/foo/jvm%2Chttp")
					.Fluent(c => c.Nodes.Info(n => n.NodeId("foo").Metric(metrics)))
					.Request(c => c.Nodes.Info(new NodesInfoRequest("foo", metrics)))
					.FluentAsync(c => c.Nodes.InfoAsync(n => n.NodeId("foo").Metric(metrics)))
					.RequestAsync(c => c.Nodes.InfoAsync(new NodesInfoRequest("foo", metrics)))
				;
		}
	}
}
