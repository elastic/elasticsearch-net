---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: filter
---


# Filter aggregation

Defines a single bucket of all the documents in the current document set context that match a specified filter.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Filter("my_filter_agg", f => f
				.Filter(fd => fd
					.Range(r => r
						.Greater(12000)
						.OnField(p => p.LOC)
					)
				)
				.Aggregations(agg => agg
					.Average("my_avg_agg", avg => avg
						.Field(p => p.LOC)
					)
				)
			)
		)
	);

	var filterAgg = result.Aggs.Filter("my_filter_agg");
	var avgAgg = filterAgg.Average("my_avg_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_filter_agg", new AggregationContainer
				 {
					 Filter = new FilterAggregator
					 {
						Filter = new FilterContainer(new RangeFilter
								 {
									 Field = "loc",
									 GreaterThan = "12000"
								 })
					 },
					 Aggregations = new Dictionary<string, IAggregationContainer>
					 {
						 { "my_avg_agg", new AggregationContainer
							 {
								 Average = new AverageAggregator
								 {
									 Field = "loc"
								 }
							 }
						 }
					 }
				 }
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var filterAgg = result.Aggs.Filter("my_filter_agg");
	var avgAgg = filterAgg.Average("my_avg_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-filter-aggregation.html) for more information.