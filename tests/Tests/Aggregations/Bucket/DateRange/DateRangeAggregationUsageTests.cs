// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Aggregations.Bucket.DateRange
{
	/**
	 * A range aggregation that is dedicated for date values. The main difference between this aggregation and the normal range aggregation is that the `from`
	 * and `to` values can be expressed in `DateMath` expressions, and it is also possible to specify a date format by which the from and
	 * to response fields will be returned.
	 *
	 * IMPORTANT: this aggregation includes the `from` value and excludes the `to` value for each range.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-daterange-aggregation.html[Date Range Aggregation]
	*/
	public class DateRangeAggregationUsageTests : AggregationUsageTestBase
	{
		public DateRangeAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			projects_date_ranges = new
			{
				date_range = new
				{
					field = "startedOn",
					ranges = new object[]
					{
						new { to = "now", from = "2015-06-06T12:01:02.123||+2d" },
						new { to = "now+1d-30m/h" },
						new { from = "2012-05-05||+1d-1m" },
					},
					time_zone = "CET"
				},
				aggs = new
				{
					project_tags = new { terms = new { field = "tags" } }
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateRange("projects_date_ranges", date => date
				.Field(p => p.StartedOn)
				.Ranges(
					r => r.From(DateMath.Anchored(FixedDate).Add("2d")).To(DateMath.Now),
					r => r.To(DateMath.Now.Add(TimeSpan.FromDays(1)).Subtract("30m").RoundTo(DateMathTimeUnit.Hour)),
					r => r.From(DateMath.Anchored("2012-05-05").Add(TimeSpan.FromDays(1)).Subtract("1m"))
				)
				.TimeZone("CET")
				.Aggregations(childAggs => childAggs
					.Terms("project_tags", avg => avg.Field(p => p.Tags))
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateRangeAggregation("projects_date_ranges")
			{
				Field = Field<Project>(p => p.StartedOn),
				Ranges = new List<DateRangeExpression>
				{
					new DateRangeExpression { From = DateMath.Anchored(FixedDate).Add("2d"), To = DateMath.Now },
					new DateRangeExpression { To = DateMath.Now.Add(TimeSpan.FromDays(1)).Subtract("30m").RoundTo(DateMathTimeUnit.Hour) },
					new DateRangeExpression { From = DateMath.Anchored("2012-05-05").Add(TimeSpan.FromDays(1)).Subtract("1m") }
				},
				TimeZone = "CET",
				Aggregations =
					new TermsAggregation("project_tags") { Field = Field<Project>(p => p.Tags) }
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			/** ==== Handling Responses
			* The `AggregateDictionary found on `.Aggregations` on `SearchResponse<T>` has several helper methods
			* so we can fetch our aggregation results easily in the correct type.
			 * <<handling-aggregate-response, Be sure to read more about these helper methods>>
			*/
			response.ShouldBeValid();

			var dateHistogram = response.Aggregations.DateRange("projects_date_ranges");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();

			/** We specified three ranges so we expect to have three of them in the response */
			dateHistogram.Buckets.Count.Should().Be(3);
			foreach (var item in dateHistogram.Buckets) item.DocCount.Should().BeGreaterThan(0);
		}
	}
}
