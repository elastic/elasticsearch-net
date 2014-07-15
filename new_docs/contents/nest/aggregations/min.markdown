---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: min
---


# Min aggregation

## Description

Returns the minimum value among numeric values extracted from the aggregated documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-min-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Min("min_aggregation", min => min
						.Field(data => data.IntValues))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.Min("min_aggregation");
