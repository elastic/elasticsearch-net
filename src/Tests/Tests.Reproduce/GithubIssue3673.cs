using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue3673 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue3673(ReadOnlyCluster cluster) => _cluster = cluster;

		[I]
		public void DeserializeDateAggregation()
		{
			Action action = () => _cluster.Client.Search<Project>(s => s
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("publication_year", st => st
						.Field(o => o.StartedOn)
						.Interval(DateInterval.Year)
						.Format("yyyy")
						.MinimumDocumentCount(0)
					)
				)
			);

			action.Should().NotThrow();
		}
	}
}
