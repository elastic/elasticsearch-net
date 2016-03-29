---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: nested
---


# Nested aggregation

A special single bucket aggregation that enables aggregating nested documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Nested("my_nested_agg", n => n
				.Path("contributors")
				.Aggregations(aa => aa
					.Average("my_avg_agg", avg => avg
						.Field(p => p.Contributors.First().Age)
					)
				)
			)
		)
	);

	var nestedAgg = result.Aggs.Nested("my_nested_agg");
	var avgAgg = nestedAgg.Average("my_ avg_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_nested_agg", new AggregationContainer
				{
					Nested = new NestedAggregator
					{
						Path = "contributors"
					},
					Aggregations = new Dictionary<string, IAggregationContainer>
					{
						{ "my_avg_agg", new AggregationContainer 
							{
								Average = new AverageAggregator
								{
									Field = "contributors.age"
								}
							}
						}
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var nestedAgg = result.Aggs.Nested("my_nested_agg");
	var avgAgg = nestedAgg.Average("my_avg_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-nested-aggregation.html) for more information.