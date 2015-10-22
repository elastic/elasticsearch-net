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

namespace Tests.Aggregations.Bucket.Filters
{
	/**
	 * Defines a multi bucket aggregations where each bucket is associated with a filter. 
	 * Each bucket will collect all documents that match its associated filter.
	 *
	 * Be sure to read the elasticsearch documentation {ref}/search-aggregations-bucket-filters-aggregation.html[on this subject here]
	*/

	/** == Named filters **/
	public class FiltersAggregationUsageTests : AggregationUsageTestBase
	{
		public FiltersAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				projects_by_state = new
				{
					filters = new
					{
						filters = new
						{
							belly_up = new { term = new { state = new { value = "BellyUp" } } },
							stable = new { term = new { state = new { value = "Stable" } } },
							very_active = new { term = new { state = new { value = "VeryActive" } } },
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
				.Filters("projects_by_state", agg => agg
					.NamedFilters(filters => filters
						.Filter("belly_up", f => f.Term(p => p.State, StateOfBeing.BellyUp))
						.Filter("stable", f => f.Term(p => p.State, StateOfBeing.Stable))
						.Filter("very_active", f => f.Term(p => p.State, StateOfBeing.VeryActive))
					)
					.Aggregations(childAggs => childAggs
						.Terms("project_tags", avg => avg.Field(p => p.CuratedTags.First().Name))
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new FiltersAgg("projects_by_state")
				{
					Filters = new NamedFiltersContainer
					{
							{ "belly_up", Query<Project>.Term(p=>p.State, StateOfBeing.BellyUp) },
							{ "stable", Query<Project>.Term(p=>p.State, StateOfBeing.Stable) },
							{ "very_active", Query<Project>.Term(p=>p.State, StateOfBeing.VeryActive) }
					},
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
		var filterAgg = response.Aggs.Filters("projects_by_state");
		filterAgg.Should().NotBeNull();

		var namedResult = filterAgg.NamedBucket("belly_up");
		namedResult.Should().NotBeNull();
		namedResult.DocCount.Should().BeGreaterThan(0);

		namedResult = filterAgg.NamedBucket("stable");
		namedResult.Should().NotBeNull();
		namedResult.DocCount.Should().BeGreaterThan(0);

		namedResult = filterAgg.NamedBucket("very_active");
		namedResult.Should().NotBeNull();
		namedResult.DocCount.Should().BeGreaterThan(0);

	});
	}

	/** == Anonymous filters **/

	public class AnonymousUsage : AggregationUsageTestBase
	{
		public AnonymousUsage(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				projects_by_state = new
				{
					filters = new
					{
						filters = new[] {
								new { term = new { state = new { value = "BellyUp" } }},
								new { term = new { state = new { value = "Stable" } }},
								new { term = new { state = new { value = "VeryActive" } }},
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
				.Filters("projects_by_state", agg => agg
					.AnonymousFilters(
						f => f.Term(p => p.State, StateOfBeing.BellyUp),
						f => f.Term(p => p.State, StateOfBeing.Stable),
						f => f.Term(p => p.State, StateOfBeing.VeryActive)
					)
					.Aggregations(childAggs => childAggs
						.Terms("project_tags", avg => avg.Field(p => p.CuratedTags.First().Name))
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new FiltersAgg("projects_by_state")
				{
					Filters = new List<IQueryContainer>
					{
							 Query<Project>.Term(p=>p.State, StateOfBeing.BellyUp) ,
							 Query<Project>.Term(p=>p.State, StateOfBeing.Stable) ,
							 Query<Project>.Term(p=>p.State, StateOfBeing.VeryActive)
					},
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
			var filterAgg = response.Aggs.Filters("projects_by_state");
			filterAgg.Should().NotBeNull();
			var results = filterAgg.AnonymousBuckets();
			results.Count.Should().Be(3);
			foreach (var singleBucket in results)
			{
				singleBucket.DocCount.Should().BeGreaterThan(0);
			}
		});
	}
}