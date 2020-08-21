// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
	public class GlobalAggregationUsageTests : AggregationUsageTestBase
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
