using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.MultiPoint
{
	public class GeoShapeMultiPointQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		private readonly IEnumerable<GeoCoordinate> _coordinates = new GeoCoordinate[]
		{
			new[] { -77.03653, 38.897676 },
			new[] { -77.009051, 38.889939 }
		};

		public GeoShapeMultiPointQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapeMultiPointQuery>(a => a.GeoShape as IGeoShapeMultiPointQuery)
			{
				q => q.Field = null,
				q => q.Shape = null,
				q => q.Shape.Coordinates = null,
			};

		protected override QueryContainer QueryInitializer => new GeoShapeMultiPointQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.Location),
			Shape = new MultiPointGeoShape(_coordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "multipoint",
			coordinates = _coordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiPoint(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(_coordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
