---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: date-histogram
---


# Date Histogram aggregation

## Description

A multi-bucket aggregation similar to the histogram except it can only be applied on date values. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-datehistogram-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.DateHistogram("date_histogram", h => h
						.Field("startedOn")
						.Interval("month"))));

You can then access the result.Aggregations to get the data, i.e.

	var rangeAgg = result.Aggs.DateHistogram("date_histogram");
