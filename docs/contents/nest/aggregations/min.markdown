---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: min
---


# Min aggregation

Returns the minimum value among numeric values extracted from the aggregated documents.
## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Min("my_min_agg", m => m
				.Field(p => p.IntValues)
			)
		)
	)

	var agg = result.Aggs.Min("min_aggregation");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_min_agg", new AggregationContainer
				{
					Min = new MinAggregator
					{
						Field = "intValues"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Min("my_min_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-min-aggregation.html) for more information.