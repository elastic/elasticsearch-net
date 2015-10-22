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

namespace Tests.Aggregations.Bucket.DateHistogram
{
	/**
	 * A multi-bucket aggregation similar to the histogram except it can only be applied on date values. 
	 * From a functionality perspective, this histogram supports the same features as the normal histogram. 
	 * The main difference is that the interval can be specified by date/time expressions.
	 *
	 * Be sure to read the elasticsearch documentation {ref}/search-aggregations-bucket-datehistogram-aggregation.html[on this subject here]
	*/
	public class DateHistogramAggregationUsageTests : AggregationUsageTestBase
	{
		public DateHistogramAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

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
						min_doc_count = 2,
						order = new { _count = "asc" },
						extended_bounds = new
						{
							min = FixedDate.AddYears(-1),
							max = FixedDate.AddYears(1)
						},
					},
					aggs = new
					{
						project_tags = new { terms = new { field = "tags" } }
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(aggs => aggs
				.DateHistogram("projects_started_per_month", date => date
					.Field(p => p.StartedOn)
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
					Field = Field<Project>(p => p.StartedOn),
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

		[I]
		public void HandlingResponses()
		{
			var response = this.Client.Search<Project>(s => s
				.Aggregations(aggs => aggs
					.DateHistogram("date_hist", dh => dh
						.Field(p => p.StartedOn)
						.Interval("2d")
						.MinimumDocumentCount(1)
					)
				)
			);

			response.IsValid.Should().BeTrue();

			/**
			* Using the `.Agg` aggregation helper we can fetch our aggregation results easily 
			* in the correct type. [Be sure to read more about `.Agg` vs `.Aggregations` on the response here]()
			*/
			var dateHistogram = response.Aggs.DateHistogram("date_hist");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Items.Should().NotBeNull();
			dateHistogram.Items.Count.Should().BeGreaterThan(10);
			foreach (var item in dateHistogram.Items)
			{
				item.Date.Should().NotBe(default(DateTime));
				item.DocCount.Should().BeGreaterThan(0);
			}
		}
	}
}
