using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.BoundingBox
{
	public class GeoBoundingBoxUsageTests : QueryDslUsageTestsBase
	{
		public GeoBoundingBoxUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_bounding_box = new
			{
				type = "indexed",
				coerce = true,
				ignore_malformed = true,
				validation_method = "strict",
				_name = "named_query",
				boost = 1.1,
				location = new
				{
					top_left = new
					{
						lat = 34.0,
						lon = -34.0
					},
					bottom_right = new
					{
						lat = -34.0,
						lon = 34.0
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoBoundingBoxQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p => p.Location),
			BoundingBox = new Nest.BoundingBox
			{
				TopLeft = new GeoLocation(34,-34),
				BottomRight = new GeoLocation(-34,34),
			},
			Type = GeoExecution.Indexed,
			Coerce = true,
			IgnoreMalformed = true,
			ValidationMethod = GeoValidationMethod.Strict
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoBoundingBox(g=>g
				.Boost(1.1)
				.Name("named_query")
				.Field(p=>p.Location)
				.BoundingBox(b=>b
					.TopLeft(34, -34)
					.BottomRight(-34, 34)
				)
				.Coerce()
				.IgnoreMalformed()
				.ValidationMethod(GeoValidationMethod.Strict)
				.Type(GeoExecution.Indexed)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoBoundingBoxQuery>(a => a.GeoBoundingBox)
		{
			q => q.BoundingBox = null,
			q => q.BoundingBox = new Nest.BoundingBox { } ,
			q =>  q.Field = null
		};
	}
}
