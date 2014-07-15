---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: range
---


# Range aggregation

## Description

A multi-bucket value source based aggregation that enables the user to define a set of ranges - each representing a bucket. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-range-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Range("range_agg", range => range
						.Field("loc")
						.Ranges(
							r => r.To(11000),
							r => r.From(11000).To(12000),
							r => r.From(12000)
						))));

You can then access the result.Aggregations to get the data, i.e.

	var rangeAgg = result.Aggs.Range("range_agg");
