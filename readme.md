Repository for both **NEST** and **Elasticsearch.Net**, the two official [elasticsearch](https://github.com/elasticsearch/elasticsearch) .NET clients.

[![install from nuget](http://img.shields.io/nuget/v/NEST.svg?style=flat-square)](https://www.nuget.org/packages/NEST)[![downloads](http://img.shields.io/nuget/dt/NEST.svg?style=flat-square)](https://www.nuget.org/packages/NEST)    
Bleeding edge package:    
[![download](http://img.shields.io/myget/elasticsearch-net/v/NEST.svg?style=flat-square)](https://www.myget.org/gallery/elasticsearch-net)[![downloads](http://img.shields.io/myget/elasticsearch-net/dt/NEST.svg?style=flat-square)](https://www.myget.org/gallery/elasticsearch-net)    
Builds:    
[![teamcity](http://img.shields.io/teamcity/http/teamcity.codebetter.com/e/bt993.svg?style=flat-square)](http://teamcity.codebetter.com/viewType.html?buildTypeId=bt993)[![elasticsearch-net MyGet Build Status](https://www.myget.org/BuildSource/Badge/elasticsearch-net?identifier=624cebb3-a461-466f-9bac-7026c8ba615a)](https://www.myget.org/gallery/elasticsearch-net)

### Compatibility Matrix
<table>
    <tr>
        <th><b>Elasticsearch<b></td>
        <th><b>.NET clients<b></td>
        <th><b>Supported<b></td>
    </tr>
    <tr>
    	<td>0.x</td>
    	<td>0.x</td>
    	<td>No</td>
    </tr>
    <tr>
    	<td>1.x</td>
    	<td>1.x</td>
    	<td>Yes</td>
    </tr>
    <tr>
    	<td>2.x</td>
    	<td>2.x</td>
    	<td>Yes</td>
    </tr>
</table>

#[NEST](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest#nest-)

NEST is the official high-level .NET client of [elasticsearch](https://github.com/elasticsearch/elasticsearch).  It aims to be a solid, strongly typed client with a very concise API.

* High-level client that internally uses the low-level **Elasticsearch.Net** client
* Maps requests and responses to strongly typed objects with a fluent interface and object initializer syntax to build them
* Comes with a very powerful query DSL that maps one-to-one with Elasticsearch
* Takes advantage of .NET features where they make sense (i.e., covariant `IEnumerable<T>` result types, type and index inference)
* All calls have async variants

## Getting Started

### Installing

From the package manager console:

	PM> Install-Package NEST

or by simply searching for `NEST` in the package manager UI.

### Connecting

You can connect to your Elasticsearch cluster via a single node, or by specifying multiple nodes using a connection pool.  Using a connection pool has a few advantages over a single node connection, such as load balancing and cluster fail over support.

**Connecting to a single node**

```csharp
var node = new Uri("http://myserver:9200");
var settings = new ConnectionSettings(node);
var client = new ElasticClient(settings);
```

**Using a connection pool**

```csharp
var nodes = new Uri[]
{
	new Uri("http://myserver1:9200"),
	new Uri("http://myserver2:9200"),
	new Uri("http://myserver3:9200")
};

var pool = new StaticConnectionPool(nodes);
var settings = new ConnectionSettings(pool);
var client = new ElasticClient(settings);
```

### Indexing

Indexing a document is as simple as:

```csharp
var tweet = new Tweet
{
	Id = 1,
    User = "kimchy",
    PostData = new DateTime(2009, 11, 15),
    Message = "Trying out NEST, so far so good?"
};

var response = client.Index(tweet);
```

All the calls have async variants:

```csharp
var response = client.IndexAsync(tweet); // returns a Task<IndexResponse>
```

### Getting a document

```csharp
var response = client.Get<Tweet>(1); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
var tweet = response.Source; // the original document
```

### Searching for documents

NEST exposes a fluent interface and a [powerful query DSL](http://nest.azurewebsites.net/concepts/writing-queries.html)

```csharp
var response = client.Search<Tweet>(s => s
	.From(0)
	.Size(10)
	.Query(q =>
			q.Term(t => t.User, "kimchy")
			|| q.Match(mq => mq.Field(f => f.User).Query("nest"))
		)
	);
```

As well as an object initializer syntax if lamdas aren't your thing:

```csharp
var request = new SearchRequest
{
	From = 0,
	Size = 10,
	Query = new TermQuery { Field = "user", Value = "kimchy" } 
		|| new MatchQuery { Field = "description", Query = "nest" }
};

var response = client.Search<Tweet>(request);
```

### Falling back to Elasticsearch.Net

NEST also includes and exposes the low-level [Elasticsearch.Net](https://github.com/elasticsearch/elasticsearch-net/tree/develop/src/Elasticsearch.Net) client that you can fall back to incase anything is missing:

```csharp
//.Raw is of type IRawElasticClient
var response = client.Raw.SearchPost("myindex","elasticsearchprojects", new
{
	from = 0,
	size = 10,
	fields = new [] {"id", "name"},
	query = new {
		term = new {
			name = new {
				value= "NEST",
				boost = 2.0
			}
		}
	}
});
```

#### [Read the full documentation here](http://nest.azurewebsites.net/)
(The documentation is terribly out of date at the moment, but we're in the process of completely revamping them.  Please bare with us during the transition.)

#[Elasticsearch.Net](src/Elasticsearch.Net)

A low level, dependency free, client that has no opinions how you build and represent your requests and responses.

* Low-level client that provides a one-to-one mapping with the Elasticsearch REST API
* No dependencies
* Almost completely generated from the official REST API spec which makes it easy to keep up to date
* Comes with an integration test suite that can be generated from the YAML test definitions that the Elasticsearch core team uses to test their REST API
* Has no opinions on how you create or consume requests and responses
* Load balancing and cluster failover support
* All calls have async variants

## Getting Started

### Installing

From the package manager console:

	PM> Install-Package Elasticsearch.Net

or by searching for `Elastcsearch.Net` in the package manager UI.

### Connecting

Connecting using the low-level client is very similar to how you would connect using NEST.  In fact, the connection constructs that NEST use are actually Elasticsearch.Net constructs.  Thus, single node connections and connection pooling still apply when using Elasticsearch.Net.

```csharp
var node = new Uri("http://myserver:9200");
var config = new ConnectionConfiguration(node);
var client = new ElasticsearchClient(config);
```

Note the main difference here is that we are instantiating an `ElasticsearchClient` rather than `ElasticClient`, and `ConnectionConfiguration` instead of `ConnectionSettings`.

### Calling an API endpoint

Elasticsearch.Net is generated from the the official client REST specification, and thus maps to all Elasticsearch API endpoints.

```csharp
client.GetSource("myindex","mytype","1",qs=>qs
    .Routing("routingvalue")
);
```

will execute a GET to /myindex/mytype/1/_source?routing=routingvalue. All the methods and arguments are fully documented based on the documentation of the specification.

As you can see, Elasticsearch.Net also strongly types the query string parameters that it knows exist on an endpoint with full Intellisense documentation. However, unknown query string parameters can still be added:

```csharp
client.GetSource("myindex","mytype","1",qs=>qs
    .Routing("routingvalue")
    .Add("key","value")
);
```

The query string parameter is always optional.

### Providing a request body

You can specify a request body directly with a string:

```csharp
var myJson = @"{ ""hello"" : ""world"" }";
client.Index("myindex","mytype","1", myJson);
```

This will execute a POST to /myindex/mytype/1 with the provided string myJson passed verbatim as request body.

Alternatively, you can specify an anonymous object:

```csharp
var myJson = new { hello = "world" };
client.Index("myindex","mytype","1", myJson);
```

This will execute the same request, but this time myJson will be serialized by the registered ISerializer.

## Contributing

[Pull requests](https://github.com/elastic/elasticsearch-net/pulls) and [issues](https://github.com/elastic/elasticsearch-net/issues) are very much welcomed and appreciated.  If you'd like to report a bug or submit a feature/bug fix then please read our [contributing guide](contributing.md) first!

#### Many thanks to:
* [Q42](http://www.q42.nl) for supporting the development of NEST
* [redgate](http://www.red-gate.com) for supplying @Mpdreamz with an ANTS Memory Profiler 8 & ANTS Performance Profiler 8 licenses
* [jetBrains](http://www.jetbrains.com) for supplying @Mpdreamz with a dotTrace profiler and Resharper license
* [CodeBetter](http://codebetter.com) for hosting the continuous integration for NEST
* Everyone who has been awesome enough to contribute back to NEST (You're listed automatically on the [documentation page](http://nest.azurewebsites.net))

## Copyright and License

This software is Copyright (c) 2014-2015 by Elasticsearch BV.

This is free software, licensed under: [The Apache License Version 2.0](https://github.com/elasticsearch/elasticsearch-net/blob/develop/license.txt).
