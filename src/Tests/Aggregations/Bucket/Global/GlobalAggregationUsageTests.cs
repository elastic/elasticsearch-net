using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Global
{
	public class GlobalAggregationUsageTests : AggregationUsageTestBase
	{
		public GlobalAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				all_projects = new
				{
					global = new {},
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
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Global("all_projects", g => g
					.Aggregations(aa => aa
						.Terms("names", t => t
							.Field(p => p.Name)
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new GlobalAggregation("all_projects")
				{
					Aggregations = new TermsAggregation("names")
					{
						Field = Field<Project>(p => p.Name)
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var allProjects = response.Aggs.Global("all_projects");
			allProjects.Should().NotBeNull();
			var names = allProjects.Terms("names");
			names.Should().NotBeNull();
		}
	}
}