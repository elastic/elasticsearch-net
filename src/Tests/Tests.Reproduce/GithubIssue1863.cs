using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	[SkipVersion("<2.1.0", "")]
	public class GithubIssue1863 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;
		public GithubIssue1863(ReadOnlyCluster cluster) { _cluster = cluster; }

		[I]
		public void ConcreteTypeConverterReturnsNullScores()
		{
			var client = _cluster.Client;
			var response = client.Search<Project>(s => s
				.ConcreteTypeSelector((d,h) => typeof(Project))
				.Sort(srt => srt.Ascending(p => p.StartedOn))
			);
			response.Hits.Count().Should().BeGreaterThan(0);
			response.Hits.All(h => h.Score.HasValue).Should().BeFalse();
		}
	}
}
