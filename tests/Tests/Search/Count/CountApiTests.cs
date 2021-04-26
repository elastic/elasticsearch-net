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
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Count
{
	public class CountApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CountResponse, ICountRequest, CountDescriptor<Project>, CountRequest<Project>>
	{
		public CountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new
				{
					name = new
					{
						query = "NEST"
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<CountDescriptor<Project>, ICountRequest> Fluent => c => c
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query("NEST")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CountRequest<Project> Initializer => new CountRequest<Project>()
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = "NEST"
			})
		};

		protected override string UrlPath => "/project/_count";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Count(f),
			(c, f) => c.CountAsync(f),
			(c, r) => c.Count(r),
			(c, r) => c.CountAsync(r)
		);
	}
}
