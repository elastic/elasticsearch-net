// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.ReloadSecureSettings
{
	public class ReloadSecureSettingsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_nodes/reload_secure_settings")
				.Fluent(c => c.Nodes.ReloadSecureSettings())
				.Request(c => c.Nodes.ReloadSecureSettings(new ReloadSecureSettingsRequest()))
				.FluentAsync(c => c.Nodes.ReloadSecureSettingsAsync())
				.RequestAsync(c => c.Nodes.ReloadSecureSettingsAsync(new ReloadSecureSettingsRequest()));

			await POST("/_nodes/foo/reload_secure_settings")
				.Fluent(c => c.Nodes.ReloadSecureSettings(n => n.NodeId("foo")))
				.Request(c => c.Nodes.ReloadSecureSettings(new ReloadSecureSettingsRequest("foo")))
				.FluentAsync(c => c.Nodes.ReloadSecureSettingsAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.Nodes.ReloadSecureSettingsAsync(new ReloadSecureSettingsRequest("foo")));
		}
	}
}
