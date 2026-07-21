// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Metric;

public class GeoBoundsAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public GeoBoundsAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.GeoBounds("viewport", gb => gb
			.Field(p => p.LocationPoint)
			.WrapLongitude()
		);

	protected override AggregationDictionary InitializerAggregations =>
		new GeoBoundsAggregation("viewport", Infer.Field<Project>(p => p.LocationPoint))
		{
			WrapLongitude = true
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		response.Aggregations.Count.Should().Be(1);

		var viewport = response.Aggregations.GetGeoBounds("viewport");
		viewport.Should().NotBeNull();
		viewport.Bounds.Should().NotBeNull();

		viewport.Bounds.IsTopLeftBottomRight.Should().BeTrue();
		viewport.Bounds.TryGetTopLeftBottomRight(out var topLeftBottomRight).Should().BeTrue();
		topLeftBottomRight.BottomRight.IsLatitudeLongitude.Should().BeTrue();
		topLeftBottomRight.BottomRight.TryGetLatitudeLongitude(out var bottomRightLatLon).Should().BeTrue();
		topLeftBottomRight.TopLeft.IsLatitudeLongitude.Should().BeTrue();
		topLeftBottomRight.TopLeft.TryGetLatitudeLongitude(out var topLeftLatLon).Should().BeTrue();

		GeoLocation.IsValidLatitude(bottomRightLatLon.Lat).Should().BeTrue();
		GeoLocation.IsValidLongitude(bottomRightLatLon.Lon).Should().BeTrue();
		GeoLocation.IsValidLatitude(topLeftLatLon.Lat).Should().BeTrue();
		GeoLocation.IsValidLongitude(topLeftLatLon.Lon).Should().BeTrue();
	}
}

