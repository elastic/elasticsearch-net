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
	public class ClusterStateUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/state")
				.Fluent(c => c.ClusterState())
				.Request(c => c.ClusterState(new ClusterStateRequest()))
				.FluentAsync(c => c.ClusterStateAsync())
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest()))
				;

			await GET("/_cluster/state/{metric}")
				.Fluent(c => c.ClusterState())
				.Request(c => c.ClusterState(new ClusterStateRequest()))
				.FluentAsync(c => c.ClusterStateAsync())
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest()))
				;

			await GET("/_cluster/state/{metric}/{index}")
				.Fluent(c => c.ClusterState())
				.Request(c => c.ClusterState(new ClusterStateRequest()))
				.FluentAsync(c => c.ClusterStateAsync())
				.RequestAsync(c => c.ClusterStateAsync(new ClusterStateRequest()))
				;
		}
	}
}
