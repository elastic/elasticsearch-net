using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Aggregations.Bucket.Children;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations
{
	/**=== Writing aggregations
	* NEST allows you to write your aggregations using
	*
	* - a strict fluent DSL
	* - a verbatim object initializer syntax that maps verbatim to the Elasticsearch API
	* - a more terse object initializer aggregation DSL
	*
	* Three different ways, yikes that's a lot to take in! Let's go over them one at a time and explain when you might
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
						},
						min_per_child = new
						{
							min = new
							{
								field = "confidenceFactor"
							}
						}
					}
				}
			}
		};

		/**[float]
		* === Fluent DSL
		* The fluent lambda syntax is the most terse way to write aggregations.
		* It benefits from types that are carried over to sub aggregations
		*/
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.Children<CommitActivity>("name_of_child_agg", child => child
					.Aggregations(childAggs => childAggs
						.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Min("min_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					)
				)
			);

		/**[float]
		* === Object Initializer syntax
		* The object initializer syntax (OIS) is a one-to-one mapping with how aggregations
		* have to be represented in the Elasticsearch API. While it has the benefit of being a one-to-one
		* mapping, being dictionary based in C# means it can gow verbose rather quickly.
		*
		* Here's the same aggregations as expressed in the Fluent API above, with the dictionary-based
		* object initializer syntax
		*/
		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new AggregationDictionary
				{
					{ "name_of_child_agg", new ChildrenAggregation("name_of_child_agg", typeof(CommitActivity))
						{
							Aggregations = new AggregationDictionary
							{
								{ "average_per_child", new AverageAggregation("average_per_child", "confidenceFactor") },
								{ "max_per_child", new MaxAggregation("max_per_child", "confidenceFactor") },
								{ "min_per_child", new MinAggregation("min_per_child", "confidenceFactor") },
							}
						}
					}
				}
			};

		/**
		 * This starts to get hard to read, wouldn't you agree? There is a better way however...
		 */
	}

	public class AggregationDslUsage : Usage
	{
		/**[float]
		* === Terse Object Initializer syntax
		* The Object Initializer syntax can be shortened dramatically by using `*Aggregation` types directly,
		* allowing you to forego the need to introduce intermediary dictionaries to represent the aggregation DSL.
		* In using these, it is also possible to combine multiple aggregations using the bitwise `&&` operator.
		*
		* Compare the following example with the previous vanilla Object Initializer syntax
		*/
		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new ChildrenAggregation("name_of_child_agg", typeof(CommitActivity))
				{
					Aggregations =
						new AverageAggregation("average_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
						&& new MaxAggregation("max_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
						&& new MinAggregation("min_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
				}
			};

		/** Now that's much cleaner! Assigning an `*Aggregation` type directly to the `Aggregation` property
		 *  on a search request works because there are implicit conversions within NEST to handle this for you.
		 */
	}

	public class AdvancedAggregationDslUsage : Usage
	{
		/**[float]
		* === Aggregating over a collection of aggregations
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
					a => a.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor)),
					a => a.Min("min_per_child", avg => avg.Field(p => p.ConfidenceFactor))
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

	public class AndMultipleDescriptorsUsage : Usage
	{
		/**
		* Combining multiple `AggregationDescriptor` is also possible using the bitwise `&&` operator
		*/
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent
		{
			get
			{
				var aggregations = new AggregationContainerDescriptor<CommitActivity>()
					.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					&& new AggregationContainerDescriptor<CommitActivity>()
						.Min("min_per_child", avg => avg.Field(p => p.ConfidenceFactor));

				return s => s
					.Aggregations(aggs => aggs
						.Children<CommitActivity>("name_of_child_agg", child => child
							.Aggregations(childAggs => aggregations)
						)
					);
			}
		}
	}
	/**[float]
	* [[aggs-vs-aggregations]]
	*=== Aggs vs. Aggregations
	*
	* The response exposes both `.Aggregations` and `.Aggs` properties for handling aggregations. Why two properties you ask?
	* Well, the former is a dictionary of aggregation names to `IAggregate` types, a common interface for
	* aggregation responses (termed __Aggregates__ in NEST), and the latter is a convenience helper to get the right type
	* of aggregation response out of the dictionary based on a key name.
	*
	* This is better illustrated with an example. Let's imagine we make the following request
	*/
	public class ChildrenAggregationFluentAggsUsageTests : ChildrenAggregationUsageTests
	{
		public ChildrenAggregationFluentAggsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.Children<CommitActivity>("name_of_child_agg", child => child
					.Aggregations(childAggs => childAggs
						.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Min("min_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					)
				)
			);

		/**
		* Now, using `.Aggs`, we can easily get the `Children` aggregation response out and from that,
		* the `Average` and `Max` sub aggregations.
		*/
		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var childAggregation = response.Aggs.Children("name_of_child_agg");

			var averagePerChild = childAggregation.Average("average_per_child");

			averagePerChild.Should().NotBeNull(); //<1> Do something with the average per child. Here we just assert it's not null

			var maxPerChild = childAggregation.Max("max_per_child");

			maxPerChild.Should().NotBeNull(); //<2> Do something with the max per child. Here we just assert it's not null
		}
	}
}
