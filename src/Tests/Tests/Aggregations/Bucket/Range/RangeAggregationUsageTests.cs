using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Range
{
	public class RangeAggregationUsageTests : AggregationUsageTestBase
	{
		public RangeAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commit_ranges = new
			{
				range = new
				{
					field = "numberOfCommits",
					ranges = new object[]
					{
						new {to = 100.0},
						new {from = 100.0, to = 500.0},
						new {from = 500.0}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Range("commit_ranges", ra => ra
				.Field(p => p.NumberOfCommits)
				.Ranges(
					r => r.To(100),
					r => r.From(100).To(500),
					r => r.From(500)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new RangeAggregation("commit_ranges")
			{
				Field = Field<Project>(p => p.NumberOfCommits),
				Ranges = new List<AggregationRange>
				{
					{new AggregationRange {To = 100}},
					{new AggregationRange {From = 100, To = 500}},
					{new AggregationRange {From = 500}}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitRanges = response.Aggregations.Range("commit_ranges");
			commitRanges.Should().NotBeNull();
			commitRanges.Buckets.Count.Should().Be(3);
			commitRanges.Buckets.FirstOrDefault(r => r.Key == "*-100.0").Should().NotBeNull();
			commitRanges.Buckets.FirstOrDefault(r => r.Key == "100.0-500.0").Should().NotBeNull();
			commitRanges.Buckets.FirstOrDefault(r => r.Key == "500.0-*").Should().NotBeNull();
		}
	}
}
