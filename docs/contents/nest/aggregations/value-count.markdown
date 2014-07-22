---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: value-count
---


# Value Count aggregation

## Description

A single-value metrics aggregation that counts the number of values that are extracted from the aggregated documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-valuecount-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.ValueCount("valuecount_aggregation", value => value
					.Field("followers.age"))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.ValueCount("valuecount_aggregation");
