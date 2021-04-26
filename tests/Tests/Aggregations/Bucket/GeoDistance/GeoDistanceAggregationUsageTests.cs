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
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.GeoDistance
{
	public class GeoDistanceAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public GeoDistanceAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			rings_around_amsterdam = new
			{
				geo_distance = new
				{
					field = "locationPoint",
					origin = new
					{
						lat = 52.376,
						lon = 4.894
					},
					ranges = new object[]
					{
						new { to = 100.0 },
						new { from = 100.0, to = 300.0 },
						new { from = 300.0 }
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoDistance("rings_around_amsterdam", g => g
				.Field(p => p.LocationPoint)
				.Origin(52.376, 4.894)
				.Ranges(
					r => r.To(100),
					r => r.From(100).To(300),
					r => r.From(300)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoDistanceAggregation("rings_around_amsterdam")
			{
				Field = Field((Project p) => p.LocationPoint),
				Origin = "52.376, 4.894",
				Ranges = new List<AggregationRange>
				{
					new AggregationRange { To = 100 },
					new AggregationRange { From = 100, To = 300 },
					new AggregationRange { From = 300 }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var ringsAroundAmsterdam = response.Aggregations.GeoDistance("rings_around_amsterdam");
			ringsAroundAmsterdam.Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "*-100.0").Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "100.0-300.0").Should().NotBeNull();
			ringsAroundAmsterdam.Buckets.FirstOrDefault(r => r.Key == "300.0-*").Should().NotBeNull();
		}
	}
}
