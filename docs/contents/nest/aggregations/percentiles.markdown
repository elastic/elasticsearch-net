---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: percentiles
---


# Percentiles aggregation

A multi-value metrics aggregation that calculates one or more percentiles over numeric values extracted from the aggregated documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Percentiles("my_percentiles_agg", p => p
				.Field(f => f.Followers.First().Age)
			)
		)
	);

	var agg = result.Aggs.Percentiles("my_percentiles_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_percentiles_agg", new AggregationContainer
				{
					Percentiles = new PercentilesAggregator
					{
						Field = "followers.age"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Percentiles("my_percentiles_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-percentile-aggregation.html) for more information.