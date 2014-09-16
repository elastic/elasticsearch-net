---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: missing
---


# Missing aggregation

A field data based single bucket aggregation, that creates a bucket of all documents in the current document set context that are missing a field value (effectively, missing a field or having the configured NULL value set).

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Missing("my_missing_agg", m => m
				.Field(p => p.Name)
			)
		)
	);

	var agg = result.Aggs.Missing("my_missing_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_missing_agg", new AggregationContainer
				{
					Missing = new MissingAggregator
					{
						Field = "name"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Missing("my_missing_agg");

 Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-missing-aggregation.html) for more information.