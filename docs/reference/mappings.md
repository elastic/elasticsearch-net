---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/mappings.html
---

# Custom mapping examples [mappings]

This page demonstrates how to configure custom mappings on an index.

## Configure mappings during index creation [_configure_mappings_during_index_creation]

```csharp
await client.Indices.CreateAsync<Person>(index => index
    .Index("index")
    .Mappings(mappings => mappings
        .Properties(properties => properties
            .IntegerNumber(x => x.Age!)
            .Keyword(x => x.FirstName!, keyword => keyword.Index(false))
        )
    )
);
```


## Configure mappings after index creation [_configure_mappings_after_index_creation]

```csharp
await client.Indices.PutMappingAsync<Person>(mappings => mappings
    .Indices("index")
    .Properties(properties => properties
        .IntegerNumber(x => x.Age!)
        .Keyword(x => x.FirstName!, keyword => keyword.Index(false))
    )
);
```

