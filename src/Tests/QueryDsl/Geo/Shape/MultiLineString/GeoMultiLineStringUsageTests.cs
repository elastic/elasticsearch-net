using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Geo.Shape.MultiLineString
{
	public class GeoMultiLineStringUsageTests : ShapeQueryUsageTestsBase
	{
		public GeoMultiLineStringUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<IEnumerable<IEnumerable<double>>> _coordinates = new[]
		{
			new [] { new [] { 102.0, 2.0 }, new [] { 103.0, 2.0},new [] { 103.0, 3.0 }, new []{ 102.0, 3.0 } },
			new [] { new [] { 100.0, 0.0 }, new [] { 101.0, 0.0},new [] { 101.0, 1.0 }, new []{ 100.0, 1.0 } },
			new [] { new [] { 100.2, 0.2 }, new [] { 100.8, 0.2},new [] { 100.8, 0.8 }, new []{ 102.0, 0.8 } },
		};

		protected override object ShapeJson => new
		{
			type ="multilinestring",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapeMultiLineStringQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new MultiLineStringGeoShape(this._coordinates)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiLineString(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Coordinates(this._coordinates)
			);
	}
}
