// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.Shape
{
	/**
	 * Like geo_shape, Elasticsearch supports the ability to index arbitrary two dimension (non Geospatial) geometries making
	 * it possible to map out virtual worlds, sporting venues, theme parks, and CAD diagrams. The shape field type
	 * supports points, lines, polygons, multi-polygons, envelope, etc.
	 *
	 * See the Elasticsearch documentation on {ref_current}/query-dsl-shape-query.html[shape queries] for more detail.
	 */
	public abstract class ShapeQueryUsageTestsBase : QueryDslUsageTestsBase
	{
		protected static readonly GeoCoordinate CircleCoordinates = new GeoCoordinate(-45.0, 45.0);

		protected static readonly IEnumerable<GeoCoordinate> EnvelopeCoordinates = new GeoCoordinate[]
		{
			new[] { 45.0, -45.0, },
			new[] { -45.0, 45.0 }
		};

		protected static readonly IEnumerable<GeoCoordinate> LineStringCoordinates = new GeoCoordinate[]
		{
			new[] { -77.03653, 38.897676 },
			new[] { -77.009051, 38.889939 }
		};

		protected static readonly IEnumerable<IEnumerable<GeoCoordinate>> MultiLineStringCoordinates = new[]
		{
			new GeoCoordinate[] { new[] { 12.0, 2.0 }, new[] { 13.0, 2.0 }, new[] { 13.0, 3.0 }, new[] { 12.0, 3.0 } },
			new GeoCoordinate[] { new[] { 10.0, 0.0 }, new[] { 11.0, 0.0 }, new[] { 11.0, 1.0 }, new[] { 10.0, 1.0 } },
			new GeoCoordinate[] { new[] { 10.2, 0.2 }, new[] { 10.8, 0.2 }, new[] { 10.8, 0.8 }, new[] { 12.0, 0.8 } },
		};

		protected static readonly IEnumerable<GeoCoordinate> MultiPointCoordinates = new GeoCoordinate[]
		{
			new[] { -77.03653, 38.897676 },
			new[] { -77.009051, 38.889939 }
		};

		protected static readonly IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> MultiPolygonCoordinates = new[]
		{
			new[]
			{
				new GeoCoordinate[]
				{
					new[] { -17.0, 10.0 },
					new[] { 16.0, 15.0 },
					new[] { 12.0, 0.0 },
					new[] { 16.0, -15.0 },
					new[] { -17.0, -10.0 },
					new[] { -17.0, 10.0 }
				},
				new GeoCoordinate[]
				{
					new[] { 18.2, 8.2 },
					new[] { -18.8, 8.2 },
					new[] { -10.8, -8.8 },
					new[] { 18.2, 8.2 }
				}
			},
			new[]
			{
				new GeoCoordinate[]
				{
					new[] { -15.0, 8.0 },
					new[] { 16.0, 15.0 },
					new[] { 12.0, 0.0 },
					new[] { 16.0, -15.0 },
					new[] { -17.0, -10.0 },
					new[] { -15.0, 8.0 }
				}
			}
		};

		protected static readonly GeoCoordinate PointCoordinates = new[] { -77.03653, 38.897676 };

		protected static readonly IEnumerable<IEnumerable<GeoCoordinate>> PolygonCoordinates = new[]
		{
			new GeoCoordinate[]
			{
				new[] { -17.0, 10.0 }, new[] { 16.0, 15.0 }, new[] { 12.0, 0.0 }, new[] { 16.0, -15.0 }, new[] { -17.0, -10.0 }, new[] { -17.0, 10.0 }
			},
			new GeoCoordinate[]
			{
				new[] { 18.2, 8.2 }, new[] { -18.8, 8.2 }, new[] { -10.8, -8.8 }, new[] { 18.2, 8.2 }
			}
		};


		protected ShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }
	}

	/**
	 * [float]
	 * [[shape-query-point]]
	 * == Querying with Point
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapePointQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapePointQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IPointGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new PointGeoShape(PointCoordinates),
			Relation = ShapeRelation.Intersects
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "point",
						coordinates = PointCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.Point(PointCoordinates)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-multipoint]]
	 * == Querying with MultiPoint
	 *
	 * NOTE: Elasticsearch 7.7.0+ required when MultiPoint is indexed using BKD trees (the default).
	 */
	[SkipVersion("<7.7.0", "Multipoint shape queries supported in 7.7.0+")]
	public class ShapeMultiPointQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeMultiPointQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IMultiPointGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new MultiPointGeoShape(MultiPointCoordinates),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "multipoint",
						coordinates = MultiPointCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.MultiPoint(MultiPointCoordinates)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-linestring]]
	 * == Querying with LineString
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapeLineStringQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeLineStringQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((ILineStringGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new LineStringGeoShape(LineStringCoordinates),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "linestring",
						coordinates = LineStringCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.LineString(LineStringCoordinates)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-multilinestring]]
	 * == Querying with MultiLineString
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapeMultiLineStringQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeMultiLineStringQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IMultiLineStringGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new MultiLineStringGeoShape(MultiLineStringCoordinates),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "multilinestring",
						coordinates = MultiLineStringCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.MultiLineString(MultiLineStringCoordinates)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-polygon]]
	 * == Querying with Polygon
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapePolygonQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapePolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IPolygonGeoShape)q.Shape).Coordinates = null
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new PolygonGeoShape(PolygonCoordinates),
			IgnoreUnmapped = true,
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				ignore_unmapped = true,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "polygon",
						coordinates = PolygonCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.Polygon(PolygonCoordinates)
				)
				.IgnoreUnmapped()
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-multipolygon]]
	 * == Querying with MultiPolygon
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapeMultiPolygonQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeMultiPolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IMultiPolygonGeoShape)q.Shape).Coordinates = null
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new MultiPolygonGeoShape(MultiPolygonCoordinates),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "multipolygon",
						coordinates = MultiPolygonCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.MultiPolygon(MultiPolygonCoordinates)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-geometrycollection]]
	 * == Querying with GeometryCollection
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapeGeometryCollectionQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeGeometryCollectionQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IGeometryCollection)q.Shape).Geometries = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new GeometryCollection(new IGeoShape[]
			{
				new PointGeoShape(PointCoordinates),
				new MultiPointGeoShape(MultiPointCoordinates),
				new LineStringGeoShape(LineStringCoordinates),
				new MultiLineStringGeoShape(MultiLineStringCoordinates),
				new PolygonGeoShape(PolygonCoordinates),
				new MultiPolygonGeoShape(MultiPolygonCoordinates),
			}),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
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
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.GeometryCollection(
						new PointGeoShape(PointCoordinates),
						new MultiPointGeoShape(MultiPointCoordinates),
						new LineStringGeoShape(LineStringCoordinates),
						new MultiLineStringGeoShape(MultiLineStringCoordinates),
						new PolygonGeoShape(PolygonCoordinates),
						new MultiPolygonGeoShape(MultiPolygonCoordinates)
					)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[shape-query-envelope]]
	 * == Querying with Envelope
	 *
	 */
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapeEnvelopeQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeEnvelopeQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IEnvelopeGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new EnvelopeGeoShape(EnvelopeCoordinates),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "envelope",
						coordinates = EnvelopeCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.Envelope(EnvelopeCoordinates)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
     * [float]
     * [[shape-query-circle]]
     * == Querying with Circle
     *
	 * NOTE: Available in Elasticsearch 7.7.0+
     */
	[SkipVersion("<7.7.0", "Circle shape queries are supported in 7.7.0+")]
	public class ShapeCircleQueryUsageTests : ShapeQueryUsageTestsBase
	{
		public ShapeCircleQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((ICircleGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			Shape = new CircleGeoShape(CircleCoordinates, "100m"),
			Relation = ShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					relation = "intersects",
					shape = new
					{
						type = "circle",
						radius = "100m",
						coordinates = CircleCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.Shape(s => s
					.Circle(CircleCoordinates, "100m")
				)
				.Relation(ShapeRelation.Intersects)
			);
	}

	/**
	* [float]
	* [[shape-query-indexedshape]]
	* == Querying with an indexed shape
	*
	* The Query also supports using a shape which has already been indexed in another index. This is particularly useful for when you have
	* a pre-defined list of shapes which are useful to your application and you want to reference this using a logical name (for example New Zealand)
	* rather than having to provide their coordinates each time. In this situation it is only necessary to provide:
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-shape-query.html for more detail.
	*/
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapeIndexedShapeQueryUsageTests : QueryDslUsageTestsBase
	{
		public ShapeIndexedShapeQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IShapeQuery>(a => a.Shape)
		{
			q => q.Field = null,
			q => q.IndexedShape = null,
			q => q.IndexedShape.Id = null,
			q => q.IndexedShape.Index = null,
			q => q.IndexedShape.Path = null,
		};

		protected override QueryContainer QueryInitializer => new ShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.ArbitraryShape),
			IndexedShape = new FieldLookup
			{
				Id = Project.Instance.Name,
				Index = Infer.Index<Project>(),
				Path = Infer.Field<Project>(p => p.ArbitraryShape),
				Routing = Project.Instance.Name
			},
			Relation = ShapeRelation.Intersects
		};

		protected override object QueryJson => new
		{
			shape = new
			{
				_name = "named_query",
				boost = 1.1,
				arbitraryShape = new
				{
					indexed_shape = new
					{
						id = Project.Instance.Name,
						index = "project",
						path = "arbitraryShape",
						routing = Project.Instance.Name
					},
					relation = "intersects"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Shape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.ArbitraryShape)
				.IndexedShape(p => p
					.Id(Project.Instance.Name)
					.Path(pp => pp.ArbitraryShape)
					.Routing(Project.Instance.Name)
				)
				.Relation(ShapeRelation.Intersects)
			);
	}
}
