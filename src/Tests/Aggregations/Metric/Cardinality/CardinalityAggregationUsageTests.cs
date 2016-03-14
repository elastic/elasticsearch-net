using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Cardinality
{
	public class CardinalityAggregationUsageTests : AggregationUsageTestBase
	{
		public CardinalityAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				state_count = new
				{
					cardinality = new
					{
						field = "state",
						precision_threshold = 100
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Cardinality("state_count", c => c
					.Field(p => p.State)
					.PrecisionThreshold(100)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new CardinalityAggregation("state_count", Field<Project>(p => p.State))
				{
					PrecisionThreshold = 100
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var projectCount = response.Aggs.Cardinality("state_count");
			projectCount.Should().NotBeNull();
			projectCount.Value.Should().Be(3);
        }
	}
}
