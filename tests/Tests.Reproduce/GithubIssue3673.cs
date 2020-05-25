// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
#pragma warning disable 612, 618
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
#pragma warning restore 612, 618

			action.Should().NotThrow();
		}
	}
}
