---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: global
---


# Global aggregation

## Description

Defines a single bucket of all the documents within the search execution context. This context is defined by the indices and the document types you’re searching on, but is not influenced by the search query itself. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-global-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Match(m => m
						.OnField(f => f.Country)
						.Query("Malaysia")))
				.Aggregations(a => a
					.Global("global_bucket", descriptor => descriptor
						.Aggregations(aggr => aggr
							.Terms("bool_count", t => t
								.Field(data => data.BoolValue))))));

You can then access the result.Aggregations to get the data, i.e.

	var agg = result.Aggs.Global("global_bucket");
	var bools = agg.Terms("bool_count");
