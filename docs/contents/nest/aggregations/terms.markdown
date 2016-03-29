---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: terms
---


# Terms aggregation

## Description

A multi-bucket value source based aggregation where buckets are dynamically built - one per unique value.

## Usage

### Fluent Syntax

	var result = _client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Terms("my_terms_agg", t => t
				.Field(p => p.Country)
			)
		)
	);

	var agg = result.Aggs.Terms("my_terms_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_terms_agg", new AggregationContainer
				{
					Terms = new TermsAggregator
					{
						Field = "country"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);
	
	var agg = result.Aggs.Terms("my_terms_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-terms-aggregation.html) for more information.