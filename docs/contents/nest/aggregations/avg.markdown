---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: avg
---


# Avg aggregation

A single-value metrics aggregation that computes the average of numeric values that are extracted from the aggregated documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Average("my_avg_agg", avg => avg
				.Field(p => p.Followers.First().Age)
			)
		)
	);

	var agg = result.Aggs.Avg("my_avg_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_avg_agg", new AggregationContainer
				 {
					 Terms = new TermsAggregator
					 {
						 Field = "followers.age"
					 }
				 }
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Avg("my_avg_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-avg-aggregation.html) for more information.