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
using static Nest.Property;
using static Tests.Framework.RoundTripper;

namespace Tests.Aggregations.Bucket
{
	public class FilterAggregation
	{
		/**
		 * Defines a single bucket of all the documents in the current document set context that match a specified filter. 
		 * Often this will be used to narrow down the current aggregation context to a specific set of documents.
		 *
		 * Be sure to read the elasticsearch documentation {ref}/search-aggregations-bucket-filter-aggregation.html[on this subject here]
		*/
		public class Usage : AggregationUsageBase
		{
			public Usage(ReadOnlyCluster i, ApiUsage usage) : base(i, usage) { }

			protected override object ExpectJson => new
			{
				aggs = new
				{
					projects_date_ranges = new
					{
						filter = new {
							term = new Dictionary<string, object>
							{
								{ "leadDeveloper.firstName", new { value = "bethel" }}
							}
						},
                        aggs = new
						{
							project_tags = new { terms = new { field = "tags" } }
						}
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs => aggs
					.Filter("projects_date_ranges", date => date
						.Filter(q=>q.Term(p=>p.LeadDeveloper.FirstName, "bethel"))
						.Aggregations(childAggs => childAggs
							.Terms("project_tags", avg => avg.Field(p => p.Tags))
						)
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new FilterAgg("projects_date_ranges")
					{
						Filter = new TermQuery { Field = Field<Project>(p=>p.LeadDeveloper.FirstName), Value = "bethel"},
						Aggregations =
							new TermsAgg("project_tags") { Field = Field<Project>(p => p.Tags) }
					}
				};

			[I] public async Task HandlingResponses() => await this.AssertOnAllResponses(response =>
			{
				response.IsValid.Should().BeTrue();

				/**
				* Using the `.Agg` aggregation helper we can fetch our aggregation results easily 
				* in the correct type. [Be sure to read more about `.Agg` vs `.Aggregations` on the response here]()
				*/
				var filterAgg = response.Aggs.Filter("projects_date_ranges");
		});
		}
	}
}
