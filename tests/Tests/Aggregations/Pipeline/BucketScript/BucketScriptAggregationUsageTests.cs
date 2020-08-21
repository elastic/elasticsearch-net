// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Pipeline.BucketScript
{
	public class BucketScriptAggregationUsageTests : AggregationUsageTestBase
	{
		public BucketScriptAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				date_histogram = new
				{
					field = "startedOn",
					interval = "month",
					min_doc_count = 1
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
					stable_state = new
					{
						filter = new
						{
							term = new
							{
								state = new
								{
									value = "Stable"
								}
							}
						},
						aggs = new
						{
							commits = new
							{
								sum = new
								{
									field = "numberOfCommits"
								}
							}
						}
					},
					stable_percentage = new
					{
						bucket_script = new
						{
							buckets_path = new
							{
								totalCommits = "commits",
								stableCommits = "stable_state>commits"
							},
							script = new
							{
								source = "params.stableCommits / params.totalCommits * 100",
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
				.MinimumDocumentCount(1)
				.Aggregations(aa => aa
					.Sum("commits", sm => sm
						.Field(p => p.NumberOfCommits)
					)
					.Filter("stable_state", f => f
						.Filter(ff => ff
							.Term(p => p.State, "Stable")
						)
						.Aggregations(aaa => aaa
							.Sum("commits", sm => sm
								.Field(p => p.NumberOfCommits)
							)
						)
					)
					.BucketScript("stable_percentage", bs => bs
						.BucketsPath(bp => bp
							.Add("totalCommits", "commits")
							.Add("stableCommits", "stable_state>commits")
						)
						.Script(ss => ss.Source("params.stableCommits / params.totalCommits * 100"))
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				MinimumDocumentCount = 1,
				Aggregations =
					new SumAggregation("commits", "numberOfCommits") &&
					new FilterAggregation("stable_state")
					{
						Filter = new TermQuery
						{
							Field = "state",
							Value = "Stable"
						},
						Aggregations = new SumAggregation("commits", "numberOfCommits")
					}
					&& new BucketScriptAggregation("stable_percentage", new MultiBucketsPath
					{
						{ "totalCommits", "commits" },
						{ "stableCommits", "stable_state>commits" }
					})
					{
						Script = new InlineScript("params.stableCommits / params.totalCommits * 100")
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

			foreach (var item in projectsPerMonth.Buckets)
			{
				var stablePercentage = item.BucketScript("stable_percentage");
				stablePercentage.Should().NotBeNull();
				stablePercentage.Value.Should().HaveValue();
			}
		}
	}
}
