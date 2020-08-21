// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo.GeoShape
{
	/**
	 * The GeoShape Query uses the same grid square representation as the geo_shape mapping
	 * to find documents that have a shape that intersects with the query shape.
	 * It will also use the same PrefixTree configuration as defined for the field mapping.
	 *
     * The query supports two ways of defining the query shape, either by providing a whole
	 * shape definition, or by referencing the name of a shape pre-indexed in another index.
	 *
	 * See the Elasticsearch documentation on {ref_current}/query-dsl-geo-shape-query.html[geoshape queries] for more detail.
	 */
	public abstract class GeoShapeQueryUsageTestsBase : QueryDslUsageTestsBase
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


		protected GeoShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }
	}

	/**
	 * [float]
	 * [[geo-shape-query-point]]
	 * == Querying with Point
	 *
	 */
	public class GeoShapePointQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapePointQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IPointGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new PointGeoShape(PointCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.Point(PointCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-multipoint]]
	 * == Querying with MultiPoint
	 *
	 * NOTE: Elasticsearch 7.7.0+ required when MultiPoint is indexed using BKD trees (the default).
	 */
	[SkipVersion("<7.7.0", "multipoint queries are supported from 7.7.0 onwards")]
	public class GeoShapeMultiPointQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeMultiPointQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IMultiPointGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new MultiPointGeoShape(MultiPointCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.MultiPoint(MultiPointCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-linestring]]
	 * == Querying with LineString
	 *
	 */
	public class GeoShapeLineStringQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeLineStringQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((ILineStringGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new LineStringGeoShape(LineStringCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.LineString(LineStringCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-multilinestring]]
	 * == Querying with MultiLineString
	 *
	 */
	public class GeoShapeMultiLineStringQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeMultiLineStringQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IMultiLineStringGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new MultiLineStringGeoShape(MultiLineStringCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.MultiLineString(MultiLineStringCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-polygon]]
	 * == Querying with Polygon
	 *
	 */
	public class GeoShapePolygonQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapePolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IPolygonGeoShape)q.Shape).Coordinates = null
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new PolygonGeoShape(PolygonCoordinates),
			IgnoreUnmapped = true,
			Relation = GeoShapeRelation.Intersects,
		};

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
					shape = new
					{
						type = "polygon",
						coordinates = PolygonCoordinates
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.Polygon(PolygonCoordinates)
				)
				.IgnoreUnmapped()
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-multipolygon]]
	 * == Querying with MultiPolygon
	 *
	 */
	public class GeoShapeMultiPolygonQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeMultiPolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IMultiPolygonGeoShape)q.Shape).Coordinates = null
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new MultiPolygonGeoShape(MultiPolygonCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.MultiPolygon(MultiPolygonCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-geometrycollection]]
	 * == Querying with GeometryCollection
	 *
	 */
	public class GeoShapeGeometryCollectionQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeGeometryCollectionQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IGeometryCollection)q.Shape).Geometries = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new GeometryCollection(new IGeoShape[]
			{
				new PointGeoShape(PointCoordinates),
				new MultiPointGeoShape(MultiPointCoordinates),
				new LineStringGeoShape(LineStringCoordinates),
				new MultiLineStringGeoShape(MultiLineStringCoordinates),
				new PolygonGeoShape(PolygonCoordinates),
				new MultiPolygonGeoShape(MultiPolygonCoordinates),
			}),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
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
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	 * [float]
	 * [[geo-shape-query-envelope]]
	 * == Querying with Envelope
	 *
	 */
	public class GeoShapeEnvelopeQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeEnvelopeQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((IEnvelopeGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new EnvelopeGeoShape(EnvelopeCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.Envelope(EnvelopeCoordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
     * [float]
     * [[geo-shape-query-circle]]
     * == Querying with Circle
     *
	 * NOTE: Available in Elasticsearch 7.7.0+
     */
	[SkipVersion("<7.7.0", "CIRCLE geometry is supported from 7.7.0+. See https://github.com/elastic/elasticsearch/issues/39237")]
	public class GeoShapeCircleQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeCircleQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => ((ICircleGeoShape)q.Shape).Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			Shape = new CircleGeoShape(CircleCoordinates, "100m"),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Shape(s => s
					.Circle(CircleCoordinates, "100m")
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}

	/**
	* [float]
	* [[geo-shape-query-indexedshape]]
	* == Querying with an indexed shape
	*
	* The GeoShape Query supports using a shape which has already been indexed in another index and/or index type within a geoshape query.
	* This is particularly useful for when you have a pre-defined list of shapes which are useful to your application and you want to reference this
	* using a logical name (for example __New Zealand__), rather than having to provide their coordinates within the request each time.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-geo-shape-query.html[geoshape queries] for more detail.
	*/
	public class GeoShapeIndexedShapeQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoShapeIndexedShapeQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q => q.Field = null,
			q => q.IndexedShape = null,
			q => q.IndexedShape.Id = null,
			q => q.IndexedShape.Index = null,
			q => q.IndexedShape.Path = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.LocationShape),
			IndexedShape = new FieldLookup
			{
				Id = Project.Instance.Name,
				Index = Infer.Index<Project>(),
				Path = Infer.Field<Project>(p => p.LocationShape),
				Routing = Project.Instance.Name
			},
			Relation = GeoShapeRelation.Intersects
		};

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
				{
					indexed_shape = new
					{
						id = Project.Instance.Name,
						index = "project",
						path = "locationShape",
						routing = Project.Instance.Name
					},
					relation = "intersects"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.IndexedShape(p => p
					.Id(Project.Instance.Name)
					.Path(pp => pp.LocationShape)
					.Routing(Project.Instance.Name)
				)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
