---
navigation_title: "Using ES|QL"
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/esql.html
---

# ES|QL in the .NET client [esql]


This page helps you understand and use [ES|QL](docs-content://explore-analyze/query-filter/languages/esql.md) in the .NET client.

There are two ways to use ES|QL in the .NET client:

* Use the Elasticsearch [ES|QL API](https://www.elastic.co/docs/api/doc/elasticsearch/group/endpoint-esql) directly: This is the most flexible approach, but it’s also the most complex because you must handle results in their raw form. You can choose the precise format of results, such as JSON, CSV, or text.
* Use ES|QL high-level helpers: These helpers take care of parsing the raw response into something readily usable by the application. Several helpers are available for different use cases, such as object mapping, cursor traversal of results (in development), and dataframes (in development).


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

The previous example showed that although the raw ES|QL API offers maximum flexibility, additional work is required in order to make use of the result data.

To simplify things, try working with these three main representations of ES|QL results (each with its own mapping helper):

* **Objects**, where each row in the results is mapped to an object from your application domain. This is similar to what ORMs (object relational mappers) commonly do.
* **Cursors**, where you scan the results row by row and access the data using column names. This is similar to database access libraries.
* **Dataframes**, where results are organized in a column-oriented structure that allows efficient processing of column data.

```csharp
// ObjectAPI example
var response = await client.Esql.QueryAsObjectsAsync<Person>(x => x.Query("FROM index"));
foreach (var person in response)
{
    // ...
}
```
