using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;
using static Tests.Framework.RoundTripper;

namespace Tests.Aggregations.Bucket.Filter
{
	/**
	 * Defines a single bucket of all the documents in the current document set context that match a specified filter. 
	 * Often this will be used to narrow down the current aggregation context to a specific set of documents.
	 *
	 * Be sure to read the elasticsearch documentation {ref}/search-aggregations-bucket-filter-aggregation.html[on this subject here]
	*/
	public class FilterAggregationUsageTests : AggregationUsageTestBase
	{
		public FilterAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		public static string FirstNameToFind = Project.Projects.First().LeadDeveloper.FirstName.ToLowerInvariant();

		protected override object ExpectJson => new
		{
			aggs = new
			{
				bethels_projects = new
				{
					filter = new
					{
						term = new Dictionary<string, object>
							{
								{ "leadDeveloper.firstName", new { value = FirstNameToFind }}
							}
					},
					aggs = new
					{
						project_tags = new { terms = new { field = "curatedTags.name" } }
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.Filter("bethels_projects", date => date
					.Filter(q => q.Term(p => p.LeadDeveloper.FirstName, FirstNameToFind))
					.Aggregations(childAggs => childAggs
						.Terms("project_tags", avg => avg.Field(p => p.CuratedTags.First().Name))
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new FilterAgg("bethels_projects")
				{
					Filter = new TermQuery { Field = Field<Project>(p => p.LeadDeveloper.FirstName), Value = FirstNameToFind },
					Aggregations =
						new TermsAgg("project_tags") { Field = Field<Project>(p => p.CuratedTags.First().Name) }
				}
			};

		[I]
		public async Task HandlingResponses() => await this.AssertOnAllResponses(response =>
	{
		response.IsValid.Should().BeTrue();

			/**
			* Using the `.Agg` aggregation helper we can fetch our aggregation results easily 
			* in the correct type. [Be sure to read more about `.Agg` vs `.Aggregations` on the response here]()
			*/
		var filterAgg = response.Aggs.Filter("bethels_projects");
		filterAgg.Should().NotBeNull();
		filterAgg.DocCount.Should().BeGreaterThan(0);
		var tags = filterAgg.Terms("project_tags");
		tags.Should().NotBeNull();
		tags.Items.Should().NotBeEmpty();
	});
	}
}
