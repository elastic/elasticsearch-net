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
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.GeoTileGrid
{
	public class GeoTileGridAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public GeoTileGridAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			my_geotile = new
			{
				geotile_grid = new
				{
					field = "locationPoint",
					precision = 3,
					size = 1000,
					shard_size = 100
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoTile("my_geotile", g => g
				.Field(p => p.LocationPoint)
				.Precision(GeoTilePrecision.Precision3)
				.Size(1000)
				.ShardSize(100)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoTileGridAggregation("my_geotile")
			{
				Field = Field<Project>(p => p.LocationPoint),
				Precision = GeoTilePrecision.Precision3,
				Size = 1000,
				ShardSize = 100
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var myGeoTileGrid = response.Aggregations.GeoTile("my_geotile");
			myGeoTileGrid.Should().NotBeNull();
			var firstBucket = myGeoTileGrid.Buckets.First();
			firstBucket.Key.Should().NotBeNullOrWhiteSpace();
			firstBucket.DocCount.Should().BeGreaterThan(0);
		}
	}
}
