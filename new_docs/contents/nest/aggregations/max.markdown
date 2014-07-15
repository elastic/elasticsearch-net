---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: max
---


# Max aggregation

## Description

Returns the maximum value among numeric values extracted from the aggregated documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-max-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Max("max_aggregation", max => max
						.Field(data => data.IntValues))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggregations["max_aggregation"] as ValueMetric;
