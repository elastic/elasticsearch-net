using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.ReverseNested
{
	public class ReverseNestedAggregationUsageTests : AggregationUsageTestBase
	{
		public ReverseNestedAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			tags = new
			{
				nested = new { path = "tags", },
				aggs = new
				{
					tag_names = new
					{
						terms = new { field = "tags.name" },
						aggs = new
						{
							tags_to_project = new
							{
								reverse_nested = new { },
								aggs = new
								{
									top_projects_per_tag = new
									{
										terms = new { field = "name" }
									}
								}
							}
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Nested("tags", n => n
				.Path(p => p.Tags)
				.Aggregations(aa => aa
					.Terms("tag_names", t => t
						.Field(p => p.Tags.Suffix("name"))
						.Aggregations(aaa => aaa
							.ReverseNested("tags_to_project", r => r
								.Aggregations(aaaa => aaaa
									.Terms("top_projects_per_tag", tt => tt
										.Field(p => p.Name)
									)
								)
							)
						)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new NestedAggregation("tags")
			{
				Path = "tags",
				Aggregations = new TermsAggregation("tag_names")
				{
					Field = "tags.name",
					Aggregations = new ReverseNestedAggregation("tags_to_project")
					{
						Aggregations = new TermsAggregation("top_projects_per_tag")
						{
							Field = Field<Project>(p => p.Name)
						}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var tags = response.Aggregations.Nested("tags");
			tags.Should().NotBeNull();
			var tagNames = tags.Terms("tag_names");
			tagNames.Should().NotBeNull();
			foreach(var tagName in tagNames.Buckets)
			{
				tagName.Key.Should().NotBeNullOrEmpty();
				tagName.DocCount.Should().BeGreaterThan(0);
				var tagsToProjects = tagName.ReverseNested("tags_to_project");
				tagsToProjects.Should().NotBeNull();
				var topProjectsPerTag = tagsToProjects.Terms("top_projects_per_tag");
				topProjectsPerTag.Should().NotBeNull();
				foreach(var topProject in topProjectsPerTag.Buckets)
				{
					topProject.Key.Should().NotBeNullOrEmpty();
					topProject.DocCount.Should().BeGreaterThan(0);
				}
			}
		}
	}
}
