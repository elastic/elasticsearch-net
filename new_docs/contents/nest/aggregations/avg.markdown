---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: Avg
---


# Avg aggregation

## Description

A single-value metrics aggregation that computes the average of numeric values that are extracted from the aggregated documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-avg-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Average("avg_aggregation", avg => avg
					.Field("followers.age"))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.Average("avg_aggregation");
