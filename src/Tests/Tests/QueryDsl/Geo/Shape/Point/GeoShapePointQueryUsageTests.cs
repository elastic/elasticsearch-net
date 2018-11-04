using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Point
{
	public class GeoShapePointQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		private readonly GeoCoordinate _coordinates = new[] { -77.03653, 38.897676 };

		public GeoShapePointQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapePointQuery>(a => a.GeoShape as IGeoShapePointQuery)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => q.Shape.Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapePointQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.Location),
			Shape = new PointGeoShape(_coordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "point",
			coordinates = _coordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapePoint(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(_coordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
