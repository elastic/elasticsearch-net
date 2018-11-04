using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Polygon
{
	public class GeoShapePolygonQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		private readonly IEnumerable<IEnumerable<GeoCoordinate>> _polygonCoordinates = new[]
		{
			new GeoCoordinate[]
			{
				new[] { -17.0, 10.0 }, new[] { 16.0, 15.0 }, new[] { 12.0, 0.0 }, new[] { 16.0, -15.0 }, new[] { -17.0, -10.0 }, new[] { -17.0, 10.0 }
			},
			new GeoCoordinate[]
			{
				new[] { 18.2, 8.2 }, new[] { -18.8, 8.2 }, new[] { -10.8, -8.8 }, new[] { 18.2, 8.8 }
			}
		};

		public GeoShapePolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapePolygonQuery>(a => a.GeoShape as IGeoShapePolygonQuery)
			{
				q => q.Field = null,
				q => q.Shape = null,
				q => q.Shape.Coordinates = null
			};

		protected override QueryContainer QueryInitializer => new GeoShapePolygonQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.Location),
			Shape = new PolygonGeoShape(_polygonCoordinates),
			Relation = GeoShapeRelation.Intersects,
			IgnoreUnmapped = false
		};

		protected override object ShapeJson => new
		{
			type = "polygon",
			coordinates = _polygonCoordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapePolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(_polygonCoordinates)
				.Relation(GeoShapeRelation.Intersects)
				.IgnoreUnmapped(false)
			);
	}
}
