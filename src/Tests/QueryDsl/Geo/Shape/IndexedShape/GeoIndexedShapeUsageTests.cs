using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.IndexedShape
{
	public class GeoIndexedShapeUsageTests : QueryDslUsageTestsBase
	{
		public GeoIndexedShapeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				location = new
				{
					_name="named_query",
					boost = 1.1,
					indexed_shape = new
					{
						id = 2,
						type = "project",
						index = "project",
						path = "location"
					},
					relation = "intersects"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoIndexedShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			IndexedShape = new FieldLookup
			{
				Id = 2,
				Index = Index<Project>(),
				Type = Type<Project>(),
				Path = Field<Project>(p=>p.Location),
			},
			Relation = GeoShapeRelation.Intersects
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoIndexedShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.IndexedShape(p=>p
					.Id(2)
					.Path(pp=>pp.Location)
				)
				.Relation(GeoShapeRelation.Intersects)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoIndexedShapeQuery>(a => a.GeoShape as IGeoIndexedShapeQuery)
		{
			q =>  q.Field = null,
			q =>  q.IndexedShape = null,
			q =>  q.IndexedShape.Id = null,
			q =>  q.IndexedShape.Index = null,
			q =>  q.IndexedShape.Type = null,
			q =>  q.IndexedShape.Path = null,
		};
	}
}
