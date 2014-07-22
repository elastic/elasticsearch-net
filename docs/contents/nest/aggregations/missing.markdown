---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: missing
---


# Missing aggregation

## Description

A field data based single bucket aggregation, that creates a bucket of all documents in the current document set context that are missing a field value (effectively, missing a field or having the configured NULL value set). For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-missing-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Missing("missing_agg", m => m
						.Field(data = >data.Name))));

You can then access the result.Aggregations to get the data, i.e.

	var missingAgg = result.Aggs.Missing("missing_agg");
