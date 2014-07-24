---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: cardinality
---


# Cardinality aggregation

A single-value metrics aggregation that calculates an approximate count of distinct values.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Cardinality("my_cardinality_agg", c => c
				.Field(p => p.Followers.First().FirstName)
			)
		)
	);

	var agg = result.Aggs.Cardinality("my_cardinality_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_cardinality_agg", new AggregationContainer
				 {
					 Cardinality = new CardinalityAggregator
					 {
						 Field = "followers.firstName"
					 }
				 }
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Cardinality("my_cardinality_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-cardinality-aggregation.html) for more information.