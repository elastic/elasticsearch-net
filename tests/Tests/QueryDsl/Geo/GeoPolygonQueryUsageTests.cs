// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo;

public class GeoPolygonQueryUsageTests : QueryDslUsageTestsBase
{
	public GeoPolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override Query QueryInitializer => new GeoPolygonQuery
	{
		Boost = 1.1f,
		QueryName = "named_query",
		ValidationMethod = GeoValidationMethod.Strict,
		Polygon = new GeoPolygonPoints
		{
			Points = new[]
			{
				GeoLocation.LatitudeLongitude(new () { Lat = 45, Lon = -45 }),
				GeoLocation.LatitudeLongitude(new () { Lat = -34, Lon = -34 }),
				GeoLocation.LatitudeLongitude(new () { Lat = 70, Lon = -70 })
			}
		},
		Field = Infer.Field<Project>(p => p.LocationPoint),
		IgnoreUnmapped = true
	};

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor) => queryDescriptor
		.GeoPolygon(c => c
				.QueryName("named_query")
				.Boost(1.1f)
				.Field(p => p.LocationPoint)
				.ValidationMethod(GeoValidationMethod.Strict)
				.Polygon(p => p.Points(new[]
				{
					GeoLocation.LatitudeLongitude(new () { Lat = 45, Lon = -45 }),
					GeoLocation.LatitudeLongitude(new () { Lat = -34, Lon = -34 }),
					GeoLocation.LatitudeLongitude(new () { Lat = 70, Lon = -70 })
				}))
				.IgnoreUnmapped(true)
			);
}
