---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: date-range
---


# Date Range aggregation

A range aggregation that is dedicated for date values.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.DateRange("my_date_range_agg", d => d
				.Field(p => p.StartedOn)
				.Format("MM-yyy")
				.Ranges(
					r => r.To("now-10M/M"),
					r => r.From("now-10M/M")
				)
			)
		)
	);

	var agg = result.Aggs.DateRange("my_date_range_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_date_range_agg", new AggregationContainer
				 {
					 DateRange = new DateRangeAggregator
					 {
						 Field = "startedOn",
						 Format = "MM-yyy",
						 Ranges = new List<DateExpressionRange>
						 {
							 new DateExpressionRange().To("now-10M/M"),
							 new DateExpressionRange().From("now-10M/M")
						 }
					 }
				 }
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.DateRange("my_date_range_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-daterange-aggregation.html) for more information.
