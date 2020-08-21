// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.TopMetrics
{
	/**
	 * The top metrics aggregation selects metrics from the document with the largest or smallest "sort" value.
	 *
	 * Top metrics is fairly similar to "top hits" in spirit but because it is more limited it is able to do its job using less memory and is often faster.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-metrics-top-metrics.html[Top Metrics Aggregation]
	 */
	[SkipVersion("<7.7.0", "Available in 7.7.0")]
	public class TopMetricsAggregationUsageTests : AggregationUsageTestBase
	{
		public TopMetricsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			tm = new
			{
				top_metrics = new
				{
					metrics = new []
					{
						new
						{
							field = "numberOfContributors"
						}
					},
					size = 10,
					sort = new[] { new { numberOfContributors = new { order = "asc" } } }
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.TopMetrics("tm", st => st
				.Metrics(m => m.Field(p => p.NumberOfContributors))
				.Size(10)
				.Sort(sort => sort
					.Ascending("numberOfContributors")
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new TopMetricsAggregation("tm")
			{
				Metrics = new List<ITopMetricsValue>
				{
					new TopMetricsValue(Field<Project>(p => p.NumberOfContributors))
				},
				Size = 10,
				Sort = new List<ISort> { new FieldSort { Field = "numberOfContributors", Order = SortOrder.Ascending } }
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var topMetrics = response.Aggregations.TopMetrics("tm");
			topMetrics.Should().NotBeNull();
			topMetrics.Top.Should().NotBeNull();
			topMetrics.Top.Count.Should().BeGreaterThan(0);

			var tipTop = topMetrics.Top.First();
			tipTop.Sort.Should().Should().NotBeNull();
			tipTop.Sort.Count.Should().BeGreaterThan(0);
			tipTop.Metrics.Should().NotBeNull();
			tipTop.Metrics.Count.Should().BeGreaterThan(0);
		}
	}
}
