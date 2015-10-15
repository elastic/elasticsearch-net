using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterPendingTasks
{
	public class ClusterPendingTasksUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/pending_tasks")
				.Fluent(c => c.ClusterPendingTasks())
				.Request(c => c.ClusterPendingTasks(new ClusterPendingTasksRequest()))
				.FluentAsync(c => c.ClusterPendingTasksAsync())
				.RequestAsync(c => c.ClusterPendingTasksAsync(new ClusterPendingTasksRequest()))
				;
		}
	}
}
