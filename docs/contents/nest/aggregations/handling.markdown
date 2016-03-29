---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: handling
---

# Aggregations

For a good overview of what aggregations are, refer the [original Elasticsearch docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations.html) on the subject.

## Specifying Aggregations during Search

Adding aggregations to a search request is as simple as:

#### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
			.Aggregations(a => a
				.Terms("my_agg", st => st
					.Field(o => o.Content)
					.Size(10)
					.ExecutionHint(TermsAggregationExecutionHint.Ordinals)
				)
			)
		);

#### Object Initializer Syntax

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

The result of the aggregations are accessed from the `Aggs` property of the response using the key that was specified on the request, `my_agg`, in the above examples:

	var myAgg = result.Aggs.Terms("my_agg");

Notice we executed a [terms aggregation](/nest/aggregations/terms.html), and on the response we had to retrieve our results from the `Terms` property of `Aggs`.  All aggregations work like this in NEST.

If `my_agg` was a [percentiles aggregation](/nest/aggregations/percentiles.html) instead, we would have to extract the results from `Aggs.Percentiles`

	var myAgg = results.Aggs.Percentiles("my_agg");

Or if it were a [geohash grid aggregation](/nest/aggregations/geohash-grid.html) we would retrieve it from `Aggs.GeoHash`

	var myAgg = results.Aggs.GeoHash("my_agg")

etc...

Since aggregation response structures all fall into similar groups, each aggregation response in NEST is typed to a specific implementation of `IAggregationMetric`.  This can be a `ValueMetric`, `SingleBucket`, `Bucket`, `BucketWithDocCount`, and the list goes on.  The `Aggs` helper property of the response will automatically convert to the response from ES to the correct CLR type.

## Sub-aggregations

NEST of course also supports sub-aggregations.

In the following example we are executing a [terms aggregation](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-terms-aggregation.html), `names`, as a top-level aggregation, and then within it a [max aggregation](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-max-aggregation.html), `max_age`, as a sub-aggregation.  This will produce a bucket per unique value of the `Name` field, and within each bucket find the max `Age` value for that particular name.

#### Fluent Syntax

	var result = client.Search<Person>(s => s
		.Aggregations(a => a
			.Terms("names", st => st
				.Field(o => o.Name)
				.Size(10)
				.Aggregations(aa => aa
					.Max("max_age", m =>  m
						.Field(o => o.Age)
					)
				)
			)
		)
	);

#### Object Initializer Syntax

	var searchRequest = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "names", new AggregationContainer
						{
							Terms = new TermsAggregator 
							{ 
								Field = "name",
								Size = 10
							},
							Aggregations = new Dictionary<string, IAggregationContainer>
							{
								{ "max_age", new AggregationContainer
									{
										Max = new MaxAggregator
										{
											Field = "age"
										}
									}
								}
							}
						}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(searchRequest);

To access the `max_age` sub-aggregation, we first extract the top-level terms aggregation, `names`, from the response:

	var names = result.Aggs.Terms("names");

We can then iterate over each `name` bucket and extract our `max_age` result:

	foreach(var name in names.Items)
	{
		var maxAge = name.Aggs.Max("max_age");
	}

That's aggregations in a nutshell.  Refer to the specific section on each aggregation type for more details.