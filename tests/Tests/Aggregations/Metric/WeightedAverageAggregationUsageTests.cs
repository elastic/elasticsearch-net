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

public class WeightedAverageAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public WeightedAverageAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		weighted_avg_commits = new
		{
			weighted_avg = new
			{
				value = new
				{
					field = "numberOfCommits",
					missing = 0.0
				},
				weight = new
				{
					script = new
					{
						source = "(doc['numberOfContributors']?.value ?: 0) + 1"
					}
				},
				value_type = "long"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.WeightedAvg("weighted_avg_commits", avg => avg
			.Value(v => v.Field(p => p.NumberOfCommits).Missing(0))
			.Weight(s => s.Script(new Script(new InlineScript("(doc['numberOfContributors']?.value ?: 0) + 1"))))
			.ValueType(Elastic.Clients.Elasticsearch.Aggregations.ValueType.Long)
		);

	protected override AggregationDictionary InitializerAggs =>
		new WeightedAverageAggregation("weighted_avg_commits")
		{
			Value = new WeightedAverageValue()
			{
				Missing = 0,
				Field = Field<Project>(p => p.NumberOfCommits)
			},
			Weight = new WeightedAverageValue()
			{
				Script = new Script(new InlineScript("(doc['numberOfContributors']?.value ?: 0) + 1"))
			},
			ValueType = Elastic.Clients.Elasticsearch.Aggregations.ValueType.Long
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var commitsAvg = response.Aggregations.GetWeightedAverage("weighted_avg_commits");
		commitsAvg.Should().NotBeNull();
		commitsAvg.Value.Should().BeGreaterThan(0);
	}
}
