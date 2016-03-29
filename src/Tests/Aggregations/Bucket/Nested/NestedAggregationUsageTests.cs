using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Bucket.Nested
{
	public class NestedAggregationUsageTests : AggregationUsageTestBase
	{
		public NestedAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				tags = new
				{
					nested = new
					{
						path = "tags",
					},
					aggs = new
					{
						tag_names = new
						{
							terms = new
							{
								field = "tags.name"
							}
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Nested("tags", n => n
					.Path(p => p.Tags)
					.Aggregations(aa => aa
						.Terms("tag_names", t => t
							.Field(p => p.Tags.Suffix("name"))
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new NestedAggregation("tags")
				{
					Path = "tags",
					Aggregations = new TermsAggregation("tag_names")
					{
						Field = "tags.name"
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var tags = response.Aggs.Nested("tags");
			tags.Should().NotBeNull();
			var tagNames = tags.Terms("tag_names");
			tagNames.Should().NotBeNull();
			foreach(var item in tagNames.Buckets)
			{
				item.Key.Should().NotBeNullOrEmpty();
				item.DocCount.Should().BeGreaterThan(0);
			}
		}
	}
}