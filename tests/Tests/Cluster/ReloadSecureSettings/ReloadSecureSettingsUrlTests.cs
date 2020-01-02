using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
