using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Geo.Shape.GeometryCollection
{
	public class GeoShapeGeometryCollectionQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		private readonly IEnumerable<GeoCoordinate> _lineStringCoordinates = new GeoCoordinate[]
		{
			new[] { -77.03653, 38.897676 },
			new[] { -77.009051, 38.889939 }
		};

		private readonly IEnumerable<IEnumerable<GeoCoordinate>> _multiLineStringCoordinates = new[]
		{
			new GeoCoordinate[] { new[] { 12.0, 2.0 }, new[] { 13.0, 2.0 }, new[] { 13.0, 3.0 }, new[] { 12.0, 3.0 } },
			new GeoCoordinate[] { new[] { 10.0, 0.0 }, new[] { 11.0, 0.0 }, new[] { 11.0, 1.0 }, new[] { 10.0, 1.0 } },
			new GeoCoordinate[] { new[] { 10.2, 0.2 }, new[] { 10.8, 0.2 }, new[] { 10.8, 0.8 }, new[] { 12.0, 0.8 } },
		};

		private readonly IEnumerable<GeoCoordinate> _multiPointCoordinates = new GeoCoordinate[]
		{
			new[] { -77.03653, 38.897676 },
			new[] { -77.009051, 38.889939 }
		};

		private readonly IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> _multiPolygonCoordinates = new[]
		{
			new[]
			{
				new GeoCoordinate[]
				{
					new[] { -17.0, 10.0 }, new[] { 16.0, 15.0 }, new[] { 12.0, 0.0 }, new[] { 16.0, -15.0 }, new[] { -17.0, -10.0 },
					new[] { -17.0, 10.0 }
				},
				new GeoCoordinate[]
				{
					new[] { 18.2, 8.2 }, new[] { -18.8, 8.2 }, new[] { -10.8, -8.8 }, new[] { 18.2, 8.8 }
				}
			},
			new[]
			{
				new GeoCoordinate[]
				{
					new[] { -15.0, 8.0 }, new[] { 16.0, 15.0 }, new[] { 12.0, 0.0 }, new[] { 16.0, -15.0 }, new[] { -17.0, -10.0 },
					new[] { -15.0, 8.0 }
				}
			}
		};

		private readonly GeoCoordinate _pointCoordinates = new[] { -77.03653, 38.897676 };

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
			Field = Infer.Field<Project>(p => p.Location),
			Shape = new Nest.GeometryCollection
			{
				Geometries = new IGeoShape[]
				{
					new PointGeoShape(_pointCoordinates),
					new MultiPointGeoShape(_multiPointCoordinates),
					new LineStringGeoShape(_lineStringCoordinates),
					new MultiLineStringGeoShape(_multiLineStringCoordinates),
					new PolygonGeoShape(_polygonCoordinates),
					new MultiPolygonGeoShape(_multiPolygonCoordinates),
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
					coordinates = _pointCoordinates
				},
				new
				{
					type = "multipoint",
					coordinates = _multiPointCoordinates
				},
				new
				{
					type = "linestring",
					coordinates = _lineStringCoordinates
				},
				new
				{
					type = "multilinestring",
					coordinates = _multiLineStringCoordinates
				},
				new
				{
					type = "polygon",
					coordinates = _polygonCoordinates
				},
				new
				{
					type = "multipolygon",
					coordinates = _multiPolygonCoordinates
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeGeometryCollection(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Geometries(
					new PointGeoShape(_pointCoordinates),
					new MultiPointGeoShape(_multiPointCoordinates),
					new LineStringGeoShape(_lineStringCoordinates),
					new MultiLineStringGeoShape(_multiLineStringCoordinates),
					new PolygonGeoShape(_polygonCoordinates),
					new MultiPolygonGeoShape(_multiPolygonCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
