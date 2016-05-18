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
	/**[float]
	*== Anonymous filters
	*/
	public class AnonymousFiltersAggregationUsageTests : AggregationUsageTestBase
	{
		public AnonymousFiltersAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			aggs = new
			{
				projects_by_state = new
				{
					meta = new
					{
						_type = "anonymous_filters"
					},
					filters = new
					{
						other_bucket = true,
						filters = new[] {
								new { term = new { state = new { value = "BellyUp" } }},
								new { term = new { state = new { value = "Stable" } }},
								new { term = new { state = new { value = "VeryActive" } }},
							}
					},
					aggs = new
					{
						project_tags = new
						{
							meta = new
							{
								_type = "terms"
							},
							terms = new { field = "curatedTags.name.keyword" }
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.AnonymousFilters("projects_by_state", agg => agg
					.OtherBucket()
					.Filters(
						f => f.Term(p => p.State, StateOfBeing.BellyUp),
						f => f.Term(p => p.State, StateOfBeing.Stable),
						f => f.Term(p => p.State, StateOfBeing.VeryActive)
					)
					.Aggregations(childAggs => childAggs
						.Terms("project_tags", avg => avg.Field(p => p.CuratedTags.First().Name.Suffix("keyword")))
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new AnonymousFiltersAggregation("projects_by_state")
				{
					OtherBucket = true,
					Filters = new List<QueryContainer>
					{
							 Query<Project>.Term(p=>p.State, StateOfBeing.BellyUp) ,
							 Query<Project>.Term(p=>p.State, StateOfBeing.Stable) ,
							 Query<Project>.Term(p=>p.State, StateOfBeing.VeryActive)
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

			var filterAgg = response.Aggs.AnonymousFilters("projects_by_state");
			filterAgg.Should().NotBeNull();
			var results = filterAgg.Buckets;
			results.Count.Should().Be(4);

			foreach (var singleBucket in results.Take(3))
			{
				singleBucket.DocCount.Should().BeGreaterThan(0);
			}

			results.Last().DocCount.Should().Be(0); // <1> The last bucket is the _other bucket_
		}

		/**[float]
		* == Conditionless Anonymous Filters */
		public class Conditionless : AggregationUsageTestBase
		{
			public Conditionless(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

			protected override bool SupportsDeserialization => false;

			protected override object ExpectJson => new
			{
				aggs = new
				{
					conditionless_filters = new
					{
						meta = new
						{
							_type = "anonymous_filters"
						},
						filters = new
						{
							filters = new object[] { }
						}
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs => aggs
					.AnonymousFilters("conditionless_filters", agg => agg
						.Filters(
							q => new QueryContainer()
						)
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new AnonymousFiltersAggregation("conditionless_filters")
					{
						Filters = new List<QueryContainer>
						{
						new QueryContainer()
						}
					}
				};

			protected override void ExpectResponse(ISearchResponse<Project> response)
			{
				response.IsValid.Should().BeTrue();
				response.Aggs.AnonymousFilters("conditionless_filters").Buckets.Should().BeEmpty();
			}
		}

		/**[float]
			* == Empty Anonymous Filters
			*/
		public class Empty : AggregationUsageTestBase
		{
			public Empty(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

			protected override bool SupportsDeserialization => false;

			protected override object ExpectJson => new
			{
				aggs = new
				{
					empty_filters = new
					{
						meta = new
						{
							_type = "anonymous_filters"
						},
						filters = new
						{
							filters = new object[] { }
						}
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs => aggs
					.AnonymousFilters("empty_filters", agg => agg
						.Filters()
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new AnonymousFiltersAggregation("empty_filters")
					{
						Filters = new List<QueryContainer>()
					}
				};

			protected override void ExpectResponse(ISearchResponse<Project> response)
			{
				response.IsValid.Should().BeTrue();
				response.Aggs.AnonymousFilters("empty_filters").Buckets.Should().BeEmpty();
			}
		}
	}
}
