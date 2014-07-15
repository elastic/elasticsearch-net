---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: stats
---


# Stats aggregation

## Description

A multi-value metrics aggregation that computes stats over numeric values extracted from the aggregated documents. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-stats-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Stats("stats_aggregation", stats => stats
					.Field("followers.age"))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.Stats("stats_aggregation");
