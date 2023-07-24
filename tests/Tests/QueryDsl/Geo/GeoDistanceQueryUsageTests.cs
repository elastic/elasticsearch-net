// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo;

public class GeoDistanceQueryUsageTests : QueryDslUsageTestsBase
{
	public GeoDistanceQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
	{
	}

	protected override Query QueryInitializer => new GeoDistanceQuery
	{
		Boost = 1.25f,
		QueryName = "named_query",
		Field = Infer.Field<Project>(p => p.LocationPoint),
		DistanceType = GeoDistanceType.Arc,
		Location = GeoLocation.LatitudeLongitude(new() { Lat = 34, Lon = 34 }),
		Distance = "200m",
		ValidationMethod = GeoValidationMethod.IgnoreMalformed,
	};

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor) =>
		queryDescriptor
			.GeoDistance(g => g
				.Boost(1.25f)
				.QueryName("named_query")
				.Field(p => p.LocationPoint)
				.DistanceType(GeoDistanceType.Arc)
				.Location(GeoLocation.LatitudeLongitude(new() { Lat = 34, Lon = 34 }))
				.Distance("200m")
				.ValidationMethod(GeoValidationMethod.IgnoreMalformed));
}
