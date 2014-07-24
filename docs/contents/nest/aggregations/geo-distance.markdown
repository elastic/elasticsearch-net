---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: geo-distance
---


# Geo Distance aggregation

A multi-bucket aggregation that works on geo_point fields and conceptually works very similar to the range aggregation. 

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.GeoDistance("my_geo_distance_agg", g => g
				.Field(p => p.Origin)
				.Origin("93.57, 93.57")
				.Ranges(
					r => r.To(100),
					r => r.From(100).To(300),
					r => r.From(300)
				)
			)
		)
	);

	var agg = result.Aggs.GeoDistance("my_geo_distance_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_geo_distance_agg", new AggregationContainer
				{
					GeoDistance = new GeoDistanceAggregator
					{
						Field = "origin",
						Origin = "93.57, 93.57",
						Ranges = new List<Range<double>>
						{
							new Range<double>().To(100),
							new Range<double>().From(100).To(300),
							new Range<double>().From(300)
						}
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.GeoDistance("my_geo_distance_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-geodistance-aggregation.html) for more information.