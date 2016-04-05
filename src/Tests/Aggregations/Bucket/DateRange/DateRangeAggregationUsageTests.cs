using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

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

		protected override object ExpectJson => new
		{
			aggs = new
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
						}
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
				.DateRange("projects_date_ranges", date => date
					.Field(p => p.StartedOn)
					.Ranges(
						r => r.From(DateMath.Anchored(FixedDate).Add("2d")).To(DateMath.Now),
						r => r.To(DateMath.Now.Add(TimeSpan.FromDays(1)).Subtract("30m").RoundTo(TimeUnit.Hour)),
						r => r.From(DateMath.Anchored("2012-05-05").Add(TimeSpan.FromDays(1)).Subtract("1m"))
					)
					.Aggregations(childAggs => childAggs
						.Terms("project_tags", avg => avg.Field(p => p.Tags))
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new DateRangeAggregation("projects_date_ranges")
				{
					Field = Field<Project>(p => p.StartedOn),
					Ranges = new List<DateRangeExpression>
					{
						new DateRangeExpression { From = DateMath.Anchored(FixedDate).Add("2d"), To = DateMath.Now},
						new DateRangeExpression { To = DateMath.Now.Add(TimeSpan.FromDays(1)).Subtract("30m").RoundTo(TimeUnit.Hour) },
						new DateRangeExpression { From = DateMath.Anchored("2012-05-05").Add(TimeSpan.FromDays(1)).Subtract("1m") }
					},
					Aggregations =
						new TermsAggregation("project_tags") { Field = Field<Project>(p => p.Tags) }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			/** === Handling Responses
			* Using the `.Agg` aggregation helper we can fetch our aggregation results easily
			* in the correct type. <<aggs-vs-aggregations, Be sure to read more about .Aggs vs .Aggregations>>
			*/
			response.IsValid.Should().BeTrue();

			var dateHistogram = response.Aggs.DateRange("projects_date_ranges");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();

			/** We specified three ranges so we expect to have three of them in the response */
			dateHistogram.Buckets.Count.Should().Be(3);
			foreach (var item in dateHistogram.Buckets)
			{
				item.DocCount.Should().BeGreaterThan(0);
			}
		}
	}
}
