---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: sum
---


# Sum aggregation

A single-value metrics aggregation that sums up numeric values that are extracted from the aggregated documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Sum("my_sum_agg", sa => sa
				.Field(p => p.Followers.First().Age)
			)
		)
	);

	var agg = result.Aggs.Sum("my_sum_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_sum_agg", new AggregationContainer
				{
					Sum = new SumAggregator
					{
						Field = "followers.age"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Sum("my_sum_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-sum-aggregation.html) for more information.