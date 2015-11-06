using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.Distance
{
	public class GeoDistanceUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

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

		protected override QueryContainer QueryInitializer => new GeoDistanceQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Static.Field<Project>(p => p.Location),
			DistanceType = GeoDistanceType.Arc,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q;
	}
}
