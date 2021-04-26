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

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo.Distance
{
	public class GeoDistanceQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoDistanceQuery>(a => a.GeoDistance)
		{
			q => q.Distance = null,
			q => q.Field = null,
			q => q.Location = null
		};

		protected override QueryContainer QueryInitializer => new GeoDistanceQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p => p.LocationPoint),
			DistanceType = GeoDistanceType.Arc,
			Location = new GeoLocation(34, -34),
			Distance = "200m",
			ValidationMethod = GeoValidationMethod.IgnoreMalformed
		};

		protected override object QueryJson => new
		{
			geo_distance = new
			{
				_name = "named_query",
				boost = 1.1,
				distance = "200m",
				distance_type = "arc",
				validation_method = "ignore_malformed",
				locationPoint = new
				{
					lat = 34.0,
					lon = -34.0
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoDistance(g => g
				.Boost(1.1)
				.Name("named_query")
				.Field(p => p.LocationPoint)
				.DistanceType(GeoDistanceType.Arc)
				.Location(34, -34)
				.Distance("200m")
				.ValidationMethod(GeoValidationMethod.IgnoreMalformed)
			);
	}
}
