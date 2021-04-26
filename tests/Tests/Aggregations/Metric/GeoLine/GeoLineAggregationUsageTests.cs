/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.GeoLine
{
	/**
	 * The geo_line aggregation aggregates all geo_point values within a bucket into a LineString ordered by the chosen sort field. 
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-metrics-geo-line.html[Geo-Line Aggregation].
	*/
	[SkipVersion("<7.11.0", "Geo-line aggregation introduced in 7.11.0")]
	public class GeoLineAggregationUsageTests : AggregationUsageTestBase<XPackCluster>
	{
		public GeoLineAggregationUsageTests(XPackCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			line = new
			{
				geo_line = new
				{
					point = new
					{
						field = "locationPoint"
					},
					sort = new
					{
						field = "startedOn"
					},
					include_sort = true,
					size = 25
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoLine("line", d => d
				.Point(p => p.LocationPoint)
				.Sort(p => p.StartedOn)
				.IncludeSort()
				.Size(25));

		protected override AggregationDictionary InitializerAggs =>
			new GeoLineAggregation("line", Field<Project>(f => f.LocationPoint), Field<Project>(f => f.StartedOn))
			{
				IncludeSort = true,
				Size = 25
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var geoLine = response.Aggregations.GeoLine("line");
			geoLine.Should().NotBeNull();
			geoLine.Type.Should().Be("Feature");
			geoLine.Geometry.Type.Should().Be("linestring");
			geoLine.Geometry.Coordinates.Should().NotBeEmpty();
			geoLine.Properties.Complete.Should().BeFalse();
			geoLine.Properties.SortValues.Should().NotBeEmpty();
		}
	}
}
