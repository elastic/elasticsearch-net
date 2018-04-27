using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.LineString
{
	public class GeoShapeLineStringQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeLineStringQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<GeoCoordinate> _coordinates = new GeoCoordinate[]
		{
			new [] {-77.03653, 38.897676},
			new [] {-77.009051, 38.889939 }
		};

		protected override object ShapeJson => new
		{
			type ="linestring",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new LineStringGeoShape(this._coordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Shape(s => s
					.LineString(this._coordinates)
				)
				.Relation(GeoShapeRelation.Intersects)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
		{
			q =>  q.Field = null,
			q =>  q.Shape = null,
			q =>  ((ILineStringGeoShape)q.Shape).Coordinates = null,
		};
	}
}
