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
using static Nest.Property;
using static Tests.Framework.RoundTripper;

namespace Tests.Aggregations.Bucket
{
	public class DateRangeAggregation
	{
		/**
		 * A multi-bucket aggregation similar to the histogram except it can only be applied on date values. 
		 * From a functionality perspective, this histogram supports the same features as the normal histogram. 
		 * The main difference is that the interval can be specified by date/time expressions.
		 *
		 * Be sure to read the elasticsearch documentation {ref}/search-aggregations-bucket-datehistogram-aggregation.html[on this subject here]
		*/
		public class Usage : AggregationUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

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
								new { from = "now", to = "2015-06-06T12:01:02.123||+2d" },
								new { from = "now+1d-30m/h" },
								new { to = "2012-05-05||+1d-1m" },
							}
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
					.DateRange("projects_date_ranges", date => date
						.Field(p => p.StartedOn)
						.Ranges(
							r=>r.From(DateMath.Now).To(DateMath.Anchored(FixedDate).Add("2d")),
							r=>r.From(DateMath.Now.Add(TimeSpan.FromDays(1)).Subtract("30m").RoundTo(TimeUnit.Hour)),
							r=>r.To(DateMath.Anchored("2012-05-05").Add(TimeSpan.FromDays(1)).Subtract("1m"))
						)
						.Aggregations(childAggs => childAggs
							.Terms("project_tags", avg => avg.Field(p => p.Tags))
						)
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Aggregations = new DateRangeAgg("projects_date_ranges")
					{
						Field = Field<Project>(p=>p.StartedOn),
						Ranges = new List<DateRangeExpression>
						{
							{new DateRangeExpression { From = DateMath.Now, To = DateMath.Anchored(FixedDate).Add("2d") } },
							{new DateRangeExpression { From = DateMath.Now.Add(TimeSpan.FromDays(1)).Subtract("30m").RoundTo(TimeUnit.Hour) } },
							{new DateRangeExpression { To = DateMath.Anchored("2012-05-05").Add(TimeSpan.FromDays(1)).Subtract("1m") } }
						},
						Aggregations =
							new TermsAgg("project_tags") { Field = Field<Project>(p => p.Tags) }
					}
				};

			[I] public void HandlingResponses()
			{
				var response = this.GetClient().Search<Project>(s => s
					.Aggregations(aggs => aggs
						.DateRange("date_hist", dh => dh
							.Field(p => p.StartedOn)
						)
					)
				);

				response.IsValid.Should().BeTrue();
				
				/**
				* Using the `.Agg` aggregation helper we can fetch our aggregation results easily 
				* in the correct type. [Be sure to read more about `.Agg` vs `.Aggregations` on the response here]()
				*/
				var dateHistogram = response.Aggs.DateRange("date_hist");
				dateHistogram.Should().NotBeNull();
				dateHistogram.Items.Should().NotBeNull();
				dateHistogram.Items.Count.Should().BeGreaterThan(10);
				foreach (var item in dateHistogram.Items)
				{
					item.DocCount.Should().BeGreaterThan(0);
				}
			}
		}


		[U] public void UsingInterval()
		{
			/**
			* Time units are specified as a union of either a `DateInterval` or `TimeUnitExpression`
			* both of which implicitly convert to the `Union<,>` of these two.
			*/
			Expect("month").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Month);
			Expect("day").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Day);
			Expect("hour").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Hour);
			Expect("minute").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Minute);
			Expect("quarter").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Quarter);
			Expect("second").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Second);
			Expect("week").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Week);
			Expect("year").WhenSerializing<Union<DateInterval, TimeUnitExpression>>(DateInterval.Year);


			Expect("2d").WhenSerializing<Union<DateInterval, TimeUnitExpression>>((TimeUnitExpression)"2d");
			Expect("1.16w").WhenSerializing<Union<DateInterval, TimeUnitExpression>>((TimeUnitExpression)TimeSpan.FromDays(8.1));
		}
	}
}
