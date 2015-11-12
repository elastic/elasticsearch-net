using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.DistanceRange
{
	public class GeoDistanceRangeUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceRangeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_distance_range = new
			{
				from = "200.0km",
				to = "400.0mi",
				distance_type = "arc",
				optimize_bbox = "indexed",
				include_lower = false,
				include_upper = false,
				coerce = true,
				ignore_malformed = true,
				validation_method = "strict",
				_name = "named_query",
				boost = 1.1,
				location = new
				{
					lat = 40.0,
					lon = -70.0
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoDistanceRangeQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Static.Field<Project>(p=>p.Location),
			DistanceType = GeoDistanceType.Arc,
			Coerce = true,
			From = GeoDistance.Kilometers(200),
			IgnoreMalformed = true,
			IncludeLower = false,
			IncludeUpper = false,
			Location = new GeoLocation(40, -70),
			OptimizeBoundingBox = GeoOptimizeBBox.Indexed,
			To = GeoDistance.Miles(400),
			ValidationMethod = GeoValidationMethod.Strict
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistanceRange(g=>g
				.Boost(1.1)
				.Name("named_query")
				.Field(p=>p.Location)
				.DistanceType(GeoDistanceType.Arc)
				.Coerce()
				.From(200, GeoPrecision.Kilometers)
				.IgnoreMalformed()
				.FromExclusive()
				.ToExclusive()
				.Location(new GeoLocation(40, -70))
				.Optimize(GeoOptimizeBBox.Indexed)
				.To(GeoDistance.Miles(400))
				.ValidationMethod(GeoValidationMethod.Strict)
			);
	}
}
