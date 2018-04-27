using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Circle
{
	public class GeoShapeCircleQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeCircleQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly GeoCoordinate _coordinates = new GeoCoordinate(-45.0, 45.0);

		protected override object ShapeJson => new
		{
			type ="circle",
			radius = "100m",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new CircleGeoShape(this._coordinates, "100m"),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Shape(s => s
					.Circle(this._coordinates, "100m")
				)
				.Relation(GeoShapeRelation.Intersects)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q =>  q.Field = null,
			q =>  q.Shape = null,
			q =>  ((ICircleGeoShape)q.Shape).Coordinates = null,
		};
	}
}
