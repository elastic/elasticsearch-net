---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: terms
---


# Terms aggregation

## Description

A multi-bucket value source based aggregation where buckets are dynamically built - one per unique value. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-terms-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Terms("country_count", term => term
						.Field("country"))));

You can then access the result.Aggregations to get the data, i.e.

	var countryCount = result.Aggs.Terms("country_count");
