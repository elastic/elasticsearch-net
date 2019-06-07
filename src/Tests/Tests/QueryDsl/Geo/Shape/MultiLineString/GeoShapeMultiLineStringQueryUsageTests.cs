using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.MultiLineString
{
	public class GeoShapeMultiLineStringQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		public GeoShapeMultiLineStringQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapeMultiLineStringQuery>(a => a.GeoShape as IGeoShapeMultiLineStringQuery)
			{
				q => q.Field = null,
				q => q.Shape = null,
				q => q.Shape.Coordinates = null,
			};

		protected override QueryContainer QueryInitializer => new GeoShapeMultiLineStringQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.LocationShape),
			Shape = new MultiLineStringGeoShape(MultiLineStringCoordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "multilinestring",
			coordinates = MultiLineStringCoordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiLineString(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationShape)
				.Coordinates(MultiLineStringCoordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}
