// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

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
			Field = Infer.Field<Project>(p => p.LocationPoint),
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
				locationPoint = new
				{
					lat = 34.0,
					lon = -34.0
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistance(g => g
				.Boost(1.1)
				.Name("named_query")
				.Field(p => p.LocationPoint)
				.DistanceType(GeoDistanceType.Arc)
				.Location(34, -34)
				.Distance("200m")
				.ValidationMethod(GeoValidationMethod.IgnoreMalformed)
			);
	}
}
