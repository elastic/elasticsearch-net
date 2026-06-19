---
navigation_title: "Using ES|QL"
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/esql.html
---

# ES|QL in the .NET client [esql]

This page helps you understand and use [ES|QL](docs-content://explore-analyze/query-filter/languages/esql.md) in the .NET client.

The recommended way to work with ES|QL is the [LINQ to ES|QL](linq-to-esql.md) provider, which lets you write type-safe C# LINQ queries that are automatically translated to ES|QL at runtime.

For lower-level control, you can also use the Elasticsearch [ES|QL API](https://www.elastic.co/docs/api/doc/elasticsearch/group/endpoint-esql) directly: This is the most flexible approach, but it's also the most complex because you must handle results in their raw form.

You can choose the precise format of results, such as JSON, CSV, or text.

## How to use the ES|QL API [esql-how-to]

The [ES|QL query API](https://www.elastic.co/docs/api/doc/elasticsearch/group/endpoint-esql) allows you to specify how results should be returned. You can choose a [response format](docs-content://explore-analyze/query-filter/languages/esql-rest.md#esql-rest-format) such as CSV, text, or JSON, then fine-tune it with parameters like column separators and locale.

The following example gets ES|QL results as CSV and parses them:

```csharp
var response = await client.Esql.QueryAsync(r => r
    .Query("FROM index")
    .Format("csv")
);

var csvContents = Encoding.UTF8.GetString(response.Data);
```

## Consume ES|QL results [esql-consume-results]

The previous example showed that although the raw ES|QL API offers maximum flexibility, additional work is required to use the result data.

The recommended approach is [LINQ to ES|QL](linq-to-esql.md), which provides type-safe queries with automatic result mapping. Refer to the [LINQ to ES|QL](linq-to-esql.md) documentation for details.
