using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterSettings.ClusterPutSettings
{
	public class ClusterPutUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_cluster/settings")
				.Fluent(c => c.ClusterPutSettings(s=>s))
				.Request(c => c.ClusterPutSettings(new ClusterPutSettingsRequest()))
				.FluentAsync(c => c.ClusterPutSettingsAsync(s=>s))
				.RequestAsync(c => c.ClusterPutSettingsAsync(new ClusterPutSettingsRequest()))
				;
		}
	}
}
