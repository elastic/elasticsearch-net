---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: cardinality
---


# Cardinality aggregation

## Description

A single-value metrics aggregation that calculates an approximate count of distinct values. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-cardinality-aggregation.html).

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Cardinality("cardinality_aggregation", cardinality => cardinality
					.Field("followers.firstName"))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.Cardinality("cardinality_aggregation");
