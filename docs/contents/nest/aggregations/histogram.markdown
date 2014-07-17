---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: histogram
---


# Histogram aggregation

## Description

A multi-bucket values source based aggregation that can be applied on numeric values extracted from the documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-histogram-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Histogram("histogram", h => h
						.Field("loc")
						.Interval(100))));

You can then access the result.Aggregations to get the data, i.e.

	var rangeAgg = result.Aggs.Histogram("histogram");
