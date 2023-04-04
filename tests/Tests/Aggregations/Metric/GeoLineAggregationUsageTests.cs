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

// TODO - Security setup for XPack cluster seems to fail in 8.x

//public class GeoLineAggregationUsageTests : AggregationUsageWithVerifyTestBase<XPackCluster>
//{
//	public GeoLineAggregationUsageTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

//	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
//		.GeoLine("line", d => d
//			.Point(p => p.Field(f => f.LocationPoint))
//			.Sort(p => p.Field(f => f.StartedOn))
//			.IncludeSort()
//			.Size(25));

//	protected override AggregationDictionary InitializerAggregations =>
//		new GeoLineAggregation("line")
//		{
//			IncludeSort = true,
//			Point = new GeoLinePoint { Field = Infer.Field<Project>(p => p.LocationPoint) },
//			Sort = new GeoLineSort { Field = Infer.Field<Project>(p => p.StartedOn) },
//			Size = 25
//		};

//	protected override void ExpectResponse(SearchResponse<Project> response)
//	{
//		response.ShouldBeValid();
//		response.Aggregations.Count.Should().Be(1);

//		var geoLine = response.Aggregations.GetGeoLine("line");
//		geoLine.Should().NotBeNull();
//		geoLine.Type.Should().Be("Feature");
//		geoLine.Geometry.Type.Should().Be("linestring");
//		geoLine.Geometry.Coordinates.Should().NotBeEmpty();

//		//geoLine.Properties.Complete.Should().BeFalse(); // Needs review in ES spec
//		//geoLine.Properties.SortValues.Should().NotBeEmpty() // Needs review in ES spec
//	}
//}
