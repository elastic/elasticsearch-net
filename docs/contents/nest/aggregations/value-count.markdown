---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: value-count
---


# Value Count aggregation

A single-value metrics aggregation that counts the number of values that are extracted from the aggregated documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.ValueCount("my_valuecount_agg", v => v
				.Field(p => p.Followers.First().Age)
			)
		)
	);

	var agg = result.Aggs.ValueCount("my_valuecount_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_valuecount_agg", new AggregationContainer
				{
					ValueCount = new ValueCountAggregator
					{
						Field = "followers.age"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.ValueCount("my_valuecount_agg");
	
Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-valuecount-aggregation.html) for more information.