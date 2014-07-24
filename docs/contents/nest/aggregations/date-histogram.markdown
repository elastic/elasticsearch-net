---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: date-histogram
---


# Date Histogram aggregation

A multi-bucket aggregation similar to the histogram except it can only be applied on date values.

## Usage

### Fluent Syntax

	var result = _client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.DateHistogram("my_date_histogram", h => h
				.Field(p => p.StartedOn)
				.Interval("month")
			)
		)
	);

	var agg = result.Aggs.DateHistogram("my_date_histogram");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_date_histogram", new AggregationContainer
				 {
					 DateHistogram = new DateHistogramAggregator
					 {
						 Field = "startedOn",
						 Interval = "month"
					 }
				 }
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.DateHistogram("my_date_histogram");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-datehistogram-aggregation.html) for more information.
