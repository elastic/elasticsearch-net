// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ClusterSettings.ClusterPutSettings
{
	public class ClusterPutUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_cluster/settings")
			.Fluent(c => c.Cluster.PutSettings(s => s))
			.Request(c => c.Cluster.PutSettings(new ClusterPutSettingsRequest()))
			.FluentAsync(c => c.Cluster.PutSettingsAsync(s => s))
			.RequestAsync(c => c.Cluster.PutSettingsAsync(new ClusterPutSettingsRequest()));
	}
}
