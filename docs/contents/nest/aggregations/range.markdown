---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: range
---


# Range aggregation


A multi-bucket value source based aggregation that enables the user to define a set of ranges - each representing a bucket.

## Usage

### Fluent Syntax

	var result = _client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Range("my_range_agg", ra => ra
				.Field(p => p.LOC)
				.Ranges(
					r => r.To(11000),
					r => r.From(11000).To(12000),
					r => r.From(12000)
				)
			)
		)
	);

	var rangeAgg = result.Aggs.Range("my_range_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_range_agg", new AggregationContainer
				{
					Range = new RangeAggregator
					{
						Field = "followers.age",
						Ranges = new List<Range<double>>
						{
							new Range<double>().To(11000),
							new Range<double>().From(11000).To(12000),
							new Range<double>().From(12000)
						}
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.Range("my_range_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-range-aggregation.html) for more information.