---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: geohash-grid
---


# Geohash Grid aggregation

A multi-bucket aggregation that works on [geo_point](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-geo-point-type.html) fields and groups points into buckets that represent cells in a grid.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.GeoHash("my_geohash_agg", g => g
				.Field(p => p.Origin)
				.GeoHashPrecision(GeoHashPrecision.Precision3)
			)
		)
	);

	var agg = result.Aggs.GeoHash("my_geohash_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_geohash_agg", new AggregationContainer
				{
					GeoHash = new GeoHashAggregator
					{
						Field = "origin",
						Precision = GeoHashPrecision.Precision3
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.GeoHash("my_geohash_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-geohashgrid-aggregation.html) for more information.