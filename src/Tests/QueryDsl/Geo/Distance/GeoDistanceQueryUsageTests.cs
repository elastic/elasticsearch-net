using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.Distance
{
	public class GeoDistanceUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_distance = new
			{
				_name = "named_query",
				boost = 1.1,
				distance = "200.0m",
				optimize_bbox = "memory",
				distance_type = "arc",
				coerce = true,
				ignore_malformed = true,
				validation_method = "strict",
				location = new
				{
					lat = 34.0,
					lon = -34.0
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoDistanceQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p => p.Location),
			DistanceType = GeoDistanceType.Arc,
			Coerce = true,
			Location = new GeoLocation(34,-34),
			Distance = "200.0m",
			IgnoreMalformed = true,
			OptimizeBoundingBox = GeoOptimizeBBox.Memory,
			ValidationMethod = GeoValidationMethod.Strict
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistance(g=>g
				.Boost(1.1)
				.Name("named_query")
				.Field(p=>p.Location)
				.DistanceType(GeoDistanceType.Arc)
				.Coerce()
				.Location(34, -34)
				.Distance("200.0m")
				.IgnoreMalformed()
				.Optimize(GeoOptimizeBBox.Memory)
				.ValidationMethod(GeoValidationMethod.Strict)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoDistanceQuery>(a => a.GeoDistance)
		{
			q => q.Distance = null,
			q =>  q.Field = null,
			q =>  q.Location = null
		};
	}
}
