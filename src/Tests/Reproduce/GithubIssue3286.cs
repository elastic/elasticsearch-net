using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3286 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue3286(WritableCluster cluster)
		{
			_cluster = cluster;
		}

		[I]
		public void AggregationsShouldNotBeNullOnNestedAggregation()
		{
			var client = _cluster.Client;
			var index = Guid.NewGuid().ToString("N").Substring(0, 8);

			if (client.IndexExists(index).Exists)
				client.DeleteIndex(index);

			client.CreateIndex(index, c => c
				.Mappings(m => m
					.Map<MyDocument>(mm => mm
						.AutoMap()
						.Properties(p => p
							.Nested<Rate>(n => n
								.AutoMap()
								.Name(nn => nn.Rates)
							)
						)
					)
				)
			);

			client.Bulk(b => b
				.Index(index)
				.IndexMany(new[]
				{
					new MyDocument
					{
						Name = "doc 1",
						Rates = new[]
						{
							new Rate
							{
								Start = new DateTime(2018, 6, 9),
								End = new DateTime(2018, 6, 16),
								WeeklyRate = 100
							},
							new Rate
							{
								Start = new DateTime(2018, 6, 16),
								End = new DateTime(2018, 6, 23),
								WeeklyRate = 200
							}
						}
					},
					new MyDocument
					{
						Name = "doc 2",
						Rates = new[]
						{
							new Rate
							{
								Start = new DateTime(2018, 6, 9),
								End = new DateTime(2018, 6, 16),
								WeeklyRate = 120
							},
							new Rate
							{
								Start = new DateTime(2018, 6, 16),
								End = new DateTime(2018, 6, 23),
								WeeklyRate = 250
							}
						}
					}
				})
				.Refresh(Refresh.WaitFor)
			);

			var searchResponse = client.Search<MyDocument>(s => s
				.Index(index)
				.Size(0)
				.Aggregations(a => a
					.Nested("nested_start_dates", n => n
						.Path(f => f.Rates)
						.Aggregations(aa => aa
							.DateHistogram("start_dates", dh => dh
								.Field(f => f.Rates.First().Start)
								.Interval(DateInterval.Day)
								.MinimumDocumentCount(1)
								.Aggregations(aaa => aaa
									.Min("min_rate", m => m
										.Field(f => f.Rates.First().WeeklyRate)
									)
									.Max("max_rate", m => m
										.Field(f => f.Rates.First().WeeklyRate)
									)
								)
							)
						)
					)
				)
			);

			searchResponse.IsValid.Should().BeTrue();

			var nested = searchResponse.Aggregations.Nested("nested_start_dates");

#pragma warning disable 618
			nested.Aggregations.Should().NotBeNull();
			nested.Should().BeSameAs(nested.Aggregations);
#pragma warning restore 618
		}
	}

	public class MyDocument
	{
		public string Name { get; set; }
		public IEnumerable<Rate> Rates { get; set; }
	}

	public class Rate
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public double WeeklyRate { get; set; }
	}
}
