using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.MultiPoint
{
	public class GeoMultiPointUsageTests : ShapeQueryUsageTestsBase
	{
		public GeoMultiPointUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<GeoCoordinate> _coordinates = new GeoCoordinate[]
		{
			new [] {-77.03653, 38.897676},
			new [] {-77.009051, 38.889939 }
		};

		protected override object ShapeJson => new
		{
			type ="multipoint",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapeMultiPointQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new MultiPointGeoShape(this._coordinates)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiPoint(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Coordinates(this._coordinates)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeMultiPointQuery>(a => a.GeoShape as IGeoShapeMultiPointQuery)
		{
			q =>  q.Field = null,
			q =>  q.Shape = null,
			q =>  q.Shape.Coordinates = null,
		};
	}
}
