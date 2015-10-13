using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterSettings.ClusterGetSettings
{
	public class ClusterGetSettingsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/settings")
				.Fluent(c => c.ClusterGetSettings())
				.Request(c => c.ClusterGetSettings(new ClusterGetSettingsRequest()))
				.FluentAsync(c => c.ClusterGetSettingsAsync())
				.RequestAsync(c => c.ClusterGetSettingsAsync(new ClusterGetSettingsRequest()))
				;
		}
	}
}
