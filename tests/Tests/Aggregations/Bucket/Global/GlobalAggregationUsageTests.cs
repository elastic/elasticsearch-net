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
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Global
{
	public class GlobalAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public GlobalAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			all_projects = new
			{
				global = new { },
				aggs = new
				{
					names = new
					{
						terms = new
						{
							field = "name"
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Global("all_projects", g => g
				.Aggregations(aa => aa
					.Terms("names", t => t
						.Field(p => p.Name)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GlobalAggregation("all_projects")
			{
				Aggregations = new TermsAggregation("names")
				{
					Field = Field<Project>(p => p.Name)
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var allProjects = response.Aggregations.Global("all_projects");
			allProjects.Should().NotBeNull();
			var names = allProjects.Terms("names");
			names.Should().NotBeNull();
		}
	}
}
