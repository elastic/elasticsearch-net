using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Geo.Shape
{
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

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				locationShape = new
				{
					relation = "intersects",
					shape = ShapeJson
				}
			}
		};

		protected abstract object ShapeJson { get; }
	}
}
