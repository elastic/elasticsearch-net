using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Polygon
{
	public class GeoShapePolygonQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapePolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapePolygonQuery>(a => a.GeoShape as IGeoShapePolygonQuery)
			{
				q => q.Field = null, q => q.Shape = null, q => q.Shape.Coordinates = null
			};

		protected override QueryContainer QueryInitializer => new GeoShapePolygonQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.LocationShape),
			Shape = new PolygonGeoShape(PolygonCoordinates),
			Relation = GeoShapeRelation.Intersects,
			IgnoreUnmapped = true
		};

		protected override object ShapeJson => new { type = "polygon", coordinates = PolygonCoordinates };

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				ignore_unmapped = true,
				locationShape = new
				{
					relation = "intersects",
					shape = ShapeJson
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapePolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Coordinates(PolygonCoordinates)
				.Relation(GeoShapeRelation.Intersects)
				.IgnoreUnmapped(true)
			);
	}
}
