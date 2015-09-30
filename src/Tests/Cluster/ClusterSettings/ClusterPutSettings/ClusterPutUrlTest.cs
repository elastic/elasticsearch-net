using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatAliases
{
	public class ClusterPutUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await PUT("/_cluster/settings")
				.Fluent(c => c.ClusterSettings())
				.Request(c => c.ClusterSettings(new ClusterSettingsRequest()))
				.FluentAsync(c => c.ClusterSettingsAsync())
				.RequestAsync(c => c.ClusterSettingsAsync(new ClusterSettingsRequest()))
				;
		}
	}
}
