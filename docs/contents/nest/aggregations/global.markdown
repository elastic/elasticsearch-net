---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: global
---


# Global aggregation

Defines a single bucket of all the documents within the search execution context. This context is defined by the indices and the document types you’re searching on, but is not influenced by the search query itself.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Query(q => q
			.Match(m => m
				.OnField(p => p.Country)
				.Query("Malaysia")))
		.Aggregations(a => a
			.Global("global_bucket", d => d
				.Aggregations(aa => aa
					.Terms("bool_count", t => t
						.Field(f => f.BoolValue)
					)
				)
			)
		)
	);

	var agg = result.Aggs.Global("global_bucket");
	var bools = agg.Terms("bool_count");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Query = new QueryContainer(new MatchQuery
				{
					Field = "country",
					Query = "Malaysia"
				}
		),
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "global_bucket", new AggregationContainer
				{
					Aggregations = new Dictionary<string, IAggregationContainer>
					{
						{ "bool_count", new AggregationContainer
							{
								Terms = new TermsAggregator
								{
									Field = "boolValue"
								}
							}
						}
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Global("global_bucket");
	var bools = agg.Terms("bool_count");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-global-aggregation.html) for more information.