using System;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;
using System.Collections.Generic;
using System.Linq;
using Tests.Aggregations.Bucket.Children;
using Tests.Framework.Integration;
using FluentAssertions;

namespace Tests.Aggregations
{
	/**
	*== Writing Aggregations
	* NEST allows you to write your aggregations using
	*
	* - a strict fluent DSL
	* - a verbatim object initializer syntax that maps verbatim to the Elasticsearch API
	* - a more terse object initializer aggregation DSL
	*
	* Three different ways, yikes that's a lot to take in! Lets go over them one by one and explain when you might
	* want to use each.
	*/
	public class Usage : UsageTestBase<ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		/**
		* This is the json output for each example
		**/
		protected override object ExpectJson => new
		{
			aggs = new
			{
				name_of_child_agg = new
				{
					children = new
					{
						type = "commits"
					},
					aggs = new
					{
						average_per_child = new
						{
							avg = new
							{
								field = "confidenceFactor"
							}
						},
						max_per_child = new
						{
							max = new
							{
								field = "confidenceFactor"
							}
						}
					}
				}
			}
		};

		/** === Fluent DSL
			* The fluent lambda syntax is the most terse way to write aggregations.
			* It benefits from types that are carried over to sub aggregations
			*/
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.Children<CommitActivity>("name_of_child_agg", child => child
					.Aggregations(childAggs => childAggs
						.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					)
				)
			);

		/** === Object Initializer syntax
			* The object initializer syntax (OIS) is a one-to-one mapping with how aggregations
			* have to be represented in the Elasticsearch API. While it has the benefit of being a one-to-one
			* mapping, being dictionary based in C# means it can grow exponentially in complexity rather quickly.
			*/
		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new ChildrenAggregation("name_of_child_agg", typeof(CommitActivity))
				{
					Aggregations =
						new AverageAggregation("average_per_child", "confidenceFactor")
						&& new MaxAggregation("max_per_child", "confidenceFactor")
				}
			};
	}

	public class AggregationDslUsage : Usage
	{
		/** === Terse Object Initializer DSL
			* For this reason the OIS syntax can be shortened dramatically by using `*Agg` related family,
			* These allow you to forego introducing intermediary Dictionaries to represent the aggregation DSL.
			* It also allows you to combine multiple aggregations using bitwise AND (`&&`) operator.
			*
			* Compare the following example with the previous vanilla OIS syntax
			*/
		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new ChildrenAggregation("name_of_child_agg", typeof(CommitActivity))
				{
					Aggregations =
						new AverageAggregation("average_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
						&& new MaxAggregation("max_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
				}
			};
	}

	public class AdvancedAggregationDslUsage : Usage
	{
		/** === Aggregating over a collection of aggregations
			* An advanced scenario may involve an existing collection of aggregation functions that should be set as aggregations
			* on the request. Using LINQ's `.Aggregate()` method, each function can be applied to the aggregation descriptor
			* (`childAggs` below) in turn, returning the descriptor after each function application.
			*
			*/
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent
		{
			get
			{
				var aggregations = new List<Func<AggregationContainerDescriptor<CommitActivity>, IAggregationContainer>> //<1> a list of aggregation functions to apply
				{
					a => a.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor)),
					a => a.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
				};

				return s => s
					.Aggregations(aggs => aggs
						.Children<CommitActivity>("name_of_child_agg", child => child
							.Aggregations(childAggs =>
								aggregations.Aggregate(childAggs, (acc, agg) => { agg(acc); return acc; }) // <2> Using LINQ's `Aggregate()` function to accumulate/apply all of the aggregation functions
							)
						)
					);
			}
		}
	}

	/**[[aggs-vs-aggregations]]
	*=== Aggs vs. Aggregations
	*
	* The response exposes both `.Aggregations` and `.Aggs` properties for handling aggregations. Why two properties you ask?
	* Well, the former is a dictionary of aggregation names to `IAggregate` types, a common interface for
	* aggregation responses (termed __Aggregates__ in NEST), and the latter is a convenience helper to get the right type
	* of aggregation response out of the dictionary based on a key name.
	*
	* This is better illustrated with an example
	*/
	public class AggsUsage : ChildrenAggregationUsageTests
	{
		public AggsUsage(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		/** Let's imagine we make the following request. */
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.Children<CommitActivity>("name_of_child_agg", child => child
					.Aggregations(childAggs => childAggs
						.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					)
				)
			);

		/**=== Aggs usage
		* Now, using `.Aggs`, we can easily get the `Children` aggregation response out and from that,
		* the `Average` and `Max` sub aggregations.
		*/
		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var childAggregation = response.Aggs.Children("name_of_child_agg");

			var averagePerChild = childAggregation.Average("average_per_child");

			averagePerChild.Should().NotBeNull(); //<1> Do something with the average per child. Here we just assert it's not null

			var maxPerChild = childAggregation.Max("max_per_child");

			maxPerChild.Should().NotBeNull(); //<2> Do something with the max per child. Here we just assert it's not null
		}
	}
}
