// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Aggregations.Bucket.DateHistogram
{
	/**
	 * A multi-bucket aggregation similar to the histogram except it can only be applied on date values.
	 * From a functionality perspective, this histogram supports the same features as the normal histogram.
	 * The main difference is that the interval can be specified by date/time expressions.
	 *
	 * NOTE: When specifying a `format` **and** `extended_bounds`, `hard_bounds` or `missing`, in order for Elasticsearch to be able to parse
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
					calendar_interval = "month",
					min_doc_count = 2,
					format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time", // <1> Note the inclusion of `date_optional_time` to `format`
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
		};

#pragma warning disable 618, 612
		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_month", date => date
				.Field(p => p.StartedOn)
				.CalendarInterval(DateInterval.Month)
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
				CalendarInterval = DateInterval.Month,
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
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			/** ==== Handling responses
			* The `AggregateDictionary found on `.Aggregations` on `SearchResponse<T>` has several helper methods
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

	// hide
	public class DateHistogramAggregationNoSubAggregationsUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		public DateHistogramAggregationNoSubAggregationsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_four_weeks = new
			{
				date_histogram = new
				{
					field = "startedOn",
					fixed_interval = "28d",
					min_doc_count = 2,
					format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time",
					order = new { _count = "asc" },
					extended_bounds = new
					{
						min = FixedDate.AddYears(-1),
						max = FixedDate.AddYears(1)
					},
					missing = FixedDate
				}
			}
		};

#pragma warning disable 618, 612
		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_four_weeks", date => date
				.Field(p => p.StartedOn)
				.FixedInterval(new Time(28, TimeUnit.Day))
				.MinimumDocumentCount(2)
				.Format("yyyy-MM-dd'T'HH:mm:ss")
				.ExtendedBounds(FixedDate.AddYears(-1), FixedDate.AddYears(1))
				.Order(HistogramOrder.CountAscending)
				.Missing(FixedDate)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_four_weeks")
			{
				Field = Field<Project>(p => p.StartedOn),
				FixedInterval = new Time(28, TimeUnit.Day),
				MinimumDocumentCount = 2,
				Format = "yyyy-MM-dd'T'HH:mm:ss",
				ExtendedBounds = new ExtendedBounds<DateMath>
				{
					Minimum = FixedDate.AddYears(-1),
					Maximum = FixedDate.AddYears(1),
				},
				Order = HistogramOrder.CountAscending,
				Missing = FixedDate
			};
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var dateHistogram = response.Aggregations.DateHistogram("projects_started_per_four_weeks");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();
			dateHistogram.Buckets.Count.Should().BeGreaterThan(10);
			foreach (var item in dateHistogram.Buckets)
			{
				item.Date.Should().NotBe(default(DateTime));
				item.DocCount.Should().BeGreaterThan(0);
			}
		}
	}

	// hide
	[SkipVersion("<7.10.0", "hard_bounds introduced in 7.10.0")]
	public class DateHistogramAggregationWithHardBoundsUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		private readonly DateTime _hardBoundsMinimum;
		private readonly DateTime _hardBoundsMaximum;
		
		public DateHistogramAggregationWithHardBoundsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage)
		{
			// Note: If these tests are run against an existing node, and seeding is not forced, it's possible the
			// dates used will not appear in the index and result in no buckets being returned. The test will still
			// pass if this is the case. For best results locally, force a reseed. This is not an issue in CI.

			var projects = Project.Projects.OrderBy(p => p.StartedOn).Skip(2).Take(5).ToArray();
			
			_hardBoundsMinimum = projects.Min(p => p.StartedOn.Date);
			_hardBoundsMaximum = projects.Max(p => p.StartedOn.Date);
		}

		protected override object AggregationJson => new
		{
			projects_started_per_day = new
			{
				date_histogram = new
				{
					field = "startedOn",
					calendar_interval = "day",
					format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time",
					min_doc_count = 1,
					hard_bounds = new
					{
						min = _hardBoundsMinimum,
						max = _hardBoundsMaximum
					},
					order = new { _key = "asc" },
				}
			}
		};

#pragma warning disable 618, 612
		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_day", date => date
				.Field(p => p.StartedOn)
				.Format("yyyy-MM-dd'T'HH:mm:ss")
				.CalendarInterval(DateInterval.Day)
				.HardBounds(_hardBoundsMinimum, _hardBoundsMaximum)
				.MinimumDocumentCount(1)
				.Order(HistogramOrder.KeyAscending)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_day")
			{
				Field = Field<Project>(p => p.StartedOn),
				Format = "yyyy-MM-dd'T'HH:mm:ss",
				CalendarInterval = DateInterval.Day,
				HardBounds = new HardBounds<DateMath>
				{
					Minimum = _hardBoundsMinimum,
					Maximum = _hardBoundsMaximum
				},
				MinimumDocumentCount = 1,
				Order = HistogramOrder.KeyAscending
			};
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var dateHistogram = response.Aggregations.DateHistogram("projects_started_per_day");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();

			foreach (var date in dateHistogram.Buckets.Select(b => DateTime.Parse(b.KeyAsString)))
				date.Should().BeOnOrAfter(_hardBoundsMinimum).And.BeOnOrBefore(_hardBoundsMaximum);
		}
	}
}
