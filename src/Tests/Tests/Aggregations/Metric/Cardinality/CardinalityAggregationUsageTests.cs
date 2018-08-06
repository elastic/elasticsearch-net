using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Cardinality
{
	public class CardinalityAggregationUsageTests : AggregationUsageTestBase
	{
		public CardinalityAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			state_count = new
			{
				cardinality = new
				{
					field = "state",
					precision_threshold = 100
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Cardinality("state_count", c => c
				.Field(p => p.State)
				.PrecisionThreshold(100)
			);

		protected override AggregationDictionary InitializerAggs =>
			new CardinalityAggregation("state_count", Field<Project>(p => p.State))
			{
				PrecisionThreshold = 100
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var projectCount = response.Aggregations.Cardinality("state_count");
			projectCount.Should().NotBeNull();
			projectCount.Value.Should().Be(3);
        }
	}
}
