using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterPendingTasks
{
	public class ClusterPendingTasksUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cluster/pending_tasks")
			.Fluent(c => c.Cluster.PendingTasks())
			.Request(c => c.Cluster.PendingTasks(new ClusterPendingTasksRequest()))
			.FluentAsync(c => c.Cluster.PendingTasksAsync())
			.RequestAsync(c => c.Cluster.PendingTasksAsync(new ClusterPendingTasksRequest()));
	}
}
