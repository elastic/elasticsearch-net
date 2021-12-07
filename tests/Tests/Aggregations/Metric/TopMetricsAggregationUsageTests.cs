// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;

namespace Tests.Aggregations.Metric;

public class TopMetricsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public TopMetricsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		tm = new
		{
			top_metrics = new
			{
				metrics = new[]
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

	protected override Action<AggregationContainerDescriptor<Project>> FluentAggs => a => a
		.TopMetrics("tm", st => st
			.Metrics(m => m.Field(p => p.NumberOfContributors))
			.Size(10)
			.Sort(new Sort { new FieldSort { Field = "numberOfContributors", Order = SortOrder.Asc } })
			//.Sort(sort => sort
			//	.Asc("numberOfContributors")
			//)
		);

	protected override AggregationDictionary InitializerAggs =>
		new TopMetricsAggregation("tm")
		{
			//Metrics = new List<ITopMetricsValue>
			//{
			//	new TopMetricsValue(Field<Project>(p => p.NumberOfContributors))
			//},
			Metrics = new TopMetricsValue()
			{
				Field = Field<Project>(p => p.NumberOfContributors)
			},
			Size = 10,
			Sort = new Sort { new FieldSort { Field = "numberOfContributors", Order = SortOrder.Asc } }
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var topMetrics = response.Aggregations.TopMetrics("tm");
		topMetrics.Should().NotBeNull();
		//topMetrics.Top.Should().NotBeNull();
		//topMetrics.Top.Count.Should().BeGreaterThan(0);

		//var tipTop = topMetrics.Top.First();
		//tipTop.Sort.Should().Should().NotBeNull();
		//tipTop.Sort.Count.Should().BeGreaterThan(0);
		//tipTop.Metrics.Should().NotBeNull();
		//tipTop.Metrics.Count.Should().BeGreaterThan(0);
	}
}
