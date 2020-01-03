using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Cluster.ClusterAllocationExplain
{
	public class ClusterAllocationExplainUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("/_cluster/allocation/explain?include_yes_decisions=true")
			.Fluent(c => c.Cluster.AllocationExplain(s => s.Index<Project>().Shard(0).Primary().IncludeYesDecisions()))
			.Request(c => c.Cluster.AllocationExplain(new ClusterAllocationExplainRequest
				{ Index = typeof(Project), Shard = 0, Primary = true, IncludeYesDecisions = true }))
			.FluentAsync(c => c.Cluster.AllocationExplainAsync(s => s.Index<Project>().Shard(0).Primary().IncludeYesDecisions()))
			.RequestAsync(c => c.Cluster.AllocationExplainAsync(new ClusterAllocationExplainRequest
				{ Index = typeof(Project), Shard = 0, Primary = true, IncludeYesDecisions = true }));
	}
}
