---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: geo-distance
---


# Geo Distance aggregation

## Description

A multi-bucket aggregation that works on geo_point fields and conceptually works very similar to the range aggregation. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-geodistance-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.GeoDistance("geo_distance", g => g
						.Field("origin")
						.Origin("93.57, 93.57")
						.Ranges(
							r => r.To(100),
							r => r.From(100).To(300),
							r => r.From(300)
						))));

You can then access the result.Aggregations to get the data, i.e.

	var rangeAgg = result.Aggs.GeoDistance("geo_distance");
