using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using static Nest.Infer;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Aggregations.Bucket.DateHistogram
{
	/**
	 * A multi-bucket aggregation similar to the histogram except it can only be applied on date values.
	 * From a functionality perspective, this histogram supports the same features as the normal histogram.
	 * The main difference is that the interval can be specified by date/time expressions.
	 *
	 * NOTE: When specifying a `format` **and** `extended_bounds` or `missing`, in order for Elasticsearch to be able to parse
	 * the serialized `DateTime` of `extended_bounds` or `missing` correctly, the `date_optional_time` format is included
	 * as part of the `format` value.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-datehistogram-aggregation.html[Date Histogram Aggregation].
	*/
	public class DateHistogramAggregationUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		public DateHistogramAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				date_histogram = new
				{
					field = "startedOn",
					interval = "month",
					min_doc_count = 2,
					format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time", //<1> Note the inclusion of `date_optional_time` to `format`
					order = new {_count = "asc"},
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
								terms = new {field = "tags.name"}
							}
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
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
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = Field<Project>(p => p.StartedOn),
				Interval = DateInterval.Month,
				MinimumDocumentCount = 2,
				Format = "yyyy-MM-dd'T'HH:mm:ss",
				ExtendedBounds = new ExtendedBounds<DateMath>
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
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			/** === Handling responses
			* The `AggregateDictionary found on `.Aggregations` on `ISearchResponse<T>` has several helper methods
			* so we can fetch our aggregation results easily in the correct type.
			 * <<handling-aggregate-response, Be sure to read more about these helper methods>>
			*/
			response.ShouldBeValid();

			var dateHistogram = response.Aggregations.DateHistogram("projects_started_per_month");
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
