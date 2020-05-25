using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.MultiPoint
{
	[SkipVersion(">=6.6.0", "New indexing strategy introduced based on BKD trees that does not support circle geomtery in this fashion, yet. See https://github.com/elastic/elasticsearch/issues/32039")]
	public class GeoShapeMultiPointQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
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
			Field = Field<Project>(p => p.LocationShape),
			Shape = new MultiPointGeoShape(MultiPointCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "multipoint",
			coordinates = MultiPointCoordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiPoint(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Coordinates(MultiPointCoordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
