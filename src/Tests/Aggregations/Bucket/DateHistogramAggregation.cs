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
					projects_started_per_month = new
					{
						date_histogram = new
						{
							field = "startedOn",
							interval = "month",
							format = "MMM",
							min_doc_count = 2,
							order = new {_count = "asc"},
							extended_bounds = new
							{
								min = FixedDate.AddYears(-1),
								max = FixedDate.AddYears(1)
							},
						},
						aggs = new
						{
							project_tags = new {terms = new {field = "tags"}}
						}
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Aggregations(aggs => aggs
					.DateHistogram("projects_started_per_month", date => date
						.Field(p => p.StartedOn)
						.Format("MMM")
						.Interval(DateInterval.Month)
						.MinimumDocumentCount(2)
						.ExtendedBounds(FixedDate.AddYears(-1), FixedDate.AddYears(1))
						.Order(HistogramOrder.CountAscending)
						.Aggregations(childAggs => childAggs
							.Terms("project_tags", avg => avg.Field(p => p.Tags))
						)
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new DateHistogramAgg("projects_started_per_month")
					{
						Field = Field<Project>(p=>p.StartedOn),
						Format = "MMM",
						Interval = DateInterval.Month,
						MinimumDocumentCount = 2,
						ExtendedBounds = new ExtendedBounds<DateTime>
						{
							Minimum = FixedDate.AddYears(-1),
							Maximum = FixedDate.AddYears(1),
						},
						Order = HistogramOrder.CountAscending,
						Aggregations =
							new TermsAgg("project_tags") { Field = Field<Project>(p => p.Tags) }
					}
				};
		}
	}
}
