// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue2788 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2788(WritableCluster cluster) => _cluster = cluster;

		public void CanDeserializeNumberToTimeSpanInInnerHits()
		{
			var indexName = "sample";
			var client = _cluster.Client;

			//create index with automapping
			client.Indices.Create(indexName, create => create
				.Map<Root>(map => map
					.AutoMap()
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

		// sample mapping with nested objects with TimeSpan field
		private class Root
		{
			[Nested]
			public Child[] Children { get; set; }
		}

		private class Child
		{
			public TimeSpan EndTime { get; set; }
			public TimeSpan StartTime { get; set; }
		}
	}
}
