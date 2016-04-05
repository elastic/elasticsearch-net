using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Children
{
	/**
	 * A special single bucket aggregation that enables aggregating from buckets on parent document types to
	 * buckets on child documents.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-children-aggregation.html[Children Aggregation]
	 */
	public class ChildrenAggregationUsageTests : AggregationUsageTestBase
	{
		public ChildrenAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				name_of_child_agg = new
				{
					children = new { type = "commits" },
					aggs = new
					{
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

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.Children<CommitActivity>("name_of_child_agg", child => child
					.Aggregations(childAggs => childAggs
						.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
						.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					)
				)
			);

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
}
