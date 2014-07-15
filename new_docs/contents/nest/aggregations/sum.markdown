---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: sum
---


# Sum aggregation

## Description

A single-value metrics aggregation that sums up numeric values that are extracted from the aggregated documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-sum-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Sum("sum_aggregation", sum => sum
					.Field("followers.age"))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggregations["sum_aggregation"] as ValueMetric;
