// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo;

public class GeoBoundingBoxQueryUsageTests : QueryDslUsageTestsBase
{
	public GeoBoundingBoxQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override Query QueryInitializer => new GeoBoundingBoxQuery
	{
		Boost = 1.25f,
		QueryName = "named_query",
		Field = Infer.Field<Project>(p => p.LocationPoint),
		BoundingBox = new TopLeftBottomRightGeoBounds
		{
			TopLeft = new LatLonGeoLocation { Lat = 34.0, Lon = -34 },
			BottomRight = new LatLonGeoLocation { Lat = -34.0, Lon = 34 }
		},
		ValidationMethod = GeoValidationMethod.Strict,
		IgnoreUnmapped = true
	};

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor) => queryDescriptor
		.GeoBoundingBox(g => g
			.Boost(1.25f)
			.QueryName("named_query")
			.Field(p => p.LocationPoint)
			.BoundingBox(new TopLeftBottomRightGeoBounds
			{
				TopLeft = new LatLonGeoLocation { Lat = 34.0, Lon = -34 },
				BottomRight = new LatLonGeoLocation { Lat = -34.0, Lon = 34 }
			})
			.ValidationMethod(GeoValidationMethod.Strict)
			.IgnoreUnmapped(true)
		);
}
