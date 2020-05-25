// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue4197 : IClusterFixture<WritableCluster>
	{
		private readonly IElasticClient _client;

		public GithubIssue4197(WritableCluster cluster) => _client = cluster.Client;

		[I]
		public void CanDeserializeAnonymousFiltersAggregation()
		{
			const string index = "github-issue-4197";

			_client.Indices.Create(index);

			_client.Index(new Doc { ModificationDate = DateTime.Parse("2019-10-09T10:43:07.8633456+02:00") },
				i => i.Index(index).Refresh(Refresh.WaitFor));

			var searchResponse = _client.Search<Doc>(s => s
				.Index(index)
				.Aggregations(a => a
					.Filters("Modification date", f => f
						.AnonymousFilters(q => q
							.DateRange(dr => dr
								.Field(d => d.ModificationDate)
								.GreaterThan(DateMath.Now.Subtract(TimeSpan.FromDays(120)))
							)
						)
					)
				)
			);

			var filtersAggregate = searchResponse.Aggregations.Filters("Modification date");
			filtersAggregate.AnonymousBuckets().Count.Should().Be(1);
			filtersAggregate.AnonymousBuckets()[0].DocCount.Should().Be(1);
		}

		private class Doc
		{
			public DateTime ModificationDate { get; set; }
		}
	}
}
