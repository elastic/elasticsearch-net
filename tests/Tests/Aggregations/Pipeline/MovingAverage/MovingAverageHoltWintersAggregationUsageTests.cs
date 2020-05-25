// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Pipeline.MovingAverage
{
	[SkipVersion("5.0.0-alpha1", "https://github.com/elastic/elasticsearch/issues/17516")]
	public class MovingAverageHoltWintersUsageTests : AggregationUsageTestBase
	{
		public MovingAverageHoltWintersUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				date_histogram = new
				{
					field = "startedOn",
					interval = "month",
					min_doc_count = 0
				},
				aggs = new
				{
					commits = new
					{
						sum = new
						{
							field = "numberOfCommits"
						}
					},
					commits_moving_avg = new
					{
						moving_avg = new
						{
							buckets_path = "commits",
							window = 4,
							model = "holt_winters",
							settings = new
							{
								type = "mult",
								alpha = 0.5,
								beta = 0.5,
								gamma = 0.5,
								period = 2,
								pad = false
							}
						}
					}
				}
			}
		};

#pragma warning disable 618, 612
		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_month", dh => dh
				.Field(p => p.StartedOn)
				.Interval(DateInterval.Month)
				.MinimumDocumentCount(0)
				.Aggregations(aa => aa
					.Sum("commits", sm => sm
						.Field(p => p.NumberOfCommits)
					)
					.MovingAverage("commits_moving_avg", mv => mv
						.BucketsPath("commits")
						.Window(4)
						.Model(m => m
							.HoltWinters(hw => hw
								.Type(HoltWintersType.Multiplicative)
								.Alpha(0.5f)
								.Beta(0.5f)
								.Gamma(0.5f)
								.Period(2)
								.Pad(false)
							)
						)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				MinimumDocumentCount = 0,
				Aggregations =
					new SumAggregation("commits", "numberOfCommits")
					&& new MovingAverageAggregation("commits_moving_avg", "commits")
					{
						Window = 4,
						Model = new HoltWintersModel
						{
							Type = HoltWintersType.Multiplicative,
							Alpha = 0.5f,
							Beta = 0.5f,
							Gamma = 0.5f,
							Period = 2,
							Pad = false
						}
					}
			};
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var bucketCount = 0;
			foreach (var item in projectsPerMonth.Buckets)
			{
				bucketCount++;

				var commits = item.Sum("commits");
				commits.Should().NotBeNull();
				commits.Value.Should().BeGreaterThan(0);

				var movingAverage = item.MovingAverage("commits_moving_avg");

				// Moving Average specifies a window of 4 so
				// moving average values should exist from 5th bucket onwards
				if (bucketCount <= 4)
					movingAverage.Should().BeNull();
				else
				{
					movingAverage.Should().NotBeNull();
					movingAverage.Value.Should().HaveValue();
				}
			}
		}
	}
}
