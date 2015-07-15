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
	public class DateHistogramAggregation
	{
		/**
		 * A special single bucket aggregation that enables aggregating from buckets on parent document types to
		 * buckets on child documents.
		 */

		public class Usage : AggregationUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

			protected override object ExpectJson => new
			{
				aggs = new
				{
					name_of_child_agg = new
					{
						children = new {type = "commits"},
						aggs = new
						{
							average_per_child = new
							{
								avg = new {field = "confidenceFactor"}
							},
							max_per_child = new
							{
								max = new {field = "confidenceFactor"}
							}
						}
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs => aggs
					.DateHistogram("projects_started_per_month", date => date
						.Field(p=>p.StartedOn)
						.Interval(DateInterval.Month)
						.MinimumDocumentCount(2)
						.ExtendedBounds()
						.Aggregations(childAggs => childAggs
							.Terms("project_tags", avg => avg.Field(p => p.Tags))
						)
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new ChildrenAgg("name_of_child_agg", typeof (CommitActivity))
					{
						Aggregations =
							new AverageAgg("average_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
							&& new MaxAgg("max_per_child", Field<CommitActivity>(p => p.ConfidenceFactor))
					}
				};
		}
	}
}
