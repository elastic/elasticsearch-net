---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: histogram
---


# Histogram aggregation

A multi-bucket values source based aggregation that can be applied on numeric values extracted from the documents.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Histogram("my_histogram_agg", h => h
				.Field(p => p.LOC)
				.Interval(100)
			)
		)
	);

	var agg = result.Aggs.Histogram("my_histogram_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_histogram_agg", new AggregationContainer
				{
					Histogram = new HistogramAggregator
					{
						Field = "loc",
						Interval = 100
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Histogram("my_histogram_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-histogram-aggregation.html) for more information.