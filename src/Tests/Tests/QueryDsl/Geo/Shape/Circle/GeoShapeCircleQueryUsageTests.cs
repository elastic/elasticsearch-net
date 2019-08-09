using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Circle
{
	[SkipVersion(">=6.6.0", "New indexing strategy introduced based on BKD trees that does not support circle geomtery in this fashion, yet. See https://github.com/elastic/elasticsearch/issues/32039")]
	public class GeoShapeCircleQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeCircleQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeCircleQuery>(a => a.GeoShape as IGeoShapeCircleQuery)
		{
			q => q.Field = null,
			q => q.Shape = null,
			q => q.Shape.Coordinates = null,
		};

		protected override QueryContainer QueryInitializer => new GeoShapeCircleQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.LocationShape),
			Shape = new CircleGeoShape(CircleCoordinates) { Radius = "100m" },
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "circle",
			radius = "100m",
			coordinates = CircleCoordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeCircle(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Coordinates(CircleCoordinates)
				.Radius("100m")
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
