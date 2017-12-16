using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;
using System.Collections.Generic;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Aggregations.Metric.Average
{
	public class AverageAggregationUsageTests : AggregationUsageTestBase
	{
		public AverageAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			average_commits = new
			{
				meta = new
				{
					foo = "bar"
				},
				avg = new
				{
					field = "numberOfCommits",
					missing = 10.0,
					script = new
					{
						source = "_value * 1.2",
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Average("average_commits", avg => avg
				.Meta(m => m
					.Add("foo", "bar")
				)
				.Field(p => p.NumberOfCommits)
				.Missing(10)
				.Script(ss => ss.Source("_value * 1.2"))
			);

		protected override AggregationDictionary InitializerAggs =>
			new AverageAggregation("average_commits", Field<Project>(p => p.NumberOfCommits))
			{
				Meta = new Dictionary<string, object>
				{
					{"foo", "bar"}
				},
				Missing = 10,
				Script = new InlineScript("_value * 1.2")
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsAvg = response.Aggregations.Average("average_commits");
			commitsAvg.Should().NotBeNull();
			commitsAvg.Value.Should().BeGreaterThan(0);
			commitsAvg.Meta.Should().NotBeNull().And.HaveCount(1);
			commitsAvg.Meta["foo"].Should().Be("bar");
		}
	}
}
