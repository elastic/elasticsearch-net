---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/request-response-serialization.html
---

# Serialization of Elasticsearch types [request-response-serialization]

Normally, it is not necessary to manually serialize or deserialize internal `Elastic.Clients.Elasticsearch` types. In rare cases, however, it may still be useful to do so.

:::{warning}

We strongly advise against persisting serialized Elasticsearch types, as their structure may vary between different versions of the client.

Instead, you should create your own POCO type and map/assign the desired properties to it.

:::

## Serializing Elasticsearch types

The default `System.Text.Json` serializer does not know how to serialize `Elastic.Clients.Elasticsearch` types. Therefore, (de-)serialization must be delegated to the built in `RequestResponseSerializer` of the client.

### Serialization [serialization]

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport.Extensions;

var query = new Query
{
    Match = new MatchQuery
    {
        Field = Infer.Field<Person>(p => p.FirstName),
        Query = "Florian"
    }
};

var json = client.RequestResponseSerializer.SerializeToString(query); <1>
```

1. Use the `RequestResponseSerializer` to serialize the `Query` instance to a JSON string.

### Deserialization [deserialization]

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport.Extensions;

var json = """
{
  "match": {
    "firstName": {
      "query": "Florian"
    }
  }
}
""";

var query = client.RequestResponseSerializer.Deserialize<Query>(json)!; <1>
```

1. Use the `RequestResponseSerializer` to deserialize the JSON string to a `Query` instance.

## Serializing Elasticsearch descriptor types

Descriptor types do not support direct (de-)serialization.  

However, starting with client versions 8.19 and 9.0, descriptor types support bidirectional conversion to/from the corresponding object representation.

We can use this functionality to indirectly (de)serialize descriptor types.

### Serialization [descriptor-serialization]

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport.Extensions;

var requestDescriptor = new SearchRequestDescriptor<Person>()
    .Query(q => q
        .Match(m => m
            .Field(p => p.FirstName)
            .Query("Florian")
        )
    );

var request = (SearchRequest)requestDescriptor; <1>
var json = client.RequestResponseSerializer.SerializeToString(request); <2>
```

1. Unwrap the request by casting the descriptor to the related object representation.
2. Serialize the unwrapped request instance.

### Deserialization [descriptor-deserialization]

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Transport.Extensions;

var json = """
{
  "query": {
    "match": {
      "firstName": {
        "query": "Florian"
      }
    }
  }
}
""";

var request = client.RequestResponseSerializer.Deserialize<SearchRequest>(json)!; <1>
var requestDescriptor = new SearchRequestDescriptor<Person>(request); <2>
```

1. Deserialize JSON payload into the object representation.
2. Create the related descriptor by wrapping the deserialized request object.

## Remarks

In addition to `SerializeToString()` and `Deserialize<T>()`, the `Elastic.Transport.Extensions` namespace offers several other extension methods related to (de)serialization.

## Limitations for request and response types

The (de)serialization of Elasticsearch request and response types is subject to certain limitations:

In addition to *body*-related properties, request types also have properties whose values are used in the HTTP *path* or *query* segment.

These properties are **not** taken into account during serialization. The result of serialization represents only the *body* of the request.

::::{warning}

When deserializing a request type, it may happen that otherwise required path or query parameters are not initialized. These properties must be manually assigned before using the request.

::::

A few request and response types in Elasticsearch do not support JSON bodies and therefore cannot be serialized in the above described way.

Two prominent examples are the `Bulk` or `MultiGet` APIs, which use NDJSON bodies. Binary responses also occur in some cases.
