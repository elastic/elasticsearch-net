using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.DateHistogram
{
	/**
	 * A multi-bucket aggregation similar to the histogram except it can only be applied on date values. 
	 * From a functionality perspective, this histogram supports the same features as the normal histogram. 
	 * The main difference is that the interval can be specified by date/time expressions.
	 *
	 * When both format and extended_bounds are specified, the `date_optional_time` format is included
	 * as part of the format value so that Elasticsearch to be able to parse
	 * the serialized DateTimes of extended_bounds correctly.
	 *
	 * Be sure to read the elasticsearch documentation {ref}/search-aggregations-bucket-datehistogram-aggregation.html[on this subject here]
	*/
	public class DateHistogramAggregationUsageTests : AggregationUsageTestBase
	{
		public DateHistogramAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			size = 0,
			aggs = new
			{
				projects_started_per_month = new
				{
					date_histogram = new
					{
						field = "startedOn",
						interval = "month",
						min_doc_count = 2,
						format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time",
						order = new { _count = "asc" },
						extended_bounds = new
						{
							min = FixedDate.AddYears(-1),
							max = FixedDate.AddYears(1)
						},
						missing = FixedDate
					},
					aggs = new
					{
						project_tags = new
						{
							nested = new
							{
								path = "tags"
							},
							aggs = new
							{
								tags = new
								{
									terms = new { field = "tags.name" }
								}
							}
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(aggs => aggs
				.DateHistogram("projects_started_per_month", date => date
					.Field(p => p.StartedOn)
					.Interval(DateInterval.Month)
					.MinimumDocumentCount(2)
					.Format("yyyy-MM-dd'T'HH:mm:ss")
					.ExtendedBounds(FixedDate.AddYears(-1), FixedDate.AddYears(1))
					.Order(HistogramOrder.CountAscending)
					.Missing(FixedDate)
					.Aggregations(childAggs => childAggs
						.Nested("project_tags", n => n
							.Path(p => p.Tags)
							.Aggregations(nestedAggs => nestedAggs
								.Terms("tags", avg => avg.Field(p => p.Tags.First().Name))
							)
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Size = 0,
				Aggregations = new DateHistogramAggregation("projects_started_per_month")
				{
					Field = Field<Project>(p => p.StartedOn),
					Interval = DateInterval.Month,
					MinimumDocumentCount = 2,
					Format = "yyyy-MM-dd'T'HH:mm:ss",
					ExtendedBounds = new ExtendedBounds<DateTime>
					{
						Minimum = FixedDate.AddYears(-1),
						Maximum = FixedDate.AddYears(1),
					},
					Order = HistogramOrder.CountAscending,
					Missing = FixedDate,
					Aggregations = new NestedAggregation("project_tags")
					{
						Path = Field<Project>(p => p.Tags),
						Aggregations = new TermsAggregation("tags")
						{
							Field = Field<Project>(p => p.Tags.First().Name)
						}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			/**
			* Using the `.Agg` aggregation helper we can fetch our aggregation results easily 
			* in the correct type. [Be sure to read more about `.Agg` vs `.Aggregations` on the response here]()
			*/
			var dateHistogram = response.Aggs.DateHistogram("projects_started_per_month");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();
			dateHistogram.Buckets.Count.Should().BeGreaterThan(10);
			foreach (var item in dateHistogram.Buckets)
			{
				item.Date.Should().NotBe(default(DateTime));
				item.DocCount.Should().BeGreaterThan(0);

				var nested = item.Nested("project_tags");
				nested.Should().NotBeNull();

				var nestedTerms = nested.Terms("tags");
				nestedTerms.Buckets.Count.Should().BeGreaterThan(0);
			}
		}
	}
}
