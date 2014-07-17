---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: filter
---


# Filter aggregation

## Description

Defines a single bucket of all the documents in the current document set context that match a specified filter. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-filter-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Filter("filter_agg", f => f
						.Filter(fd => fd
							.Range(r => r
								.Greater(12000)
								.OnField(data => data.LOC)))
						.Aggregations(agg => agg
							.Average("avg_agg", avg => avg
								.Field(data => data.LOC))))));

You can then access the result.Aggregations to get the data, i.e.

	var filterAgg = result.Aggs.Filter("filter_agg");
	var avgAgg = filterAgg.Average("avg_agg");
