using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Geo.Distance
{
	public class GeoDistanceQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoDistanceQuery>(a => a.GeoDistance)
		{
			q => q.Distance = null,
			q => q.Field = null,
			q => q.Location = null
		};

		protected override QueryContainer QueryInitializer => new GeoDistanceQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p => p.Location),
			DistanceType = GeoDistanceType.Arc,
			Location = new GeoLocation(34, -34),
			Distance = "200m",
			ValidationMethod = GeoValidationMethod.IgnoreMalformed
		};

		protected override object QueryJson => new
		{
			geo_distance = new
			{
				_name = "named_query",
				boost = 1.1,
				distance = "200m",
				distance_type = "arc",
				validation_method = "ignore_malformed",
				location = new
				{
					lat = 34,
					lon = -34
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistance(g => g
				.Boost(1.1)
				.Name("named_query")
				.Field(p => p.Location)
				.DistanceType(GeoDistanceType.Arc)
				.Location(34, -34)
				.Distance("200m")
				.ValidationMethod(GeoValidationMethod.IgnoreMalformed)
			);
	}
}
