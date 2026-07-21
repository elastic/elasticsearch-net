---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/aggregations.html
---

# Aggregation examples [aggregations]

This page demonstrates how to use aggregations.

## Top-level aggreggation [_top_level_aggreggation]

### Fluent API [_fluent_api]

```csharp
var response = await client.SearchAsync<Person>(search => search
    .Indices("persons")
    .Query(query => query
        .MatchAll()
    )
    .Aggregations(aggregations => aggregations
        .Add("agg_name", aggregation => aggregation
            .Max(max => max
                .Field(x => x.Age)
            )
        )
    )
    .Size(10)
);
```

### Object initializer API [_object_initializer_api]

```csharp
var response = await client.SearchAsync<Person>(new SearchRequest("persons")
{
    Query = new Query
    {
        MatchAll = new MatchAllQuery()
    },
    Aggregations = new Dictionary<string, Aggregation>
    {
        { "agg_name", new Aggregation
        {
            Max = new MaxAggregation
            {
                Field = Infer.Field<Person>(x => x.Age)
            }
        }}
    },
    Size = 10
});
```

### Consume the response [_consume_the_response]

```csharp
var max = response.Aggregations!.GetMax("agg_name")!;
Console.WriteLine(max.Value);
```

## Sub-aggregation [_sub_aggregation]

### Fluent API [_fluent_api_2]

```csharp
var response = await client.SearchAsync<Person>(search => search
    .Indices("persons")
    .Query(query => query
        .MatchAll(_ => {})
    )
    .Aggregations(aggregations => aggregations
        .Add("firstnames", aggregation => aggregation <1>
            .Terms(terms => terms
                .Field(x => x.FirstName)
            )
            .Aggregations(aggregations => aggregations
                .Add("avg_age", aggregation => aggregation <2>
                    .Max(avg => avg
                        .Field(x => x.Age)
                    )
                )
            )
        )
    )
    .Size(10)
);
```

1. The top level `Terms` aggregation with name `firstnames`.
2. Nested aggregation of type `Max` with name `avg_age`.

### Object initializer API [_object_initializer_api_2]

```csharp
var response = await client.SearchAsync<Person>(new SearchRequest<Person>
{
    Query = new Query
    {
        MatchAll = new MatchAllQuery()
    },
    Aggregations = new Dictionary<string, Aggregation>
    {
        { "firstnames", new Aggregation
        {
            Terms = new TermsAggregation
            {
                Field = Infer.Field<Person>(x => x.FirstName)
            },
            Aggregations = new Dictionary<string, Aggregation>
            {
                { "avg_age", new Aggregation
                {
                    Max = new MaxAggregation
                    {
                        Field = Infer.Field<Person>(x => x.Age)
                    }
                }}
            }
        }}
    }
});
```

### Consume the response [_consume_the_response_2]

```csharp
var firstnames = response.Aggregations!.GetStringTerms("firstnames")!;
foreach (var bucket in firstnames.Buckets)
{
    var avg = bucket.Aggregations.GetAverage("avg_age")!;
    Console.WriteLine($"The average age for persons named '{bucket.Key}' is {avg}");
}
```
