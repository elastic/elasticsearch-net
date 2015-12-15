using System.Threading.Tasks;
using Nest;
using Tests.Framework;
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
