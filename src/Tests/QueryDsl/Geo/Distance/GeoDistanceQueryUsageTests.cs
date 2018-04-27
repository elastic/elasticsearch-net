using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.Distance
{
	public class GeoDistanceQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_distance = new
			{
				_name = "named_query",
				boost = 1.1,
				distance = "200.0m",
				optimize_bbox = "memory",
				distance_type = "arc",
				validation_method = "ignore_malformed",
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
			Location = new GeoLocation(34,-34),
			Distance = "200.0m",
#pragma warning disable 618
			OptimizeBoundingBox = GeoOptimizeBBox.Memory,
#pragma warning restore 618
			ValidationMethod = GeoValidationMethod.IgnoreMalformed
		};

#pragma warning disable 618 // use of GeoOptimizeBBox
		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistance(g=>g
				.Boost(1.1)
				.Name("named_query")
				.Field(p=>p.Location)
				.DistanceType(GeoDistanceType.Arc)
				.Location(34, -34)
				.Distance("200.0m")
				.Optimize(GeoOptimizeBBox.Memory)
				.ValidationMethod(GeoValidationMethod.IgnoreMalformed)
			);
#pragma warning restore 618

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoDistanceQuery>(a => a.GeoDistance)
		{
			q => q.Distance = null,
			q =>  q.Field = null,
			q =>  q.Location = null
		};
	}
}
