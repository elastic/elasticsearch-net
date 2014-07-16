---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: geohash-grid
---


# Geohash Grid aggregation

## Description

A multi-bucket aggregation that works on geo_point fields and groups points into buckets that represent cells in a grid. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-geohashgrid-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.GeoHash("geohash", g => g
						.Field("origin")
						.GeoHashPrecision(GeoHashPrecision.Precision3))));

You can then access the result.Aggregations to get the data, i.e.

	var rangeAgg = result.Aggs.GeoHash("geohash");
