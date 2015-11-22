using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Geo.Shape.Envelope
{
	public class GeoEnvelopeUsageTests : ShapeQueryUsageTestsBase
	{
		public GeoEnvelopeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<IEnumerable<double>> _coordinates = new[]
		{
			new [] { -45.0, 45.0 },
			new [] { 45.0, -45.0 }
		};

		protected override object ShapeJson => new
		{
			type ="envelope",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapeEnvelopeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new EnvelopeGeoShape(this._coordinates)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeEnvelope(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Coordinates(this._coordinates)
			);
	}
}
