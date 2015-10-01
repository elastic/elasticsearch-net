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
	public class ClusterHealthUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/health")
				.Fluent(c => c.ClusterHealth())
				.Request(c => c.ClusterHealth(new ClusterHealthRequest()))
				.FluentAsync(c => c.ClusterHealthAsync())
				.RequestAsync(c => c.ClusterHealthAsync(new ClusterHealthRequest()))
				;

			await GET("/_cluster/health/project")
				.Fluent(c => c.ClusterHealth(h => h.Index<Project>()))
				.Request(c => c.ClusterHealth(new ClusterHealthRequest(typeof(Project))))
				.FluentAsync(c => c.ClusterHealthAsync(h => h.Index<Project>()))
				.RequestAsync(c => c.ClusterHealthAsync(new ClusterHealthRequest(typeof(Project))))
				;
		}
	}
}
