// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;

namespace Tests.Aggregations.Bucket;

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
				//extended_bounds = new
				//{
				//	min = FixedDate.AddYears(-1),
				//	max = FixedDate.AddYears(1)
				//},
				missing = "2015-06-06T12:01:02.1230000"
			},
			aggregations = new
			{
				project_tags = new
				{
					nested = new
					{
						path = "tags"
					},
					aggregations = new
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

	protected override Action<AggregationContainerDescriptor<Project>> FluentAggs => a => a
		.DateHistogram("projects_started_per_month", date => date
			.Field(p => p.StartedOn)
			.CalendarInterval(CalendarInterval.Month)
			.MinDocCount(2)
			.Format("yyyy-MM-dd'T'HH:mm:ss||date_optional_time")
			//.ExtendedBounds(FixedDate.AddYears(-1), FixedDate.AddYears(1))
			.Order(new HistogramOrder { Count = SortOrder.Asc })
			.Missing("2015-06-06T12:01:02.1230000")
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
			CalendarInterval = CalendarInterval.Month,
			MinDocCount = 2,
			Format = "yyyy-MM-dd'T'HH:mm:ss||date_optional_time",
			//ExtendedBounds = new ExtendedBounds<DateMath>
			//{
			//	Minimum = FixedDate.AddYears(-1),
			//	Maximum = FixedDate.AddYears(1),
			//},
			Order = new HistogramOrder { Count = SortOrder.Asc }, // TODO: Not compatible with existiing NEST
			Missing = "2015-06-06T12:01:02.1230000", // TODO: Implement Missing accepting a date!
			Aggregations = new NestedAggregation("project_tags")
			{
				Path = Field<Project>(p => p.Tags),
				Aggregations = new TermsAggregation("tags")
				{
					Field = Field<Project>(p => p.Tags.First().Name)
				}
			}
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();

		var dateHistogram = response.Aggregations.DateHistogram("projects_started_per_month");
		dateHistogram.Should().NotBeNull();
		//dateHistogram.Buckets.Should().NotBeNull();
		//dateHistogram.Buckets.Count.Should().BeGreaterThan(10);
		//foreach (var item in dateHistogram.Buckets)
		//{
		//	item.Date.Should().NotBe(default(DateTime));
		//	item.DocCount.Should().BeGreaterThan(0);

		//	var nested = item.Nested("project_tags");
		//	nested.Should().NotBeNull();

		//	var nestedTerms = nested.Terms("tags");
		//	nestedTerms.Buckets.Count.Should().BeGreaterThan(0);
		//}
	}
}
