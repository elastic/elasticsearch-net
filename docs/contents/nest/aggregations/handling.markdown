---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: handling
---

# Aggregations

For a good overview of what aggregations are, see the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations.html) on the subject.

## Specifying Aggregations during Search

Adding aggregations to a search request is as simple as

	var result = client.Search<ElasticsearchProject>(s => s
			.Aggregations(a => a
				.Terms("my_agg", st => st
					.Field(o => o.Content)
					.Size(10)
					.ExecutionHint(TermsAggregationExecutionHint.Ordinals)
				)
			)
		);

The above can also be accomplished using the object initializer syntax...

	var searchRequest = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_agg", new AggregationContainer
						{
							Terms = new TermsAggregator 
							{ 
								Field = "content",
								Size = 10,
								ExecutionHint = TermsAggregationExecutionHint.Ordinals
							}
						}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(searchRequest);

### Getting to your aggregation

The result of the aggregations are accessed from the `Aggs` property of the response using the key that was specified on the request, which in the examples above would be `my_agg`:

	var myAgg = result.Aggs.Terms("my_agg");

Notice we executed a [terms aggregation](/nest/aggregations/terms.html), and on the response we had to retrieve our results from the `Terms` property of `Aggs`.  All aggregations work like this in NEST.  If `my_agg` was a [percentiles aggregation](/nest/aggregations/percentiles.html) instead, we would have to extract the results from `Percentiles`

	var myAgg = results.Aggs.Percentiles("my_agg");

Or if it were a [geohash grid aggregation](/nest/aggregations/geohash-grid.html) we would retrieve it from `GeoHash`

	var myAgg = results.Aggs.GeoHash("my_agg")

etc...

Since aggregation response structures fall into similar groups, each aggregation response in NEST is typed to a specific implementation of `IAggregationMetric`.  This can be a `ValueMetric`, `SingleBucket`, `Bucket<T>`, `BucketWithDocCount<T>`, and the list goes on.  The `Aggs.<Aggregation>` property of the response will automatically convert to the response from ES to the correct type.

## Sub-aggregations

NEST of course also supports sub-aggregations...

#### Fluent

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.Terms("my_agg", st => st
				.Field(o => o.Content)
				.Size(10)
				.ExecutionHint(TermsAggregationExecutionHint.Ordinals)
				.Aggregations(aa => aa
					.Max("my_sub_agg", m =>  m
						.Field(o => o.LongValue)
					)
				)
			)
		)
	);

#### OIS

	var searchRequest = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_agg", new AggregationContainer
						{
							Terms = new TermsAggregator 
							{ 
								Field = "content",
								Size = 10,
								ExecutionHint = TermsAggregationExecutionHint.Ordinals
							},
							Aggregations = new Dictionary<string, IAggregationContainer>
							{
								{ "my_sub_agg", new AggregationContainer
									{
										Max = new MaxAggregator
										{
											Field = "longValue"
										}
									}
								}
							}
						}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(searchRequest);

Accessing the top level aggregation and sub-aggregation is as simple as

	var myAgg = result.Aggs.Terms("my_agg");
	var mySubAgg = myAgg.Aggs.Max("my_sub_agg");

That's aggregations in a nutshell. See the specific sections on each aggregation for more details.
