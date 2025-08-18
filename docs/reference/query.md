---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/query.html
---

# Query examples [query]

This page demonstrates how to perform a search request.

## Fluent API [_fluent_api_3]

```csharp
var response = await client.SearchAsync<Person>(search => search
    .Indices("persons")
    .Query(query => query
        .Term(term => term
            .Field(x => x.FirstName)
            .Value("Florian")
        )
    )
    .Size(10)
);
```

## Object initializer API [_object_initializer_api_3]

```csharp
var response = await client.SearchAsync<Person>(
    new SearchRequest<Person>("persons")
    {
        Query = new Query
        {
            Term = new TermQuery
            {
                Field = Infer.Field<Person>(x => x.FirstName),
                Value = "Florian"
            }
        },
        Size = 10
    }
);
```

## Consume the response [_consume_the_response_3]

```csharp
foreach (var person in response.Documents)
{
    Console.WriteLine(person.FirstName);
}
```
