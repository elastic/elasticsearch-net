using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using static Nest.Infer;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Aggregations.Metric.WeightedAverage
{
	public class WeightedAverageAggregationUsageTests : AggregationUsageTestBase
	{
		public WeightedAverageAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private const string AggregationName = "weighted_avg_commits";

		protected override object AggregationJson => new
		{
			aggs = new
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
							field = "numberOfContributors"
						}
					}
				}
			},
			size = 0
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.WeightedAverage(AggregationName, avg => avg
				.Value(v => v.Field(p => p.NumberOfCommits).Missing(0))
				.Weight(v => v.Field(p => p.NumberOfContributors))
			);

		protected override AggregationDictionary InitializerAggs =>
			new WeightedAverageAggregation(AggregationName)
			{
				Value = new WeightedAverageValue(Field<Project>(p => p.NumberOfCommits))
				{
					Missing = 0
				},
				Weight = new WeightedAverageValue(Field<Project>(p => p.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsAvg = response.Aggregations.WeightedAverage(AggregationName);
			commitsAvg.Should().NotBeNull();
			commitsAvg.Value.Should().BeGreaterThan(0);
		}
	}
}
