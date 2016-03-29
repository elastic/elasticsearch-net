using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.GeoDistance
{
	public class GeoDistanceAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoDistanceAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
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
							new { to = 100.0 },
							new { from = 100.0, to = 300.0 },
							new { from = 300.0 }
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.GeoDistance("rings_around_amsterdam", g => g
					.Field(p => p.Location)
					.Origin(52.376, 4.894)
					.Ranges(
						r => r.To(100),
						r => r.From(100).To(300),
						r => r.From(300)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new GeoDistanceAggregation("rings_around_amsterdam")
				{
					Field = Field((Project p) => p.Location),
					Origin = "52.376, 4.894",
					Ranges = new List<Nest.Range>
					{
						new Nest.Range { To = 100 },
						new Nest.Range { From = 100, To = 300 },
						new Nest.Range { From = 300 }
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var ringsAroundAmsterdam = response.Aggs.GeoDistance("rings_around_amsterdam");
			ringsAroundAmsterdam.Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.Where(r => r.Key == "*-100.0").FirstOrDefault().Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.Where(r => r.Key == "100.0-300.0").FirstOrDefault().Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.Where(r => r.Key == "300.0-*").FirstOrDefault().Should().NotBeNull();
		}
	}
}