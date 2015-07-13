using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Property;

namespace Tests.Aggregations.Bucket
{
	public class ChildrenAggregation
	{
		/**
		 * A special single bucket aggregation that enables aggregating from buckets on parent document types to buckets on child documents.
		 */

		private void MappingExample()
		{
			/** To use the child aggregation you have to make sure 
			 *  a `_parent` mapping is in place, here we create the project
			 *  index with two mapped types, `project` and `commitactivity` and 
			 *  we add a `_parent` mapping from `commitactivity` to `parent` */
			var createProjectIndex = TestClient.GetClient().CreateIndex(c => c
				.Index<Project>()
				.AddMapping<Project>(m=>m.MapFromAttributes())
				.AddMapping<CommitActivity>(m=>m
					.SetParent<Project>()
				)
			);
		}

		public class Usage : AggregationUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

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
							}
						}
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs=>aggs
					.Children<CommitActivity>("name_of_child_agg", child => child
						.Aggregations(childAggs=>childAggs
							.Average("average_per_child", avg=>avg.Field(p=>p.ConfidenceFactor))
						)
					)
				);

			protected override SearchRequest<Project> Initializer => 
				new SearchRequest<Project>
				{
					Aggregations = new Dictionary<string, IAggregationContainer>
					{
						{"name_of_child_agg", new AggregationContainer
						{
							Children = new ChildrenAggregator
							{
								Type = typeof(CommitActivity)
							},
							Aggregations = new Dictionary<string, IAggregationContainer>
							{
								{"average_per_child", new AggregationContainer
								{
									Average = new AverageAggregator { Field = "confidenceFactor"}
								} }
							}
						}
						}
					}
				};
		}

		public class AggregationDslUsage : Usage
		{
			public AggregationDslUsage(ReadOnlyIntegration i) : base(i) { }

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new ChildrenAgg("name_of_child_agg", typeof(CommitActivity))
					{
						Aggregations = new AverageAgg("average_per_child", Field<CommitActivity>(p=>p.ConfidenceFactor))
					}
				};
		}
	}
}
