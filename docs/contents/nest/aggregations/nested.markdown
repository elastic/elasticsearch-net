---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: nested
---


# Nested aggregation

## Description

For more info, read the [docs]().

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Nested("nested_agg", n => n
						.Path("contributors")
						.Aggregations(agg => agg
							.Average("avg_agg", avg => avg
								.Field("contributors.age"))))));

You can then access the result.Aggregations to get the data, i.e.

	var nestedAgg = result.Aggs.Nested("nested_agg");
	var avgAgg = nestedAgg.Average("avg_agg");
