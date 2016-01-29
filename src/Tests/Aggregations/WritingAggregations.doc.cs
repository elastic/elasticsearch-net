using System;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;
using System.Collections.Generic;

namespace Tests.Aggregations
{
	public class WritingAggregations
	{
		/**
		 * Aggregations are arguably one of the most powerful features of Elasticsearch.
		 * NEST allows you to write your aggregations using a strict fluent dsl, a verbatim object initializer 
		 * syntax that maps verbatim to the elasticsearch API & a more terse object initializer aggregation DSL. 
		 * 
		 * Three different ways, yikes thats a lot to take in! Lets go over them one by one and explain when you might
		 * want to use which one.
		 */
		public class Usage : UsageTestBase<ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
		{
			protected override object ExpectJson => new
			{
				aggs = new
				{
					name_of_child_agg = new
					{
						children = new { type = "commits" },
						aggs = new {
							average_per_child = new
							{
								avg = new { field = "confidenceFactor" }
							},
							max_per_child = new
							{
								max = new { field = "confidenceFactor" }
							}
						}
					}
				}
			};
			/**
			 * The fluent lambda syntax is the most terse way to write aggregations.
			 * It benefits from types that are carried over to sub aggregations
			 */
			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs=>aggs
					.Children<CommitActivity>("name_of_child_agg", child => child
						.Aggregations(childAggs=>childAggs
							.Average("average_per_child", avg=>avg.Field(p=>p.ConfidenceFactor))
							.Max("max_per_child", avg=>avg.Field(p=>p.ConfidenceFactor))
						)
					)
				);
			
			/**
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
							new AverageAggregation("average_per_child", "confidenceFactor") &&
							new MaxAggregation("max_per_child", "confidenceFactor")
					}
				};
		}

		public class MetaUsage : Usage
		{
			protected override object ExpectJson => new
			{
				aggs = new
				{
					my_terms_agg = new
					{
						terms = new
						{
							field = "name",
							meta = new
							{
								foo = "bar",
								count = 1
							}
						}
					},
					my_avg_agg = new
					{
						avg = new
						{
							field = "numberOfCommits",
							meta = new
							{
								foo = "bar",
								count = 1
							}
						}
					},
					my_derivative_agg = new
					{
						derivative = new
						{
							buckets_path = "my_avg_agg",
							meta = new
							{
								foo = "bar",
								count = 1
							}
						}
					}
				}
			};


			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs => aggs
					.Terms("my_terms_agg", t => t
						.Field(p => p.Name)
						.Meta(meta => meta
							.Add("foo", "bar")
							.Add("count", 1)
						)
					)
					.Average("my_avg_agg", avg => avg
						.Field(p => p.NumberOfCommits)
						.Meta(meta => meta
							.Add("foo", "bar")
							.Add("count", 1)
						)
					)
					.Derivative("my_derivative_agg", dv => dv
						.BucketsPath("my_avg_agg")
						.Meta(meta => meta
							.Add("foo", "bar")
							.Add("count", 1)
						)
					)
				);


			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new TermsAggregation("my_terms_agg")
					{
						Field = "name",
						Meta = new Dictionary<string, object>
						{
							{ "foo", "bar" },
							{ "count", 1 }
						}
					} 
					&& new AverageAggregation("my_avg_agg", "numberOfCommits")
					{
						Meta = new Dictionary<string, object>
						{
							{ "foo", "bar" },
							{ "count", 1 }
						}
					}
					&& new DerivativeAggregation("my_derivative_agg", "my_avg_agg")
					{
						Meta = new Dictionary<string, object>
						{
							{ "foo", "bar" },
							{ "count", 1 }
						}
					}
				};
		}
	}
}
