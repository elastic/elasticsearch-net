using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.MultiPolygon
{
	public class GeoShapeMultiPolygonQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeMultiPolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapeMultiPolygonQuery>(a => a.GeoShape as IGeoShapeMultiPolygonQuery)
			{
				q => q.Field = null,
				q => q.Shape = null,
				q => q.Shape.Coordinates = null
			};

		protected override QueryContainer QueryInitializer => new GeoShapeMultiPolygonQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.LocationShape),
			Shape = new MultiPolygonGeoShape(MultiPolygonCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "multipolygon",
			coordinates = MultiPolygonCoordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiPolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Coordinates(MultiPolygonCoordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
