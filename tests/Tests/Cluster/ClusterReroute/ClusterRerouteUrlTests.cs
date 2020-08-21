// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ClusterReroute
{
	public class ClusterRerouteUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_cluster/reroute")
			.Fluent(c => c.Cluster.Reroute(r => r))
			.Request(c => c.Cluster.Reroute(new ClusterRerouteRequest()))
			.FluentAsync(c => c.Cluster.RerouteAsync(r => r))
			.RequestAsync(c => c.Cluster.RerouteAsync(new ClusterRerouteRequest()));
	}
}
