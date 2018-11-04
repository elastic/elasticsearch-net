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
		private readonly IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> _coordinates = new[]
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
			Field = Field<Project>(p => p.Location),
			Shape = new MultiPolygonGeoShape(_coordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "multipolygon",
			coordinates = _coordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiPolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(_coordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
