using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.GeoDistance
{
	public class GeoDistanceAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoDistanceAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			rings_around_amsterdam = new
			{
				geo_distance = new
				{
					field = "location",
					origin = new
					{
						lat = 52.376,
						lon = 4.894
					},
					ranges = new object[]
					{
						new {to = 100.0},
						new {from = 100.0, to = 300.0},
						new {from = 300.0}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoDistance("rings_around_amsterdam", g => g
				.Field(p => p.Location)
				.Origin(52.376, 4.894)
				.Ranges(
					r => r.To(100),
					r => r.From(100).To(300),
					r => r.From(300)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoDistanceAggregation("rings_around_amsterdam")
			{
				Field = Field((Project p) => p.Location),
				Origin = "52.376, 4.894",
				Ranges = new List<AggregationRange>
				{
					new AggregationRange {To = 100},
					new AggregationRange {From = 100, To = 300},
					new AggregationRange {From = 300}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var ringsAroundAmsterdam = response.Aggregations.GeoDistance("rings_around_amsterdam");
			ringsAroundAmsterdam.Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "*-100.0").Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "100.0-300.0").Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "300.0-*").Should().NotBeNull();
		}
	}
}
