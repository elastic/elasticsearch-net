using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.DistanceRange
{
	public class GeoDistanceRangeUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceRangeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_distance_range = new
			{
				gt = "200.0km",
				gte = "200.0km",
				lt = "400.0mi",
				lte = "400.0mi",
				distance_type = "arc",
				optimize_bbox = "indexed",
				coerce = true,
				ignore_malformed = true,
				validation_method = "strict",
				_name = "named_query",
				boost = 1.1,
				location = new
				{
					lat = 40.0,
					lon = -70.0
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoDistanceRangeQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p=>p.Location),
			DistanceType = GeoDistanceType.Arc,
			Coerce = true,
			GreaterThanOrEqualTo = Nest.Distance.Kilometers(200),
			IgnoreMalformed = true,
			GreaterThan = Nest.Distance.Kilometers(200),
			LessThan = Nest.Distance.Miles(400),
			Location = new GeoLocation(40, -70),
			OptimizeBoundingBox = GeoOptimizeBBox.Indexed,
			LessThanOrEqualTo = Nest.Distance.Miles(400),
			ValidationMethod = GeoValidationMethod.Strict
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistanceRange(g=>g
				.Boost(1.1)
				.Name("named_query")
				.Field(p=>p.Location)
				.DistanceType(GeoDistanceType.Arc)
				.Coerce()
				.GreaterThanOrEqualTo(200, DistanceUnit.Kilometers)
				.GreaterThan(200, DistanceUnit.Kilometers)
				.IgnoreMalformed()
				.Location(new GeoLocation(40, -70))
				.Optimize(GeoOptimizeBBox.Indexed)
				.LessThanOrEqualTo(Nest.Distance.Miles(400))
				.LessThan(Nest.Distance.Miles(400))
				.ValidationMethod(GeoValidationMethod.Strict)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoDistanceRangeQuery>(a => a.GeoDistanceRange)
		{
			q => {
				q.LessThanOrEqualTo = null;
				q.LessThan = null;
				q.GreaterThanOrEqualTo = null;
				q.GreaterThan = null;
			},
			q =>  q.Field = null,
			q =>  q.Location = null
		};
	}
}
