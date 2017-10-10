using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using FluentAssertions.Common;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Reproduce
{
	public class GithubIssue2788 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2788(WritableCluster cluster)
		{
			_cluster = cluster;
		}

		// sample mapping with nested objects with TimeSpan field
		class Root
		{
			[Nested]
			public Child[] Children { get; set; }
		}

		class Child
		{
			public TimeSpan StartTime { get; set; }

			public TimeSpan EndTime { get; set; }
		}

		[I]
		public void CanDeserializeNumberToTimeSpanInInnerHits()
		{
			var indexName = "sample";
			var client = _cluster.Client;

			//create index with automapping
			client.CreateIndex(indexName, create => create
				.Mappings(mappings => mappings
					.Map<Root>(map => map
						.AutoMap()
					)
				)
			);

			var startTime = new TimeSpan(1, 2, 3);
			var endTime = new TimeSpan(2, 3, 4);

			client.Index(new Root
			{
				Children = new[]
				{
					new Child
					{
						StartTime = startTime,
						EndTime = endTime

					}
				}
			}, index => index
				.Index(indexName)
				.Refresh(Refresh.WaitFor)
			);

			var result = client.Search<Root>(search => search
				.Query(query => query
					.Nested(nested => nested
						.Query(nestedQuery => nestedQuery
							.MatchAll()
						)
						.Path(i => i.Children)
						.InnerHits()
					)
				)
				.Index(indexName)
			);

			var child = result.Hits.First().InnerHits.Single().Value.Documents<Child>().Single();

			child.Should().NotBeNull();
			child.StartTime.Should().Be(startTime);
			child.EndTime.Should().Be(endTime);
		}
	}
}
