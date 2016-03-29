---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: significant-terms
---


# Significant Terms aggregation

An aggregation that returns interesting or unusual occurrences of terms in a set.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
			.Aggregations(a => a
			.SignificantTerms("my_sig_terms_agg", sa => sa
				.Field(p => p.Content)
			)
		)
	);

	var agg = result.Aggs.SignificantTerms("my_sig_terms_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_sig_terms_agg", new AggregationContainer
				{
					SignificantTerms = new SignificantTermsAggregator
					{
						Field = "content"
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);
	
	var agg = result.Aggs.SignificantTerms("my_sig_terms_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-significantterms-aggregation.html) for more information.
