using FluentAssertions;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue3412 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue3412(WritableCluster cluster) => _cluster = cluster;

		[I]
		public void WildcardBeingEscapedHasNoBarringsOnResult()
		{
			var indexedDocument1 = _cluster.Client.Index(new Project { Name = "project-1" }, d => d.Index("prefixed-1"));
			var indexedDocument2 = _cluster.Client.Index(new Project { Name = "project-2" }, d => d.Index("prefixed-2"));
			var refresh = _cluster.Client.Refresh("prefixed-*");
			var search = _cluster.Client.Search<Project>(s => s.Index("prefixed-*"));
			search.Total.Should().Be(2);
			var queriedIndices = search.Hits.Select(h => h.Index).ToArray();
			queriedIndices.Should().BeEquivalentTo(new[] {"prefixed-1", "prefixed-2"});
		}
	}
}
