---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: percentiles
---


# Percentiles aggregation

## Description

A multi-value metrics aggregation that calculates one or more percentiles over numeric values extracted from the aggregated documents. For more info, read the [docs]().

## Usage

	var result = client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Percentiles("percentiles_aggregation", percentile => percentile
					.Field("followers.age"))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.Percentiles("percentiles_aggregation");
