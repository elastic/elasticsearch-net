// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Aggregations.Bucket;

public class GeoDistanceAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public GeoDistanceAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.GeoDistance("rings_around_amsterdam", g => g
				.Field(p => p.LocationPoint)
				.Origin(GeoLocation.LatitudeLongitude(new LatLonGeoLocation { Lat = 52.376, Lon = 4.894 }))
				.Ranges(
					r => r.To(100),
					r => r.From(100).To(300),
					r => r.From(300)
				)
			);

	protected override AggregationDictionary InitializerAggregations =>
		new GeoDistanceAggregation("rings_around_amsterdam")
		{
			Field = Infer.Field((Project p) => p.LocationPoint),
			Origin = "52.376, 4.894",
			Ranges = new List<AggregationRange>
				{
					new AggregationRange { To = 100 },
					new AggregationRange { From = 100, To = 300 },
					new AggregationRange { From = 300 }
				}
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		response.Aggregations.Count.Should().Be(1);

		var ringsAroundAmsterdam = response.Aggregations.GetGeoDistance("rings_around_amsterdam");
		ringsAroundAmsterdam.Should().NotBeNull();
		ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "*-100.0").Should().NotBeNull();
		ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "100.0-300.0").Should().NotBeNull();
		ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "300.0-*").Should().NotBeNull();
	}
}
