using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Filters
{
	/**
	 * Defines a multi bucket aggregations where each bucket is associated with a filter.
	 * Each bucket will collect all documents that match its associated filter. For documents
	 * that do not match any filter, these will be collected in the _other bucket_.
	 *
	 * Be sure to read the Elasticsearch documentation {ref_current}/search-aggregations-bucket-filters-aggregation.html[Filters Aggregation]
	*/

	/**[float]
	* == Named filters
	*/
	public class NamedFiltersAggregationUsageTests : AggregationUsageTestBase
	{
		public NamedFiltersAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				projects_by_state = new
				{
					filters = new
					{
						other_bucket = true,
						other_bucket_key = "other_states_of_being",
						filters = new
						{
							belly_up = new { term = new { state = new { value = "BellyUp" } } },
							stable = new { term = new { state = new { value = "Stable" } } },
							very_active = new { term = new { state = new { value = "VeryActive" } } },
						}
					},
					aggs = new
					{
						project_tags = new { terms = new { field = "curatedTags.name.keyword" } }
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.NamedFilters("projects_by_state", agg => agg
					.OtherBucket()
					.OtherBucketKey("other_states_of_being")
					.Filters(filters => filters
						.Filter("belly_up", f => f.Term(p => p.State, StateOfBeing.BellyUp))
						.Filter("stable", f => f.Term(p => p.State, StateOfBeing.Stable))
						.Filter("very_active", f => f.Term(p => p.State, StateOfBeing.VeryActive))
					)
					.Aggregations(childAggs => childAggs
						.Terms("project_tags", avg => avg.Field(p => p.CuratedTags.First().Name.Suffix("keyword")))
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new NamedFiltersAggregation("projects_by_state")
				{
					OtherBucket = true,
					OtherBucketKey = "other_states_of_being",
					Filters = new NamedFiltersContainer
					{
							{ "belly_up", Query<Project>.Term(p=>p.State, StateOfBeing.BellyUp) },
							{ "stable", Query<Project>.Term(p=>p.State, StateOfBeing.Stable) },
							{ "very_active", Query<Project>.Term(p=>p.State, StateOfBeing.VeryActive) }
					},
					Aggregations =
						new TermsAggregation("project_tags") { Field = Field<Project>(p => p.CuratedTags.First().Name.Suffix("keyword")) }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			/** === Handling Responses
			* Using the `.Agg` aggregation helper we can fetch our aggregation results easily
			* in the correct type. <<aggs-vs-aggregations, Be sure to read more about .Aggs vs .Aggregations>>
			*/
			response.IsValid.Should().BeTrue();

			var filterAgg = response.Aggs.NamedFilters("projects_by_state");
			filterAgg.Should().NotBeNull();

			var namedResult = filterAgg.Buckets["belly_up"];
			namedResult.Should().NotBeNull();
			namedResult.DocCount.Should().BeGreaterThan(0);

			namedResult = filterAgg.Buckets["stable"];
			namedResult.Should().NotBeNull();
			namedResult.DocCount.Should().BeGreaterThan(0);

			namedResult = filterAgg.Buckets["very_active"];
			namedResult.Should().NotBeNull();
			namedResult.DocCount.Should().BeGreaterThan(0);

			namedResult = filterAgg.Buckets["other_states_of_being"];
			namedResult.Should().NotBeNull();
			namedResult.DocCount.Should().Be(0);
		}
	}
}
