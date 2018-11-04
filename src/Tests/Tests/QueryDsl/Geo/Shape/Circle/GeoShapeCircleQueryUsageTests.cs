using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Circle
{
	public class GeoShapeCircleQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		private readonly GeoCoordinate _coordinates = new GeoCoordinate(-45.0, 45.0);

		public GeoShapeCircleQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeCircleQuery>(a => a.GeoShape as IGeoShapeCircleQuery)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => q.Shape.Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeCircleQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.Location),
			Shape = new CircleGeoShape(_coordinates) { Radius = "100m" },
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "circle",
			radius = "100m",
			coordinates = _coordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeCircle(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(_coordinates)
				.Radius("100m")
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
