---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/examples.html
---

# CRUD usage examples [examples]

This page helps you to understand how to perform various basic {{es}} CRUD (create, read, update, delete) operations using the .NET client. It demonstrates how to create a document by indexing an object into {{es}}, read a document back, retrieving it by ID or performing a search, update one of the fields in a document and delete a specific document.

These examples assume you have an instance of the `ElasticsearchClient` accessible via a local variable named `client` and several using directives in your C# file.

```csharp
using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
var client = new ElasticsearchClient(); <1>
```

1. The default constructor, assumes an unsecured {{es}} server is running and exposed on *http://localhost:9200*. See [connecting](/reference/connecting.md) for examples of connecting to secured servers and [Elastic Cloud](https://www.elastic.co/cloud) deployments.


The examples operate on data representing tweets. Tweets are modelled in the client application using a C# class named *Tweet* containing several properties that map to the document structure being stored in {{es}}.

```csharp
public class Tweet
{
    public int Id { get; set; } <1>
    public string User { get; set; }
    public DateTime PostDate { get; set; }
    public string Message { get; set; }
}
```

1. By default, the .NET client will try to find a property called `Id` on the class. When such a property is present it will index the document into {{es}} using the ID specified by the value of this property.



## Indexing a document [indexing-net]

Documents can be indexed by creating an instance representing a tweet and indexing it via the client. In these examples, we will work with an index named *my-tweet-index*.

```csharp
var tweet = new Tweet <1>
{
    Id = 1,
    User = "stevejgordon",
    PostDate = new DateTime(2009, 11, 15),
    Message = "Trying out the client, so far so good?"
};

var response = await client.IndexAsync(tweet, "my-tweet-index"); <2>

if (response.IsValidResponse) <3>
{
    Console.WriteLine($"Index document with ID {response.Id} succeeded."); <4>
}
```

1. Create an instance of the `Tweet` class with relevant properties set.
2. Prefer the async APIs, which require awaiting the response.
3. Check the `IsValid` property on the response to confirm that the request and operation succeeded.
4. Access the `IndexResponse` properties, such as the ID, if necessary.



## Getting a document [getting-net]

```csharp
var response = await client.GetAsync<Tweet>(1, idx => idx.Index("my-tweet-index")); <1>

if (response.IsValidResponse)
{
    var tweet = response.Source; <2>
}
```

1. The `GetResponse` is mapped 1-to-1 with the Elasticsearch JSON response.
2. The original document is deserialized as an instance of the Tweet class, accessible on the response via the `Source` property.



## Searching for documents [searching-net]

The client exposes a fluent interface and a powerful query DSL for searching.

```csharp
var response = await client.SearchAsync<Tweet>(s => s <1>
    .Index("my-tweet-index") <2>
    .From(0)
    .Size(10)
    .Query(q => q
        .Term(t => t.User, "stevejgordon") <3>
    )
);

if (response.IsValidResponse)
{
    var tweet = response.Documents.FirstOrDefault(); <4>
}
```

1. The generic type argument specifies the `Tweet` class, which is used when deserialising the hits from the response.
2. The index can be omitted if a `DefaultIndex` has been configured on `ElasticsearchClientSettings`, or a specific index was configured when mapping this type.
3. Execute a term query against the `user` field, searching for tweets authored by the user *stevejgordon*.
4. Documents matched by the query are accessible via the `Documents` collection property on the `SearchResponse`.


You may prefer using the object initializer syntax for requests if lambdas aren’t your thing.

```csharp
var request = new SearchRequest("my-tweet-index") <1>
{
    From = 0,
    Size = 10,
    Query = new TermQuery("user") { Value = "stevejgordon" }
};

var response = await client.SearchAsync<Tweet>(request); <2>

if (response.IsValidResponse)
{
    var tweet = response.Documents.FirstOrDefault();
}
```

1. Create an instance of `SearchRequest`, setting properties to control the search operation.
2. Pass the request to the `SearchAsync` method on the client.



## Updating documents [updating-net]

Documents can be updated in several ways, including by providing a complete replacement for an existing document ID.

```csharp
tweet.Message = "This is a new message"; <1>

var response = await client.UpdateAsync<Tweet, Tweet>("my-tweet-index", 1, u => u
    .Doc(tweet)); <2>

if (response.IsValidResponse)
{
    Console.WriteLine("Update document succeeded.");
}
```

1. Update a property on the existing tweet instance.
2. Send the updated tweet object in the update request.



## Deleting documents [deleting-net]

Documents can be deleted by providing the ID of the document to remove.

```csharp
var response = await client.DeleteAsync("my-tweet-index", 1);

if (response.IsValidResponse)
{
    Console.WriteLine("Delete document succeeded.");
}
```

