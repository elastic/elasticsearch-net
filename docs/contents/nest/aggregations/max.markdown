---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: max
---


# Max aggregation

Returns the maximum value among numeric values extracted from the aggregated documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Max("my_max_agg", m => m
				.Field(p => p.IntValues)
			)
		)
	);

	var agg = result.Aggs.Max("my_max_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_max_agg", new AggregationContainer
				{
					Max = new MaxAggregator
					{
						Field = "intValues"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Max("my_max_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-max-aggregation.html) for more information.
