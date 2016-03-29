---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: stats
---


# Stats aggregation

A multi-value metrics aggregation that computes stats over numeric values extracted from the aggregated documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Stats("my_stats_agg", sa => sa
				.Field(p => p.Followers.First().Age)
			)
		)
	);

	var agg = result.Aggs.Stats("my_stats_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_stats_agg", new AggregationContainer
				{
					Stats = new StatsAggregator
					{
						Field = "followers.age"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);
	
	var agg = result.Aggs.Stats("my_stats_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-stats-aggregation.html) for more information.