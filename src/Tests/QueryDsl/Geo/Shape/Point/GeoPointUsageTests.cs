using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Geo.Shape.Point
{
	public class GeoPointUsageTests : ShapeQueryUsageTestsBase
	{
		public GeoPointUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<double> _coordinates = new[] { -77.03653, 38.897676 };

		protected override object ShapeJson => new
		{
			type ="point",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapePointQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new PointGeoShape(this._coordinates)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapePoint(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Coordinates(this._coordinates)
			);
	}
}
