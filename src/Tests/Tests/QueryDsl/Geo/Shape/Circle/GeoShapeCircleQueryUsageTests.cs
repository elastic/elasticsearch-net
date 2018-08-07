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

		protected override QueryContainer QueryInitializer => new GeoShapeCircleQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new CircleGeoShape(this._coordinates) { Radius = "100m" },
			Relation = GeoShapeRelation.Intersects,
			IgnoreUnmapped = false
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeCircle(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.IgnoreUnmapped()
				.Coordinates(this._coordinates)
				.Radius("100m")
				.Relation(GeoShapeRelation.Intersects)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeCircleQuery>(a => a.GeoShape as IGeoShapeCircleQuery)
		{
			q =>  q.Field = null,
			q =>  q.Shape = null,
			q =>  q.Shape.Coordinates = null,
		};
	}
}
