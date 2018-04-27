using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.IndexedShape
{
	/**
	 * The GeoShape IndexedShape Query supports using a shape which has already been indexed in another index and/or index type within a geoshape query.
	 * This is particularly useful for when you have a pre-defined list of shapes which are useful to your application and you want to reference this
	 * using a logical name (for example __New Zealand__), rather than having to provide their coordinates within the request each time.
	 *
	 * See the Elasticsearch documentation on {ref_current}/query-dsl-geo-shape-query.html[geoshape queries] for more detail.
	 */
	public class GeoShapeIndexedShapeQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoShapeIndexedShapeQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name="named_query",
				boost = 1.1,
				location = new
				{
					indexed_shape = new
					{
						id = 2,
						type = "doc",
						index = "project",
						path = "location"
					},
					relation = "intersects"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoShapeQuery
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
			.GeoShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.IndexedShape(p=>p
					.Id(2)
					.Path(pp=>pp.Location)
				)
				.Relation(GeoShapeRelation.Intersects)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeQuery>(a => a.GeoShape)
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
