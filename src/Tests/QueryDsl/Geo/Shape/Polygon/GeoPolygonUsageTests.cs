using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Geo.Shape.Polygon
{
	public class GeoPolygonUsageTests : ShapeQueryUsageTestsBase
	{
		public GeoPolygonUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<IEnumerable<IEnumerable<double>>> _coordinates = new[]
		{
			new []
			{
				new [] {-177.0, 10.0}, new [] {176.0, 15.0}, new [] {172.0, 0.0}, new [] {176.0, -15.0}, new [] {-177.0, -10.0}, new [] {-177.0, 10.0}
			},
			new []
			{
				new [] {178.2, 8.2}, new [] {-178.8, 8.2}, new [] {-180.8, -8.8}, new [] {178.2, 8.8}
			}
		};

		protected override object ShapeJson => new
		{
			type = "polygon",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapePolygonQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.Location),
			Shape = new PolygonGeoShape(this._coordinates) { }
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapePolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(this._coordinates)
			);
	}
}
