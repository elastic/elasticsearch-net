// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ClusterSettings.ClusterGetSettings
{
	public class ClusterGetSettingsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cluster/settings")
			.Fluent(c => c.Cluster.GetSettings())
			.Request(c => c.Cluster.GetSettings(new ClusterGetSettingsRequest()))
			.FluentAsync(c => c.Cluster.GetSettingsAsync())
			.RequestAsync(c => c.Cluster.GetSettingsAsync(new ClusterGetSettingsRequest()));
	}
}
