// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch.Experimental;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Tests.Aggregations.Metric;

public class GeoCentroidAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public GeoCentroidAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.GeoCentroid("centroid", gb => gb
			.Field(p => p.LocationPoint)
		);

	protected override AggregationDictionary InitializerAggregations =>
		new GeoCentroidAggregation("centroid", Infer.Field<Project>(p => p.LocationPoint));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		response.Aggregations.Count.Should().Be(1);

		var centroid = response.Aggregations.GetGeoCentroid("centroid");
		centroid.Should().NotBeNull();
		centroid.Count.Should().BeGreaterThan(0);
		centroid.Location.Should().NotBeNull();

		centroid.Location.IsLatitudeLongitude.Should().BeTrue();
		centroid.Location.TryGetLatitudeLongitude(out var centroidLatLon).Should().BeTrue();
		centroidLatLon.Lat.Should().NotBe(0);
		centroidLatLon.Lon.Should().NotBe(0);

		GeoLocation.IsValidLatitude(centroidLatLon.Lat).Should().BeTrue();
		GeoLocation.IsValidLongitude(centroidLatLon.Lon).Should().BeTrue();
	}
}

public class NestedGeoCentroidAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public NestedGeoCentroidAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	// The `geo_centroid` aggregation is more interesting when combined as a sub-aggregation to other bucket aggregations

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.Terms("projects", t => t
			.Field(p => p.Name)
			.Aggregations(sa => sa
				.GeoCentroid("centroid", gb => gb
					.Field(p => p.LocationPoint)
				)
			)
		);

	protected override AggregationDictionary InitializerAggregations =>
		new TermsAggregation("projects")
		{
			Field = Infer.Field<Project>(p => p.Name),
			Aggregations = new GeoCentroidAggregation("centroid", Infer.Field<Project>(p => p.LocationPoint))
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();

		var projects = response.Aggregations.GetTerms("projects");

		foreach (var bucket in projects.Buckets)
		{
			var centroid = bucket.GetGeoCentroid("centroid");

			centroid.Should().NotBeNull();
			centroid.Count.Should().BeGreaterThan(0);
			centroid.Location.Should().NotBeNull();

			centroid.Location.IsLatitudeLongitude.Should().BeTrue();
			centroid.Location.TryGetLatitudeLongitude(out var centroidLatLon).Should().BeTrue();
			centroidLatLon.Lat.Should().NotBe(0);
			centroidLatLon.Lon.Should().NotBe(0);

			GeoLocation.IsValidLatitude(centroidLatLon.Lat).Should().BeTrue();
			GeoLocation.IsValidLongitude(centroidLatLon.Lon).Should().BeTrue();
		}
	}
}

public class GeoCentroidNoResultsAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public GeoCentroidNoResultsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
			.GeoCentroid("centroid", gb => gb
				.Field(p => p.LocationPoint)
			);

	protected override AggregationDictionary InitializerAggregations =>
		new GeoCentroidAggregation("centroid", Infer.Field<Project>(p => p.LocationPoint));

	protected override Query QueryScope => new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "noresult" };

	protected override object QueryScopeJson { get; } = new { term = new { name = new { value = "noresult" } } };

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var centroid = response.Aggregations.GetGeoCentroid("centroid");
		centroid.Should().NotBeNull();
		centroid.Count.Should().Be(0);
	}
}
