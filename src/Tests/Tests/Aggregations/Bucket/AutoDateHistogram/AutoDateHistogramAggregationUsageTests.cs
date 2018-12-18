using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Aggregations.Bucket.AutoDateHistogram
{
	/**
	 * A multi-bucket aggregation similar to the Date Histogram Aggregation except instead of providing an interval to
	 * use as the width of each bucket, a target number of buckets is provided indicating the number of buckets needed
	 * and the interval of the buckets is automatically chosen to best achieve that target. The number of buckets
	 * returned will always be less than or equal to this target number.
	 *
	 * NOTE: When specifying a `format` **and** `extended_bounds` or `missing`, in order for Elasticsearch to be able to parse
	 * the serialized `DateTime` of `extended_bounds` or `missing` correctly, the `date_optional_time` format is included
	 * as part of the `format` value.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-autodatehistogram-aggregation.html[Auto Date Histogram Aggregation].
	*/
	public class AutoDateHistogramAggregationUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		public AutoDateHistogramAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				auto_date_histogram = new
				{
					field = "startedOn",
					format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time", //<1> Note the inclusion of `date_optional_time` to `format`
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

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.AutoDateHistogram("projects_started_per_month", date => date
				.Field(p => p.StartedOn)
				.Format("yyyy-MM-dd'T'HH:mm:ss")
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
			new AutoDateHistogramAggregation("projects_started_per_month")
			{
				Field = Field<Project>(p => p.StartedOn),
				Format = "yyyy-MM-dd'T'HH:mm:ss",
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
			/** ==== Handling responses
			* The `AggregateDictionary found on `.Aggregations` on `ISearchResponse<T>` has several helper methods
			* so we can fetch our aggregation results easily in the correct type.
			 * <<handling-aggregate-response, Be sure to read more about these helper methods>>
			*/
			response.ShouldBeValid();

			var dateHistogram = response.Aggregations.AutoDateHistogram("projects_started_per_month");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Interval.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();
			dateHistogram.Buckets.Count.Should().BeGreaterThan(1);
			foreach (var item in dateHistogram.Buckets)
			{
				item.Date.Should().NotBe(default);
				item.DocCount.Should().BeGreaterThan(0);

				var nested = item.Nested("project_tags");
				nested.Should().NotBeNull();

				var nestedTerms = nested.Terms("tags");
				nestedTerms.Buckets.Count.Should().BeGreaterThan(0);
			}
		}
	}
}
