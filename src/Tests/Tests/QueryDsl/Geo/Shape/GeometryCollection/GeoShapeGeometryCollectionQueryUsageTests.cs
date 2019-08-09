using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Geo.Shape.GeometryCollection
{
	public class GeoShapeGeometryCollectionQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeGeometryCollectionQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapeGeometryCollectionQuery>(a => a.GeoShape as IGeoShapeGeometryCollectionQuery)
			{
				q => q.Field = null,
				q => q.Shape = null,
				q => q.Shape.Geometries = null,
			};

		protected override QueryContainer QueryInitializer => new GeoShapeGeometryCollectionQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new Nest.GeometryCollection
			{
				Geometries = new IGeoShape[]
				{
					new PointGeoShape(PointCoordinates),
					new MultiPointGeoShape(MultiPointCoordinates),
					new LineStringGeoShape(LineStringCoordinates),
					new MultiLineStringGeoShape(MultiLineStringCoordinates),
					new PolygonGeoShape(PolygonCoordinates),
					new MultiPolygonGeoShape(MultiPolygonCoordinates),
				}
			},
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "geometrycollection",
			geometries = new object[]
			{
				new
				{
					type = "point",
					coordinates = PointCoordinates
				},
				new
				{
					type = "multipoint",
					coordinates = MultiPointCoordinates
				},
				new
				{
					type = "linestring",
					coordinates = LineStringCoordinates
				},
				new
				{
					type = "multilinestring",
					coordinates = MultiLineStringCoordinates
				},
				new
				{
					type = "polygon",
					coordinates = PolygonCoordinates
				},
				new
				{
					type = "multipolygon",
					coordinates = MultiPolygonCoordinates
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeGeometryCollection(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Geometries(
					new PointGeoShape(PointCoordinates),
					new MultiPointGeoShape(MultiPointCoordinates),
					new LineStringGeoShape(LineStringCoordinates),
					new MultiLineStringGeoShape(MultiLineStringCoordinates),
					new PolygonGeoShape(PolygonCoordinates),
					new MultiPolygonGeoShape(MultiPolygonCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
